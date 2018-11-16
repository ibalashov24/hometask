namespace MyNUnit.TestTools
{
    public static class Assert
    {
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

        public static void Fail(
            string message = "")
        {
            throw new AssertFailedException(
                "Assert.Fail was called",
                message);
        }
    }
}
