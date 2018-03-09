namespace Tree
{
    using System;

    /// <summary>
    /// Interface of the parse tree
    /// </summary>
    public interface IExpressionTree
    {
        /// <summary>
        /// Returns result of the expression tree calculation
        /// </summary>
        double Value { get; }
        
        /// <summary>
        /// Returns infix (1 + 2 * 3) representation of the tree
        /// </summary>
        /// <returns>Infix representaion</returns>
        string GetInfixRepresentation();
    }
}