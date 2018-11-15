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

        public void RunAllTestsAsync()
        {
            var testTask = new Task(() =>
            {
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
                        this.ReportAllTestsFailed(
                            testMethods,
                            $"Cannot create instance of {classToTest.Name}");
                        continue;
                    }

                    if (!this.RunSecurely(testMethods.BeforeClassMethods,
                                         testClassInstance))
                    {
                        this.ReportAllTestsFailed(
                            testMethods,
                            $"Failed to execute Before class methods of {classToTest.Name}");
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
                            this.ReportTestFailed(method,
                                                  "Failed to execute Before or " +
                                                  $"After methods of {classToTest.Name}");
                            continue;
                        }

                        this.RunTest(method, testClassInstance);

                        fatalErrorOccured = this.RunSecurely(testMethods.AfterMethods,
                                                            testClassInstance);
                    }

                    if (!this.RunSecurely(testMethods.AfterClassMethods,
                                          testClassInstance))
                    {
                        this.SendMessage(
                            $"Failed to execute After class methods of {classToTest.Name}");
                    }
                }
            });

            testTask.Start();
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

        private bool RunTest(MethodInfo method, object instance)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                method.Invoke(instance, Array.Empty<object>());
            }
            catch (Exception e)
            {
                stopwatch.Stop();
                this.ReportTestFailed(
                    method,
                    $"{e.GetType().ToString()} ({stopwatch.Elapsed}s): {e.Message}");

                return false;
            }
            stopwatch.Stop();

            this.ReportTestSucceed(method, $"Elapsed {stopwatch.Elapsed}s");

            return true;
        }

        void ReportTestFailed(MethodInfo test, string message)
        {
            this.SendMessage($"{test.Name} failed. {message}");
        }

        private void ReportAllTestsFailed(
            TestMethodsContainer container,
            string message)
        {
            foreach (var test in container.TestMethods)
            {
                this.ReportTestFailed(test, message);
            }
        }

        private void ReportTestSucceed(MethodInfo test, string message)
        {
            this.SendMessage($"{test.Name} passed. {message}");
        }

        private void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
