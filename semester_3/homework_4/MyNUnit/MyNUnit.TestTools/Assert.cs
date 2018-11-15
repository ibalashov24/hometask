namespace MyNUnit.TestTools
{
    public static class Assert
    {
        static void AreEqual<T>(
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

        static void AreEqual(
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

        static void Fail(
            string message = "")
        {
            throw new AssertFailedException(
                "Assert.Fail was called",
                message);
        }

        static void IsTrue(
            bool condition, 
            string message)
        {
            if (!condition)
            {
                throw new AssertFailedException(
                "Condition is not true",
                message);
            }
        }
    }
}
