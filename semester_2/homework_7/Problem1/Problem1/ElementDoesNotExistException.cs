using System;

namespace ListStuff.Exception
{
    /// <summary>
    /// This exception will be thrown if the requested item is not in list
    /// </summary>
    public class ElementDoesNotExistException : System.Exception
    {
        /// <summary>
        /// Initializes new instance of ElementDoesNotExistException
        /// </summary>
        /// <param name="message">Message describing problem</param>
        public ElementDoesNotExistException(string message) : base(message)
        {
        }
    }
}