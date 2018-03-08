namespace Tree.Vertex
{
    using System;

    /// <summary>
    /// Implements operator -
    /// </summary>
    internal class OperationMinus : OperationVertex
    {
        public OperationMinus(
            ExpressionTreeVertex leftSon,
            ExpressionTreeVertex rightSon) : base(leftSon, rightSon) { }

        /// <summary>
        /// Calculates value of this vertex
        /// </summary>
        /// <returns>Value</returns>
        public override double Handle()
        {
            try
            {
                this.Value =  this.leftSon.Handle() - this.rightSon.Handle();
            }
            catch (NullReferenceException e)
            {
                throw new ArgumentException("Tree is inconsistent!!!", e);
            }

            return this.Value;
        }

        /// <summary>
        /// Converts operator to its string representation
        /// </summary>
        /// <returns>String representation: (operator-sign operand1 operand2)</returns>
        public override string ToString()
        {
            return "(-" + base.ToString();
        }

        public override string GetInfixRepresentation()
        {
            return base.GetInfixRepresentation('-');
        }
    }
}