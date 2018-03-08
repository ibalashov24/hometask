namespace Tree
{
    using System;

    public interface IExpressionTree
    {
        /// <summary>
        /// Returns result of the expression tree calculation
        /// </summary>
        double Value { get; }

        /// <summary>
        /// Returns string representation in tree form
        /// </summary>
        /// <returns>String representation</returns>
        string ToString();

        /// <summary>
        /// Returns infix (1 + 2 * 3) representation of the tree
        /// </summary>
        /// <returns>Infix representaion</returns>
        string GetInfixRepresentation();
    }
}