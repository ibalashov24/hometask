namespace Problem4.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DataDrivenTests
    {
        private StackCalculatorStuff.StackCalculator stackCalculator;

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get => this.testContextInstance;
            set => this.testContextInstance = value;
        }

        [TestInitialize]
        public void CalculatorInitialization()
        {
            // Stacks are tested separately, so there is no difference which stack
            this.stackCalculator = new StackCalculatorStuff.StackCalculator(
                new StackStuff.Stack<double>(), "");
        }

        // Fully correct arguments
        [TestMethod]
        [DeploymentItem("CorrectTests.csv")]
        [DataSource(
        "Microsoft.VisualStudio.TestTools.DataSource.CSV",
        "|DataDirectory|\\CorrectTests.csv",
        "CorrectTests#csv",
            DataAccessMethod.Sequential)]
        public void DataOnWhichShouldBeCorrectResult()
        {
            // Program can not find column "Test" by name because of garbage
            // in memory at the place of the column name iside DataRow (see debugger watches)
            // I do not know how to fix this
            var expression = this.testContextInstance.DataRow[0].GetType() != typeof(System.DBNull) ?
                (string)this.testContextInstance.DataRow[0] : "";
            var correctResult = double.Parse(
                (string)this.testContextInstance.DataRow["Result"]);
            
            this.stackCalculator.Expression = expression;

            Assert.AreEqual(
                correctResult,
                this.stackCalculator.CalculateResult(),
                "Incorrect result!!!");
        }

        // Checks that calculator detects invalid arguments
        [TestMethod]
        [DeploymentItem("IncorrectTests.csv")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\IncorrectTests.csv",
            "IncorrectTests#csv",
             DataAccessMethod.Sequential)]
        public void DataOnWhichShouldBeArgumentException()
        {
            // Program can not find column "Test" by name because of garbage
            // in memory at the place of the column name iside DataRow (see debugger watches)
            // I do not know how to fix this
            var expression = this.testContextInstance.DataRow[0].GetType() != typeof(System.DBNull) ?
                (string)this.testContextInstance.DataRow[0] : "";

            this.stackCalculator.Expression = expression;

            try
            {
                this.stackCalculator.CalculateResult();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                return;
            }

            Assert.Fail("An exception was not thrown on the wrong test");
        }
    }
}
