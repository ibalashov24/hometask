﻿namespace Calculator
{
    using System;

    /// <summary>
    /// All available operations in the calculator
    /// </summary>
    public enum OperatorType
    {
        NotDefined,
        Plus,
        Minus,
        Multiplication,
        Division
    }

    /// <summary>
    /// Implements simple logic for visual calculator
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Gets the value of the
        /// last operand (it must contain a real number)
        /// Empty by default
        /// </summary>
        double LastOperand { get; }

        /// <summary>
        /// Returns the value of the entire expression
        /// </summary>
        double ExpressionValue { get; }

        /// <summary>
        /// Returns expression in string format
        /// </summary>
        string Expression { get; }

        /// <summary>
        /// Sets the value of the last operand
        /// </summary>
        /// <param name="value"></param>
        void SetLastOperandValue(double value);

        /// <summary>
        /// Set next operation type
        /// </summary>
        /// <remarks>
        ///  Keep in mind that the first operand in 
        ///  the expression is always (hidden) zero
        /// </remarks>
        /// <param name="nextOperator">Next operator</param>
        void SetNextOperator(OperatorType nextOperator);

        /// <summary>
        /// Adds last operand to the expression
        /// </summary>
        /// <remarks>
        /// It will flush default (zero) operand 
        /// if it is not set
        /// Keep in mind that the first operand in 
        /// the expression is always (hidden) zero
        /// </remarks>
        void FlushOperand();

        /// <summary>
        /// Clears expression in memory
        /// </summary>
        void ClearMemory();

        /// <summary>
        /// Negates current operand
        /// </summary>
        void NegateCurrentOperand();

        /// <summary>
        /// Makes operand --> 1 / operand
        /// </summary>
        void PutOperandIntoDenominator();

        /// <summary>
        /// Last operand := sqrt(last operand)
        /// </summary>
        void ApplySqrtToOperand();

        /// <summary>
        /// Last operand :+ [last operand]^2
        /// </summary>
        void ApplySqrToOperand();

        /// <summary>
        /// Cleans calculator memory and 
        /// initializes it with old result
        /// </summary>
        void ReinitializeCalculatorWithResult();
    }
}