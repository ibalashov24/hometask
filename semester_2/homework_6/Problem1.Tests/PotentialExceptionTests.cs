using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problem1.Tests
{
    [TestClass]
    public class PotentialExceptionTests
    {
        private Calculator.Calculator calculator;

        [TestInitialize]
        public void InitCalculator()
        {
            this.calculator = new Calculator.Calculator();
        }

        // In order to improve speed of work and 
        // show system's standard designations of special values
        [TestMethod]
        public void DividingByZeroShouldReturnInfinity()
        {
            this.calculator.SetLastOperandValue(15);
            this.calculator.FlushOperand();

            this.calculator.SetLastOperandValue(0);
            this.calculator.SetNextOperator(Calculator.OperatorType.Division);
            this.calculator.FlushOperand();

            double dividingResult = this.calculator.ExpressionValue;
            Assert.IsTrue(double.IsInfinity(dividingResult));
        }

        [TestMethod]
        public void MinusFirtsPowerOfZeroShouldBeInfinty()
        {
            this.calculator.SetLastOperandValue(0);
            this.calculator.PutOperandIntoDenominator();
            Assert.IsTrue(double.IsInfinity(this.calculator.LastOperand));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NotDefinedOperationTypeShouldBeCatched()
        {
            this.calculator.SetNextOperator(Calculator.OperatorType.NotDefined);
        }

        // TODO: Prepare several tests about overflowing 
        // (and fix this problem in the calculator)
    }
}