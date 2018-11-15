namespace MyNUnit.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    using MyNUnit.TestTools;

    public class TestRunner
    {
        private Assembly testAssembly;

        public TestRunner(Assembly assembly)
        {
            this.testAssembly = assembly;
        }

        public async Task<List<TestResult>> RunAllTestsAsync()
        {
            return await Task.Factory.StartNew<List<TestResult>>(this.RunAllTests);
        }

        public List<TestResult> RunAllTests()
        {
            var results = new List<TestResult>();

            foreach (var classToTest in this.FindAllTestClasses())
            {
                var testMethods = this.FindTestMethodsInClass(classToTest);

                object testClassInstance;
                try
                {
                    testClassInstance =
                        System.Activator.CreateInstance(classToTest);
                }
                catch (Exception)
                {
                    results.AddRange(this.ReportAllTestsFailed(
                        testMethods,
                        $"Cannot create instance of {classToTest.Name}"));
                    continue;
                }

                if (!this.RunSecurely(testMethods.BeforeClassMethods, testClassInstance))
                {
                    results.AddRange(this.ReportAllTestsFailed(
                        testMethods,
                        $"Failed to execute Before class methods of {classToTest.Name}"));
                    continue;
                }

                bool fatalErrorOccured = false;
                foreach (var method in testMethods.TestMethods)
                {
                    if (fatalErrorOccured ||
                        !fatalErrorOccured && !this.RunSecurely(testMethods.BeforeMethods,
                                                               testClassInstance))
                    {
                        fatalErrorOccured = true;
                        results.Add(this.ReportTestFailed(method,
                            "Failed to execute Before or " +
                            $"After methods of {classToTest.Name}"));
                        continue;
                    }

                    var testResult = this.RunTest(method, testClassInstance);
                    if (testResult != null)
                    {
                        results.Add(testResult);
                    }

                    fatalErrorOccured = !this.RunSecurely(testMethods.AfterMethods,
                                                        testClassInstance);
                }

                if (!this.RunSecurely(testMethods.AfterClassMethods,
                                      testClassInstance))
                {
                    this.SendMessage(
                        $"Failed to execute After class methods of {classToTest.Name}");
                }
            }

            return results;
        }

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

        private bool RunSecurely(List<MethodInfo> methodList, object instance)
        {
            foreach (var method in methodList)
            {
                if (!this.RunSecurely(method, instance))
                {
                    return false;
                }
            }

            return true;
        }

        private bool RunSecurely(MethodInfo method, object instance)
        {
            try
            {
                method.Invoke(instance, Array.Empty<object>());
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private TestResult RunTest(MethodInfo method, object instance)
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
                stopwatch.Stop();

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
            stopwatch.Stop();

            return this.ReportTestSucceed(method, $"Elapsed {stopwatch.Elapsed}s");
        }

        private TestResult ReportTestFailed(MethodInfo test, string message)
        {
            this.SendMessage($"{test.Name} failed. {message}");
            return new TestResult(test.Name, false, message);
        }

        private List<TestResult> ReportAllTestsFailed(
            TestMethodsContainer container,
            string message)
        {
            var results = new List<TestResult>();
            foreach (var test in container.TestMethods)
            {
                results.Add(this.ReportTestFailed(test, message));
            }
            return results;
        }

        private TestResult ReportTestSucceed(MethodInfo test, string message)
        {
            this.SendMessage($"{test.Name} passed. {message}");
            return new TestResult(test.Name, true, string.Empty);
        }

        private void ReportTestIgnored(MethodInfo test, string message)
        {
            this.SendMessage($"Ignoring {test.Name}. {message}");
        }

        private void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
