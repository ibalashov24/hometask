namespace Problem3.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class HashFunctionTests
    {
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Initializes AvailableHashList.csv with names of available hash functions
        /// </summary>
        [ClassInitialize]
        public static void InitializeHashFunctionList(TestContext context)
        {
            using (var hashNameFile = new StreamWriter("AvailableHashList.csv"))
            {
                foreach (var hashFunction in
                    typeof(HashTableStuff.StringHashFunctions).GetMethods(
                        System.Reflection.BindingFlags.Static |
                        System.Reflection.BindingFlags.Public))
                {
                    hashNameFile.WriteLine(hashFunction.Name);
                }
            }
        }

        // Smoke test for hash functions
        [DataTestMethod]
        [DeploymentItem("AvailableHashList.csv")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\AvailableHashList.csv",
            "AvailableHashList#csv",
             DataAccessMethod.Sequential)]
        public void AllHashFunctionShouldReturnIntegerHash()
        {
            var inputStrings = new string[] {
                "lalala",
                "4545",
                "%$%$%\n",
                ""};

            var hashFunctionInfo = typeof(HashTableStuff.StringHashFunctions).
                GetMethod((string)this.TestContext.DataRow[0]);
            if (hashFunctionInfo == null)
            {
                throw new ArgumentException("Invalid hash function name. Check AvailableHashList.csv");
            }

            var hashFunction = (HashTableStuff.HashFunctionType<string>)hashFunctionInfo.CreateDelegate(
                typeof(HashTableStuff.HashFunctionType<string>));
            
            foreach (var test in inputStrings)
            {
                try
                {
                    int hashValue = hashFunction(test);
                }
                catch
                {
                    Assert.Fail($"Hash function fails on test: {test}");
                }
            }
        }
    }
}