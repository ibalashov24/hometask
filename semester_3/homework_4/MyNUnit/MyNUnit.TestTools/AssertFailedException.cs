namespace MyNUnit.TestTools
{
    using System;

    /// <summary>
    /// Exception that means that testing is failed
    /// </summary>
    public class AssertFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertFailedException"/> class.
        /// </summary>
        /// <param name="assertMessage">Description of test fail</param>
        /// <param name="userMessage">Custom message</param>
        public AssertFailedException(string assertMessage, string userMessage)
            : base(userMessage)
        {
            this.AssertMessage = assertMessage;
        }

        /// <summary>
        /// Gets description of test fail
        /// </summary>
        public string AssertMessage { get; }
    }
}
