namespace Calculator
{
    using System;
    using System.Collections.Generic;

    public class Calculator : ICalculator
    {
        private string expression = "";
        private double fullSum;
        private double lastMultiplicationResult;

        private double lastOperandValue;
        private OperatorType lastOperationSign
            = OperatorType.NotDefined;

        public string Expression
        {
            get => this.expression +
                this.GetOperationSign(this.lastOperationSign);
            private set => this.expression = value;
        }

        public double LastOperand
        {
            get => this.lastOperandValue;
        }

        public double ExpressionValue
        {
            get => this.fullSum + this.lastMultiplicationResult;
        }

        public Calculator()
        {
        }

        public void SetLastOperandValue(double value)
        {
            this.lastOperandValue = value;
        }

        public void SetNextOperator(OperatorType nextOperator)
        {
            if (nextOperator == OperatorType.NotDefined)
            {
                throw new InvalidOperationException(
                    "Operator must be defined");
            }

            this.lastOperationSign = nextOperator;
        }

        public void FlushOperand()
        {
            if (this.expression.Length > 0 &&
                this.lastOperationSign == OperatorType.NotDefined)
            {
                throw new InvalidOperationException(
                    "Operation type is undefined");
            }

            FlushLastOperand();
            this.lastOperandValue = 0;
        }

        public void ClearMemory()
        {
            this.expression = "";
            this.lastOperationSign = OperatorType.NotDefined;
            this.lastOperandValue = 0;

            this.fullSum = 0;
            this.lastMultiplicationResult = 0;
        }

        public void NegateCurrentOperand()
        {
            this.lastOperandValue = -this.lastOperandValue;
        }

        public void PutOperandIntoDenominator()
        {
            this.lastOperandValue = 1 / this.lastOperandValue;
        }

        public void ApplySqrtToOperand()
        {
            this.lastOperandValue = Math.Sqrt((double)this.lastOperandValue);
        }

        public void ApplySqrToOperand()
        {
            this.lastOperandValue *= this.lastOperandValue;
        }

        public void ReinitializeCalculatorWithResult()
        {
            var oldResult = this.ExpressionValue;
            this.ClearMemory();
            this.SetLastOperandValue(oldResult);
        }

        private void FlushLastOperand()
        {
            if (this.lastOperationSign == OperatorType.Plus
                || this.lastOperationSign == OperatorType.Minus)
            {
                this.fullSum += this.lastMultiplicationResult;
                this.lastMultiplicationResult = 0;
            }

            switch (this.lastOperationSign)
            {
                case OperatorType.NotDefined:
                case OperatorType.Plus:
                    this.lastMultiplicationResult = this.lastOperandValue;
                    break;
                case OperatorType.Minus:
                    this.lastMultiplicationResult = -this.lastOperandValue;
                    break;
                case OperatorType.Multiplication:
                    this.lastMultiplicationResult *= this.lastOperandValue;
                    break;
                case OperatorType.Division:
                    this.lastMultiplicationResult /= this.lastOperandValue;
                    break;
                default:
                    throw new NotImplementedException(
                        "Program does not know how to handle operation!");
            }

            this.expression += this.GetOperationSign(this.lastOperationSign) +
                " " + this.lastOperandValue.ToString() + " ";
        }

        private string GetOperationSign(OperatorType operation)
        {
            switch (operation)
            {
                case OperatorType.Plus:
                    return "+";
                case OperatorType.Minus:
                    return "-";
                case OperatorType.Multiplication:
                    return "*";
                case OperatorType.Division:
                    return "/";
                default:
                    return "";
            }
        }
    }
}