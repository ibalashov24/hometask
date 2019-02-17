namespace MyNUnit.TestTools
{
    /// <summary>
    /// Contains method for testing purposes
    /// </summary>
    public static class Assert
    {
        /// <summary>
        /// Throwing exception if given objects are not equal
        /// </summary>
        /// <typeparam name="T">Type of objects</typeparam>
        /// <param name="expected">Expected value</param>
        /// <param name="actual">Actual value</param>
        /// <param name="message">Custom message to send if check is failed</param>
        public static void AreEqual<T>(
            T expected, 
            T actual, 
            string message = "")
        {
            if (!actual.Equals(expected))
            {
                throw new AssertFailedException(
                    "Actual value is not the same as expected",
                    message);
            }
        }

        /// <summary>
        /// Throwing exception if given string are not equal
        /// </summary>
        /// <typeparam name="T">Type of objects</typeparam>
        /// <param name="expected">Expected value</param>
        /// <param name="actual">Actual value</param>
        /// <param name="message">Custom message to send if check is failed</param>
        public static void AreEqual(
            string expected, 
            string actual, 
            string message = "")
        {
            if (actual != expected)
            {
                throw new AssertFailedException(
                    "Actual value is not the same as expected",
                    message);
            }
        }

        /// <summary>
        /// Throwing exception that means that test is failed
        /// </summary>
        /// <param name="message">Custom message to send with exception</param>
        public static void Fail(
            string message = "")
        {
            throw new AssertFailedException(
                "Assert.Fail was called",
                message);
        }
    }
}
