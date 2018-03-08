namespace Tree.Vertex
{
    /// <summary>
    /// Implements operators in parse tree (+, - etc)
    /// </summary>
    internal abstract class OperatorVertex : ExpressionTreeVertex
    {
        /// <summary>
        /// Left and right branches of the tree
        /// </summary>
        protected ExpressionTreeVertex leftSon;
        protected ExpressionTreeVertex rightSon;

        /// <summary>
        /// Initializes new instance of OperatorVertex
        /// </summary>
        /// <param name="left">Reference to the left branch</param>
        /// <param name="right">Reference to the right branch</param>
        public OperatorVertex(
            ExpressionTreeVertex left,
            ExpressionTreeVertex right)
        {
            this.leftSon = left;
            this.rightSon = right;
        }

        public override string ToString()
        {
            return $" {this.leftSon.ToString()} {this.rightSon.ToString()})";
        }

        protected string GetInfixRepresentation(char operationSign)
        {
            return $"({this.leftSon.GetInfixRepresentation()} " +
                $"{operationSign} " +
                $"{this.rightSon.GetInfixRepresentation()})";
        }
    }
}