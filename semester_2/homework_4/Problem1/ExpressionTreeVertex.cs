namespace Tree.Vertex
{
    using System;

    public abstract class ExpressionTreeVertex : Object
    {
        /// <summary>
        /// Value of current vertex in the parse tree
        /// </summary>
        public double Value
        {
            get;
            protected set;
        }

        /// <summary>
        /// Proceeds calculations of current vertex
        /// </summary>
        /// <returns>Result of the calculation</returns>
        public abstract double Handle();

        /// <summary>
        /// Returns string representation of the tree in infix form
        /// </summary>
        /// <returns>String representation</returns>
        public abstract string GetInfixRepresentation();
    }
}