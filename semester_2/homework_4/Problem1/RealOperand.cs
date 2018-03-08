namespace Tree.Vertex
{
    /// <summary>
    /// Implements constant operand which has real type
    /// </summary>
    internal class RealOperand : ExpressionTreeVertex
    {
        public RealOperand(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Returns value of operand
        /// </summary>
        /// <returns>Double value</returns>
        public override double Handle()
        {
            return this.Value;
        }

        /// <summary>
        /// Converts operand to its string representation
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }

        public override string GetInfixRepresentation()
        {
            return this.ToString();
        }
    }
}