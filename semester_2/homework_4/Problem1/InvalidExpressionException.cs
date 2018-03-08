namespace Tree.Exception
{
    using System;

    public class InvalidExpressionException : Exception
    {
        /// <summary>
        /// Position of expression string where mistake was found
        /// Or -1 if mistake is genereal
        /// </summary>
        public int ErrorPosition
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes new instance of InvalidExpressionException
        /// </summary>
        /// <param name="message">Error message</param>
        public InvalidExpressionException(string message) : base(message)
        {
            // Mistake is general
            this.ErrorPosition = -1;
        }

        /// <summary>
        /// Initializes new instance of InvalidExpressionException
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="errorPosition">Position of expression string where
        /// mistake was found</param>
        public InvalidExpressionException(
            string message, 
            int errorPosition) : base(message)
        {
            this.ErrorPosition = errorPosition;
        }
    }
}