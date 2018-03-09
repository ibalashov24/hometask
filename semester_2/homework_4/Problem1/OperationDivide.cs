namespace Tree.Vertex
{
    using System;

    /// <summary>
    /// Implements operator /
    /// </summary>
    internal class OperationDivide : OperationVertex
    {
        public OperationDivide(
            ExpressionTreeVertex leftSon,
            ExpressionTreeVertex rightSon) : base(leftSon, rightSon) { }

        /// <summary>
        /// Calculates value of this vertex
        /// </summary>
        /// <returns>Value</returns>
        public override double Handle()
        {
            const double epsilon = 1e-5;
            if (Math.Abs(this.rightSon.Handle()) < epsilon)
            {
                throw new DivideByZeroException("Dividing by zero!!");
            }

            this.Value = this.leftSon.Handle() / this.rightSon.Handle();
            return this.Value;
        }

        /// <summary>
        /// Converts operator to its string representation
        /// </summary>
        /// <returns>String representation: (operator-sign operand1 operand2)</returns>
        public override string ToString()
        {
            return "(/" + base.ToString();
        }

        /// <summary>
        /// Converts operand to its string representation in infix form
        /// </summary>
        /// <returns>Infix representation</returns>
        public override string GetInfixRepresentation()
        {
            return base.GetInfixRepresentation('/');
        }
    }
}