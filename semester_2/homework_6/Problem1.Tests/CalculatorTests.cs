using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problem1.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        const double epsilon = 1e-6;
        private Calculator.Calculator calculator;

        [TestInitialize]
        public void InitCalculator()
        {
            this.calculator = new Calculator.Calculator();
        }

        [TestMethod]
        public void CalculatorValuesShouldBeDefaultInTheBeginning()
        {
            this.IsExpressionEmpty();
            Assert.IsTrue(this.AreDoubleEqual(
                0,
                this.calculator.LastOperand));
        }

        [TestMethod]
        public void SimpleCalculationsShouldWork()
        {
            this.calculator.SetLastOperandValue(15.5);
            this.calculator.FlushOperand();
            Assert.IsTrue(AreDoubleEqual(
                15.5,
                calculator.ExpressionValue));
        }

        [TestMethod]
        public void AdditionShouldWork()
        {
            this.calculator.SetLastOperandValue(10.5);
            this.calculator.FlushOperand();

            this.calculator.SetLastOperandValue(-11.5);
            this.calculator.SetNextOperator(Calculator.OperatorType.Plus);
            this.calculator.FlushOperand();

            Assert.IsTrue(AreDoubleEqual(
                -1,
                this.calculator.ExpressionValue));
        }

        [TestMethod]
        public void SubstractionShouldWork()
        {
            this.calculator.SetLastOperandValue(10.5);
            this.calculator.FlushOperand();

            this.calculator.SetLastOperandValue(2);
            this.calculator.SetNextOperator(Calculator.OperatorType.Minus);
            this.calculator.FlushOperand();

            Assert.IsTrue(AreDoubleEqual(
                8.5,
                this.calculator.ExpressionValue));
        }

        [TestMethod]
        public void MultiplicationShouldWork()
        {
            this.calculator.SetLastOperandValue(10.5);
            this.calculator.FlushOperand();

            this.calculator.SetLastOperandValue(2.5);
            this.calculator.SetNextOperator(
                Calculator.OperatorType.Multiplication);
            this.calculator.FlushOperand();

            Assert.IsTrue(AreDoubleEqual(
                26.25,
                this.calculator.ExpressionValue));
        }

        [TestMethod]
        public void DivisionShouldWork()
        {
            this.calculator.SetLastOperandValue(10.5);
            this.calculator.FlushOperand();

            this.calculator.SetLastOperandValue(2);
            this.calculator.SetNextOperator(Calculator.OperatorType.Division);
            this.calculator.FlushOperand();

            Assert.IsTrue(AreDoubleEqual(
                5.25,
                this.calculator.ExpressionValue));
        }

        [TestMethod]
        public void ComplexExpressionWOMultiplicationShouldBeCalculated()
        {
            var realResult = 0.0;
            for (int i = 0; i < 50; ++i)
            {
                this.calculator.SetLastOperandValue(i);
                this.calculator.FlushOperand();

                if (i % 2 == 0)
                {
                    this.calculator.SetNextOperator(Calculator.OperatorType.Minus);
                    realResult += i;
                }
                else
                {
                    this.calculator.SetNextOperator(Calculator.OperatorType.Plus);
                    realResult -= i;
                }
            }

            Assert.IsTrue(this.AreDoubleEqual(
                realResult,
                this.calculator.ExpressionValue));
        }

        [TestMethod]
        public void ComplexExpressionWithMultiplicationShouldBeCalculated()
        {
            var realResult = 5.0;
            this.calculator.SetLastOperandValue(5);
            this.calculator.FlushOperand();

            for (int i = 0; i < 3; ++i)
            {
                for (int k = 0; k < 5; k++)
                {
                    this.calculator.SetNextOperator(Calculator.OperatorType.Plus);
                    this.calculator.SetLastOperandValue(k);
                    this.calculator.FlushOperand();

                    realResult += k;
                }

                this.calculator.SetNextOperator(Calculator.OperatorType.Plus);
                this.calculator.SetLastOperandValue(5);
                this.calculator.FlushOperand();
                var realMultiplicationResult = 5.0;

                for (int j = 1; j < 50; ++j)
                {
                    if (j % 2 == 0)
                    {
                        this.calculator.SetNextOperator(Calculator.OperatorType.Multiplication);
                        realMultiplicationResult *= j % 13 + 1;
                    }
                    else
                    {
                        this.calculator.SetNextOperator(Calculator.OperatorType.Division);
                        realMultiplicationResult /= j % 13 + 1;
                    }

                    this.calculator.SetLastOperandValue(j % 13 + 1);
                    this.calculator.FlushOperand();
                }

                realResult += realMultiplicationResult;
            }

            Assert.IsTrue(this.AreDoubleEqual(
                realResult,
                this.calculator.ExpressionValue));
        }

        [TestMethod]
        public void SqrtShouldWork()
        {
            this.calculator.SetLastOperandValue(65);
            this.calculator.ApplySqrtToOperand();

            Assert.IsTrue(this.AreDoubleEqual(
                Math.Sqrt(65),
                this.calculator.LastOperand));
        }

        [TestMethod]
        public void SquareShouldWork()
        {
            this.calculator.SetLastOperandValue(15.56);
            this.calculator.ApplySqrToOperand();

            Assert.IsTrue(this.AreDoubleEqual(
                Math.Pow(15.56, 2),
                this.calculator.LastOperand));
        }

        [TestMethod]
        public void ClearMemoryOperationShouldWork()
        {
            this.calculator.SetLastOperandValue(1234.5678);
            this.calculator.FlushOperand();

            this.calculator.SetLastOperandValue(5);
            this.calculator.SetNextOperator(Calculator.OperatorType.Plus);
            this.calculator.FlushOperand();

            this.calculator.SetNextOperator(Calculator.OperatorType.Division);
            this.calculator.SetLastOperandValue(2);
            this.calculator.FlushOperand();

            Assert.AreEqual(
                "1234,5678 + 5 / 2 /",
                this.calculator.Expression);
            Assert.IsTrue(
                Math.Abs(this.calculator.ExpressionValue) > epsilon);

            this.calculator.ClearMemory();

            Assert.IsTrue(this.IsExpressionEmpty());
            Assert.IsTrue(this.AreDoubleEqual(
                0,
                this.calculator.LastOperand));
        }

        [TestMethod]
        public void PowerMinusOneShouldWork()
        {
            this.calculator.SetLastOperandValue(15.5);
            this.calculator.PutOperandIntoDenominator();

            Assert.IsTrue(this.AreDoubleEqual(
                1/15.5,
                this.calculator.LastOperand));
        }

        [TestMethod]
        public void NegationShouldWork()
        {
            this.calculator.SetLastOperandValue(42);
            this.calculator.NegateCurrentOperand();

            Assert.IsTrue(this.AreDoubleEqual(
                -42,
                this.calculator.LastOperand));

            this.calculator.NegateCurrentOperand();
            Assert.IsTrue(this.AreDoubleEqual(
                42,
                this.calculator.LastOperand));
        }

        [TestMethod]
        public void ReinitializationShouldWork()
        {
            calculator.SetLastOperandValue(10.5);
            calculator.FlushOperand();

            calculator.SetLastOperandValue(2);
            calculator.SetNextOperator(Calculator.OperatorType.Minus);
            calculator.FlushOperand();

            this.calculator.ReinitializeCalculatorWithResult();

            Assert.IsTrue(this.IsExpressionEmpty());
            Assert.IsTrue(this.AreDoubleEqual(
                8.5,
                this.calculator.LastOperand));
        }

        [TestMethod]
        public void SettingOperationsBeforeFirstOperandShouldAffectOnResult()
        {
            this.calculator.SetNextOperator(Calculator.OperatorType.Minus);
            this.calculator.SetLastOperandValue(153);
            this.calculator.FlushOperand();

            Assert.IsTrue(this.AreDoubleEqual(
                -153,
                this.calculator.ExpressionValue));
            Assert.AreEqual("- 153 -", this.calculator.Expression);
        }

        private bool IsExpressionEmpty()
        {
            var expressionValueZero = this.AreDoubleEqual(
                0,
                this.calculator.ExpressionValue);
            
            var expressionEmpty 
                = string.Empty == this.calculator.Expression;

            return expressionEmpty && expressionValueZero;
        }

        private bool AreDoubleEqual(double a, double b)
        {
            return Math.Abs(a - b) < epsilon;
        }
    }
}
