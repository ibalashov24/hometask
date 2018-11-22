namespace MyNUnit.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Threading.Tasks;

    using MyNUnit.TestTools;

    /// <summary>
    /// Runs MyNUnit test for given assembly
    /// </summary>
    public class TestRunner
    {
        /// <summary>
        /// Assembly where test are contained
        /// </summary>
        private readonly Assembly testAssembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunner"/> class.
        /// </summary>
        /// <param name="assembly">Assembly to test</param>
        public TestRunner(Assembly assembly)
        {
            this.testAssembly = assembly;
        }

        /// <summary>
        /// Runs all tests in given assembly
        /// </summary>
        /// <returns>Test results</returns>
        public List<TestResult> RunAllTestsAsync()
        {
            var results = new ConcurrentQueue<TestResult>();

            Parallel.ForEach<Type>(this.FindAllTestClasses(), (classToTest) =>
                {
                    this.TestClassAsync(classToTest, results);
                });

            return new List<TestResult>(results);
        }

        /// <summary>
        /// Runs all tests from given class
        /// </summary>
        /// <param name="classToTest">Class to test</param>
        /// <param name="results">Testing results</param>
        private void TestClassAsync(Type classToTest, ConcurrentQueue<TestResult> results)
        {
            var testMethods = this.FindTestMethodsInClass(classToTest);

            if (!this.RunStaticClassMethods(testMethods.BeforeClassMethods, out string message))
            {
                this.ReportAllTestsFailed(results, testMethods, message);
                return;
            }

            Parallel.ForEach<MethodInfo>(testMethods.TestMethods, (method) =>
                {
                    var testResult = this.RunTest(
                        classToTest,
                        method,
                        testMethods.BeforeMethods,
                        testMethods.AfterMethods);

                    // If null then test was ingnored
                    if (testResult != null)
                    {
                        results.Enqueue(testResult);
                    }
                });

            if (!this.RunStaticClassMethods(testMethods.AfterClassMethods, out message))
            {
                this.SendMessage(message);
            }

            return;
        }

        /// <summary>
        /// Runs all static methods from the list
        /// </summary>
        /// <param name="methods">Method list</param>
        /// <param name="instance">Class instance</param>
        /// <param name="resultMessage">Supporting message (fail or success)</param>
        /// <returns>True if success</returns>
        private bool RunStaticClassMethods(List<MethodInfo> methods, out string resultMessage)
        {
            foreach (var staticClassMethod in methods)
            {
                if (!staticClassMethod.IsStatic)
                {
                    resultMessage = $"{staticClassMethod.Name} is should be static";
                    return false;
                }

                if (!this.RunMethodSecurely(staticClassMethod, null, out resultMessage))
                {
                    return false;
                }
            }

            resultMessage = "Success";
            return true;
        }

        /// <summary>
        /// Runs all non static methods from the list
        /// </summary>
        /// <param name="methods">Method list</param>
        /// <param name="instance">Class instance</param>
        /// <param name="resultMessage">Supporting message (fail or success)</param>
        /// <returns>True if success</returns>
        private bool RunNonStaticClassMethods(
            List<MethodInfo> methods,
            object instance,
            out string resultMessage)
        {
            foreach (var method in methods)
            {
                if (!this.RunMethodSecurely(method, instance, out resultMessage))
                {
                    return false;
                }
            }

            resultMessage = "Success";
            return true;
        }

        /// <summary>
        /// Executes given test and returns results of testing
        /// </summary>
        /// <param name="method">Test method</param>
        /// <param name="instance">Instance of the class where method is contined</param>
        /// <returns>Testing results</returns>
        private TestResult RunTest(
            Type testClass,
            MethodInfo method, 
            List<MethodInfo> beforeMethods, 
            List<MethodInfo> afterMethods)
        {
            object instance;
            try
            {
                instance = System.Activator.CreateInstance(testClass);
            }
            catch (Exception e)
            {
                return this.ReportTestFailed(
                    method, $"Unable to create class instance: {e.GetType().ToString()}");
            }

            if (!this.RunNonStaticClassMethods(beforeMethods, instance, out string message))
            {
                return this.ReportTestFailed(method, message);
            }

            var testResult = this.RunTestBody(method, instance);

            if (!this.RunNonStaticClassMethods(afterMethods, instance, out message) 
                && testResult.IsPassed)
            {
                return this.ReportTestFailed(method, message);
            }

            return testResult;
        }

        /// <summary>
        /// Tests test method itself
        /// </summary>
        /// <param name="method">Test method</param>
        /// <param name="instance">Class instance</param>
        /// <returns></returns>
        private TestResult RunTestBody(MethodInfo method, object instance)
        {
            var testParams = method.GetCustomAttribute<TestAttribute>();
            if (testParams.IgnoreReason != null)
            {
                this.ReportTestIgnored(method, testParams.IgnoreReason);
                return null;
            }

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                method.Invoke(instance, Array.Empty<object>());
            }
            catch (Exception e)
            {
                var exceptionType = e.InnerException.GetType().ToString();
                var exceptionMessage = e.InnerException.Message;

                if (exceptionType == testParams.Expected?.ToString())
                {
                    return this.ReportTestSucceed(
                        method,
                        $"{exceptionType} ({stopwatch.Elapsed}s): {exceptionMessage}");
                }
                else
                {
                    return this.ReportTestFailed(
                        method,
                        $"{exceptionType} ({stopwatch.Elapsed}s): {exceptionMessage}");
                }
            }
            finally
            {
                stopwatch.Stop();
            }

            if (testParams.Expected != null)
            {
                return this.ReportTestFailed(
                        method,
                        $"({stopwatch.Elapsed}s): {testParams.Expected.ToString()} did not fire");
            }

            return this.ReportTestSucceed(method, $"Elapsed {stopwatch.Elapsed}s");
        }

        /// <summary>
        /// Detects all classes with tests (with TestClass attribute)
        /// </summary>
        /// <returns>Classes with tests</returns>
        private IEnumerable<Type> FindAllTestClasses()
        {
            foreach (var candidateClass in this.testAssembly.ExportedTypes)
            {
                if (candidateClass.IsClass &&
                    candidateClass.GetCustomAttribute(typeof(TestClassAttribute)) != null)
                {
                    yield return candidateClass;
                }
            }
        }

        /// <summary>
        /// Detects all test methods of all types 
        /// (Test, Before, After, etc.) in given class
        /// </summary>
        /// <param name="classToTest">Class with tests</param>
        /// <returns>Container with all test methods</returns>
        private TestMethodsContainer FindTestMethodsInClass(Type classToTest)
        {
            var result = new TestMethodsContainer();

            foreach (var method in classToTest.GetMethods())
            {
                if (method.GetCustomAttribute(typeof(TestAttribute)) != null)
                {
                    result.TestMethods.Add(method);
                }
                else if (method.GetCustomAttribute(typeof(AfterAttribute)) != null)
                {
                    result.AfterMethods.Add(method);
                }
                else if (method.GetCustomAttribute(typeof(BeforeAttribute)) != null)
                {
                    result.BeforeMethods.Add(method);
                }
                else if (method.GetCustomAttribute(typeof(AfterClassAttribute)) != null)
                {
                    result.AfterClassMethods.Add(method);
                }
                else if (method.GetCustomAttribute(typeof(BeforeClassAttribute)) != null)
                {
                    result.BeforeClassMethods.Add(method);
                }
            }

            return result;
        }

        /// <summary>
        /// Run method and checks if any exeption happened
        /// </summary>
        /// <param name="method">Method to test</param>
        /// <param name="instance">Class instance where method from</param>
        /// <param name="message">Supporting message</param>
        /// <returns></returns>
        private bool RunMethodSecurely(MethodInfo method, object instance, out string message)
        {
            try
            {
                method.Invoke(instance, Array.Empty<object>());
            }
            catch (Exception e)
            {
                var type = e.InnerException.GetType().ToString();
                var exceptionMessage = e.InnerException.Message;

                message = $"Failed to execute {method.Name}. {type}: {exceptionMessage}";
                return false;
            }

            message = "Success";
            return true;
        }

        /// <summary>
        /// Reports that given test failed and generates full report
        /// </summary>
        /// <param name="test">Failed test</param>
        /// <param name="message">Custom message</param>
        /// <returns>Testing report</returns>
        private TestResult ReportTestFailed(MethodInfo test, string message)
        {
            this.SendMessage($"{test.Name} failed. {message}");
            return new TestResult(test.Name, false, message);
        }

        /// <summary>
        /// Reports that all tests in class failed and generates full report
        /// </summary>
        /// <param name="container">Container with all test methods</param>
        /// <param name="message">Custom message</param>
        /// <returns>Testing reports</returns>
        private void ReportAllTestsFailed(
            ConcurrentQueue<TestResult> outContainer,
            TestMethodsContainer container,
            string message)
        {
            foreach (var test in container.TestMethods)
            {
                outContainer.Enqueue(this.ReportTestFailed(test, message));
            }
        }

        /// <summary>
        /// Reports that given test succeed and generates full report
        /// </summary>
        /// <param name="test">Succeed test</param>
        /// <param name="message">Custom message</param>
        /// <returns>Testing report</returns>
        private TestResult ReportTestSucceed(MethodInfo test, string message)
        {
            this.SendMessage($"{test.Name} passed. {message}");
            return new TestResult(test.Name, true, string.Empty);
        }

        /// <summary>
        /// Reports that given test was ignored
        /// </summary>
        /// <param name="test">Ignored test</param>
        /// <param name="message">Custom message</param>
        private void ReportTestIgnored(MethodInfo test, string message)
        {
            this.SendMessage($"Ignoring {test.Name}. {message}");
        }

        /// <summary>
        /// Sends text message to console
        /// </summary>
        /// <param name="message">Message</param>
        private void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
