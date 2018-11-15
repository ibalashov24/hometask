using System;

namespace MyNUnit.Runner
{
    public class TestResult
    {
        public TestResult(string testName, bool isPassed, string message)
        {
            this.TestName = testName;
            this.IsPassed = isPassed;
            this.Message = message;
        }

        public string TestName { get; }

        public bool IsPassed { get; }

        public string Message { get; }
    }
}
