using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problem1.Tests
{
    [TestClass]
    public class DataDrivenTests
    {
        public TestContext TestContext
        {
            get;
            set;
        }
        
        /// <summary>
        /// Tests which should pass
        /// </summary>
        [DataTestMethod]
        [DeploymentItem("CorrectTests.csv")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\CorrectTests.csv",
            "CorrectTests#csv",
            DataAccessMethod.Sequential)]
        public void CorrectTests()
        {
            var tree = new Tree.ExpressionTree((string)TestContext.DataRow[0]);

            Assert.AreEqual(
                double.Parse((string)TestContext.DataRow["Result"]), 
                tree.Value);

            Assert.AreEqual(
                (string)TestContext.DataRow[0],
                tree.ToString());

            Assert.AreEqual(
                (string)TestContext.DataRow["Infix"],
                tree.GetInfixRepresentation());
        }

        [DataTestMethod]
        [DeploymentItem("ExceptionTests.csv")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\ExceptionTests.csv",
            "ExceptionTests#csv",
            DataAccessMethod.Sequential)]
        [ExpectedException(typeof(Tree.Exception.InvalidExpressionException))]
        public void ExceptionTests()
        {
            // Constructor runs calculations
            var tree = new Tree.ExpressionTree((string)TestContext.DataRow[0]);
        }
    }
}
