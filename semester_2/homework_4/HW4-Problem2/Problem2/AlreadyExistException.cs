using System;

namespace ListStuff.Exception
{
    /// <summary>
    /// This exception will be thrown 
    /// if the inserted item is already in the list
    /// </summary>
    public class AlreadyExistException : System.Exception
    {
        /// <summary>
        /// Initializes new instance of AlreadyExistsException
        /// </summary>
        /// <param name="message">Message describing problem</param>
        public AlreadyExistException(string message) : base(message)
        { }
    }
}