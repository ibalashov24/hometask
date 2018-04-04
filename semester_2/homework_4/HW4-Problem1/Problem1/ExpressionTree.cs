namespace Tree
{
    using System;

    /// <summary>
    /// Implements the parse tree
    /// </summary>
    public class ExpressionTree : IExpressionTree
    {
        /// <summary>
        /// Reference to the root of the tree
        /// </summary>
        private Vertex.ExpressionTreeVertex root;

        /// <summary>
        /// Service variable containing value of the tree
        /// </summary>
        private double? calculatedValue;

        /// <summary>
        /// Initializes new instance of ExpressionTree
        /// </summary>
        /// <param name="expression">Expression to parse</param>
        public ExpressionTree(string expression)
        {
            var parser = new ExpressionStringParser(expression);
            this.root = parser.Root;
            this.EvaluateTree();
        }

        /// <summary>
        /// Returns result of the expression tree calculation
        /// </summary>
        public double Value => this.calculatedValue ?? 0;

        /// <summary>
        /// Returns string representation in tree form
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => this.root.ToString();

        /// <summary>
        /// Returns infix (1 + 2 * 3) representation of the tree
        /// </summary>
        /// <returns>Infix representaion</returns>
        public string GetInfixRepresentation() => this.root.GetInfixRepresentation();

        /// <summary>
        /// Calculates value of the expression parse tree
        /// </summary>
        private void EvaluateTree() => this.calculatedValue = this.root.Handle();
    }
}