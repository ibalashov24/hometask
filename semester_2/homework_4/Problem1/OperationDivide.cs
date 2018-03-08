namespace Tree.Vertex
{
    using System;

    /// <summary>
    /// Implements operator /
    /// </summary>
    internal class OperatorDivide : OperatorVertex
    {
        public OperatorDivide(
            ExpressionTreeVertex leftSon,
            ExpressionTreeVertex rightSon) : base(leftSon, rightSon) { }

        /// <summary>
        /// Calculates value of this vertex
        /// </summary>
        /// <returns>Value</returns>
        public override double Handle()
        {
            const double epsilon = 1e-5;

            double rightSonValue;
            double leftSonValue;

            try
            {
                rightSonValue = this.rightSon.Handle();
                leftSonValue = this.leftSon.Handle();
            }
            catch (NullReferenceException e)
            {
                throw new ArgumentException("Tree is inconsistent!!!", e);
            }

            if (Math.Abs(rightSonValue) < epsilon)
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

        public override string GetInfixRepresentation()
        {
            return base.GetInfixRepresentation('/');
        }
    }
}