namespace HashTableStuff
{
    using System.Security.Cryptography;
    using System.Text;

    public delegate int HashFunctionType<T>(T element);

    /// <summary>
    /// Provides several hash functions for strings
    /// </summary>
    public static class StringHashFunctions
    {
        /// <summary>
        /// The hash function, which summarizes the codes of all characters in the string
        /// </summary>
        /// <param name="inputString">The string from which the hash will be calculated</param>
        /// <returns>
        /// Integer hash code
        /// </returns>
        public static int SimpleHash(string inputString)
        {
            int result = 0;
            foreach (char symbol in inputString)
            {
                result += (int)symbol;
            }

            return result;
        }

        /// <summary>
        /// The hash function, which calculates something like polynomial hash
        /// (assuming all characters have their ASCII positions)
        /// </summary>
        /// <param name="inputString">The string from which the hash will be calculated</param>
        /// <returns>
        /// Integer hash code
        /// </returns>
        public static int PolynomialHash(string inputString)
        {
            // ASCII symbols count
            const int approximateAlphabetSize = 256;

            int factor = 1;
            int result = 0;
            foreach (char symbol in inputString)
            {
                result += (int)symbol * factor;
                factor *= approximateAlphabetSize;
            }

            return result;
        }

        /// <summary>
        /// The hash function, which calculates integer hash code of the string using md5 algorithm
        /// </summary>
        /// <param name="inputString">The string from which the hash will be calculated</param>
        /// <returns>Integer hash code</returns>
        public static int MD5Hash(string inputString)
        {
            var md5 = new MD5Cng();
            var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            int factor = 1;
            int result = 0;
            foreach (char currentByte in hashedBytes)
            {
                result += currentByte * factor;
                factor *= 2;
            }

            return result;
        }
    }
}