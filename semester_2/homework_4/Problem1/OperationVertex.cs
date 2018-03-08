namespace Tree.Vertex
{
    /// <summary>
    /// Implements operators in parse tree (+, - etc)
    /// </summary>
    internal abstract class OperationVertex : ExpressionTreeVertex
    {
        /// <summary>
        /// Left and right branches of the tree
        /// </summary>
        protected ExpressionTreeVertex leftSon;
        protected ExpressionTreeVertex rightSon;

        /// <summary>
        /// Initializes new instance of OperationVertex
        /// </summary>
        /// <param name="left">Reference to the left branch</param>
        /// <param name="right">Reference to the right branch</param>
        public OperationVertex(
            ExpressionTreeVertex left,
            ExpressionTreeVertex right)
        {
            this.leftSon = left;
            this.rightSon = right;
        }

        /// <summary>
        /// Converts operand to its string representation
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return $" {this.leftSon.ToString()} {this.rightSon.ToString()})";
        }

        /// <summary>
        /// Converts operand to its string representation in infix form
        /// </summary>
        /// <returns>Infix representation</returns>
        protected string GetInfixRepresentation(char operationSign)
        {
            return $"({this.leftSon.GetInfixRepresentation()} " +
                $"{operationSign} " +
                $"{this.rightSon.GetInfixRepresentation()})";
        }
    }
}