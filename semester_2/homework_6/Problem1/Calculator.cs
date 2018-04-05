namespace Calculator
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements logic for visual calculator
    /// </summary>
    public class Calculator : ICalculator
    {
        /// <summary>
        /// Expression in formatted text form
        /// </summary>
        private string expression = string.Empty;

        /// <summary>
        /// The sum of all terms in the expression 
        /// (except the last one <see cref="lastMultiplicationResult"/>)
        /// </summary>
        private double fullSum;

        /// <summary>
        /// The value of the last factor (last term in the sum of terms)
        /// 1 + 3 * 5 + --> 10 * 45 <-- lastMultiplicationResult == 450
        /// </summary>
        private double lastMultiplicationResult;

        /// <summary>
        /// The value of the last operand
        /// </summary>
        private double lastOperandValue;

        /// <summary>
        /// The type of the last operation
        /// </summary>
        private OperatorType lastOperationSign
            = OperatorType.NotDefined;

        /// <summary>
        /// Returns an expression in a formatted form 
        /// (with the last operation sign)
        /// </summary>
        public string Expression
        {
            get
            {
                var returnValue = this.expression + 
                    this.GetOperationSign(this.lastOperationSign);
                return returnValue.Trim();
            }
            private set => this.expression = value;
        }

        /// <summary>
        /// Gets the value of the last operand
        /// </summary>
        public double LastOperand => this.lastOperandValue;

        /// <summary>
        /// Gets value of the whole expression
        /// </summary>
        public double ExpressionValue 
            => this.fullSum + this.lastMultiplicationResult;
        
        /// <summary>
        /// Sets the value of the last operands
        /// </summary>
        /// <param name="value"></param>
        public void SetLastOperandValue(double value) 
            => this.lastOperandValue = value;

        /// <summary>
        /// Sets the type of the next operator
        /// </summary>
        /// <param name="nextOperator">Operation type</param>
        public void SetNextOperator(OperatorType nextOperator)
        {
            if (nextOperator == OperatorType.NotDefined)
            {
                throw new InvalidOperationException(
                    "Operator must be defined");
            }

            this.lastOperationSign = nextOperator;
        }

        /// <summary>
        /// Adds last operand to the expression
        /// Attention: next operand value will be 0
        /// </summary>
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

        /// <summary>
        /// Leads calculator to the initial state
        /// </summary>
        public void ClearMemory()
        {
            this.expression = string.Empty;
            this.lastOperationSign = OperatorType.NotDefined;
            this.lastOperandValue = 0;

            this.fullSum = 0;
            this.lastMultiplicationResult = 0;
        }

        /// <summary>
        /// Applies the negation operation to the last operand
        /// </summary>
        public void NegateCurrentOperand() 
            => this.lastOperandValue = -this.lastOperandValue;

        /// <summary>
        /// Replaces operand X with operand 1/X
        /// </summary>
        public void PutOperandIntoDenominator()
            => this.lastOperandValue = 1 / this.lastOperandValue;

        /// <summary>
        /// Replaces the last operand with it's square root
        /// </summary>
        public void ApplySqrtToOperand()
            => this.lastOperandValue = Math.Sqrt((double)this.lastOperandValue);

        /// <summary>
        /// Replaces the last operand with it's square
        /// </summary>
        public void ApplySqrToOperand()
            => this.lastOperandValue *= this.lastOperandValue;

        /// <summary>
        /// Leads calculator to it's initial state 
        /// and initializes it with the value of the (old) expression
        /// </summary>
        public void ReinitializeCalculatorWithResult()
        {
            var oldResult = this.ExpressionValue;
            this.ClearMemory();
            this.SetLastOperandValue(oldResult);
        }

        /// <summary>
        /// Adds last operand to the expression
        /// </summary>
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

        /// <summary>
        /// Returns the string representation 
        /// of operations from OperatorType
        /// </summary>
        /// <seealso cref="OperatorType"/>
        /// <param name="operation">
        /// Operation, the sign of which must be obtained
        /// </param>
        /// <returns>Operation sign</returns>
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
                    return string.Empty;
            }
        }
    }
}