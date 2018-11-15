namespace MyNUnit.TestTools
{
    using System;

    public class AssertFailedException : Exception
    {
        public AssertFailedException(string assertMessage, string userMessage)
            : base(userMessage)
        {
            this.AssertMessage = assertMessage;
        }

        public string AssertMessage { get; }
    }
}
