namespace MyNUnit.Runner
{
    /// <summary>
    /// Contains result of single test execution
    /// </summary>
    public class TestResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestResult"/> class.
        /// </summary>
        /// <param name="testName">Name of the test method</param>
        /// <param name="isPassed">True if test is passed</param>
        /// <param name="message">Message</param>
        public TestResult(string testName, bool isPassed, string message)
        {
            this.TestName = testName;
            this.IsPassed = isPassed;
            this.Message = message;
        }

        /// <summary>
        /// Gets name of the test method
        /// </summary>
        public string TestName { get; }

        /// <summary>
        /// Gets a value indicating whether test is passed
        /// </summary>
        public bool IsPassed { get; }

        /// <summary>
        /// Gets supporting message
        /// </summary>
        public string Message { get; }
    }
}
