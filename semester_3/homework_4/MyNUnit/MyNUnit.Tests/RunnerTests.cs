using NUnit.Framework;
using MyNUnit.Runner;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyNUnit.Tests
{
    [TestFixture]
    public class RunnerTests
    {
        [Test]
        public void SimpleTest()
        {
            var assembly = this.GetAssemblyByName("SimpleTest");
            var actual = this.GetTestResults(assembly);
            
            var expected = new List<TestResult>();
            expected.Add(new TestResult("TestWhichMustWorkGood", true, string.Empty));
            expected.Add(new TestResult("TestWhichMustWorkBad", false, "It failed :("));

            Assert.That(actual, Is.EquivalentTo(expected).Using((TestResult a, TestResult b) => 
                a.TestName == b.TestName && a.IsPassed == b.IsPassed));
        }

        [Test]
        public void TesterShouldWorkCorrectlyOnProjectWithNoTests()
        {
            var assembly = this.GetAssemblyByName("ProjectWithoutTests");
            var actual = this.GetTestResults(assembly);

            var expected = new List<TestResult>();
            Assert.That(actual, Is.EquivalentTo(expected).Using((TestResult a, TestResult b) =>
                a.TestName == b.TestName && a.IsPassed == b.IsPassed));
        }

        [Test]
        public void MultipleCorrectAndFailedTests()
        {
            var assembly = this.GetAssemblyByName("MultipleCorrectAndFailedTests");
            var actual = this.GetTestResults(assembly);

            var expected = new List<TestResult>();
            expected.Add(new TestResult("TestWhichMustWorkGood", true, string.Empty));
            expected.Add(new TestResult("TestWhichMustWorkBad", false, string.Empty));
            expected.Add(new TestResult("DivisionByZero", false, string.Empty));
            expected.Add(new TestResult("LongCycle", true, string.Empty));
            expected.Add(new TestResult("Recursion", true, string.Empty));
            expected.Add(new TestResult("ThrowingException", false, string.Empty));

            Assert.That(actual, Is.EquivalentTo(expected).Using((TestResult a, TestResult b) =>
                a.TestName == b.TestName && a.IsPassed == b.IsPassed));
        }

        [Test]
        public void BeforeAndAfterWorksCorrectly()
        {
            var assembly = this.GetAssemblyByName("BeforeAndAfterWorksCorrectly");
            var actual = this.GetTestResults(assembly);

            var resultFile = File.OpenRead(
                Path.Combine(Path.GetTempPath(), "MyNUnitTestBeforeAfter.tst"));
            var result = resultFile.ReadByte();

            // Magic number was calculated on the calculator 
            // (and is the same as in the test class file)
            Assert.AreEqual(945 % 256, result);
        }

        [Test]
        public void TestShouldBehaveCorrectlyAfterErrorInBeforeClassMethod()
        {
            if (File.Exists(Path.Combine(Path.GetTempPath(), "MyNUnitFailInBeforeClass.tst")))
            {
                File.Delete(Path.Combine(Path.GetTempPath(), "MyNUnitFailInBeforeClass.tst"));
            }
            
            var assembly = this.GetAssemblyByName("FailInBeforeClassMethod");
            var actual = this.GetTestResults(assembly);

            // BeforeClass methods was called before class constructor
            Assert.IsFalse(File.Exists(Path.Combine(Path.GetTempPath(), "MyNUnitFailInBeforeClass.tst")));
        }

        [Test]
        public void TestShouldBehaveCorrectlyAfterErrorInAfterClassMethod()
        {
            var assembly = this.GetAssemblyByName("FailInAfterClassMethod");
            var actual = this.GetTestResults(assembly);

            var expected = new List<TestResult>();
            expected.Add(new TestResult("TestWhichMustWorkGood", true, string.Empty));
            expected.Add(new TestResult("TestWhichMustWorkBad", false, "It failed :("));
            expected.Add(new TestResult("TestWhichAlsoMustWorkGood", true, string.Empty));

            Assert.That(actual, Is.EquivalentTo(expected).Using((TestResult a, TestResult b) =>
                a.TestName == b.TestName && a.IsPassed == b.IsPassed));
        }

        [Test]
        public void TestShouldBehaveCorrectlyAfterErrorInAfterMethod()
        {
            var assembly = this.GetAssemblyByName("FailInAfterMethod");
            var actual = this.GetTestResults(assembly);

            // After method crashed on 2 (out of 4) run
            Assert.AreEqual(2, actual.Count((a) => a.IsPassed));
        }

        [Test]
        public void TestShouldBehaveCorrectlyAfterErrorInBeforeMethod()
        {
            var assembly = this.GetAssemblyByName("FailInBeforeMethod");
            var actual = this.GetTestResults(assembly);

            // Before method crashed on 2 (out of 4) run
            Assert.AreEqual(1, actual.Count((a) => a.IsPassed));
        }

        [Test]
        public void ExpectedFlagShouldWorkCorrectly()
        {
            var assembly = this.GetAssemblyByName("TestWithExpectedParam");
            var actual = this.GetTestResults(assembly);

            var expected = new List<TestResult>();
            expected.Add(new TestResult("TestWhichMustWorkGood", true, string.Empty));
            expected.Add(new TestResult("TestWhichMustWorkBad", false, string.Empty));

            Assert.That(actual, Is.EquivalentTo(expected).Using((TestResult a, TestResult b) =>
                a.TestName == b.TestName && a.IsPassed == b.IsPassed));
        }

        [Test]
        public void IgnoreFlagShouldWorkCorrectly()
        {
            var assembly = this.GetAssemblyByName("TestWithIgnoreParam");
            var actual = this.GetTestResults(assembly);

            var expected = new List<TestResult>();
            expected.Add(new TestResult("TestWhichMustWorkGood", true, string.Empty));

            Assert.That(actual, Is.EquivalentTo(expected).Using((TestResult a, TestResult b) =>
                a.TestName == b.TestName && a.IsPassed == b.IsPassed));
        }

        [Test]
        public void AreEqualShouldWorkCorrectly()
        {
            var assembly = this.GetAssemblyByName("AreEqualTest");
            var actual = this.GetTestResults(assembly);

            var expected = new List<TestResult>();
            expected.Add(new TestResult("AreEqualForStringsShouldPass", true, string.Empty));
            expected.Add(new TestResult("AreEqualForStringsShouldNotPass", false, string.Empty));
            expected.Add(new TestResult("AreEqualForObjectsShouldPass", true, string.Empty));
            expected.Add(new TestResult("AreEqualForObjectsShouldNotPass", false, string.Empty));

            Assert.That(actual, Is.EquivalentTo(expected).Using((TestResult a, TestResult b) =>
                a.TestName == b.TestName && a.IsPassed == b.IsPassed));
        }

        [Test]
        public void TesterShouldHandleTestsWhichAreInDifferentClassesCorrectly()
        {
            var assembly = this.GetAssemblyByName("TestsInDifferentClasses");
            var actual = this.GetTestResults(assembly);

            var expected = new List<TestResult>();
            expected.Add(new TestResult("TestInFirstClassWhichShouldWorkGood", true, string.Empty));
            expected.Add(new TestResult("TestInFirstClassWhichShouldWorkBad", false, string.Empty));
            expected.Add(new TestResult("TestInSecondClassWhichShouldWorkGood", true, string.Empty));
            expected.Add(new TestResult("TestInSecondClassWhichShouldWorkBad", false, string.Empty));

            Assert.That(actual, Is.EquivalentTo(expected).Using((TestResult a, TestResult b) =>
                a.TestName == b.TestName && a.IsPassed == b.IsPassed));
        }

        private Assembly GetAssemblyByName(string assemblyName)
        {
            var pathSearchResult = Directory.GetFiles(
                $"../../../../{assemblyName}/bin",
                $"{assemblyName}.dll",
                SearchOption.AllDirectories)[0];
            var assembly = Assembly.LoadFrom(pathSearchResult);

            return assembly;
        }

        private List<TestResult> GetTestResults(Assembly assembly)
        {
            var runner = new TestRunner(assembly);
            return runner.RunAllTestsAsync();
        }
    }
}