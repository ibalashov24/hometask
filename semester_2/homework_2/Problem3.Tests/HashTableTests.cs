namespace Problem3.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // I will use default hash function everywhere 
    // (hash functions will be tested separately)
    [TestClass]
    public class HashTableTests
    {
        // Smoke tests
        [TestMethod]
        public void AddingElementUsingMethodMustWork()
        {
            var table = new HashTableStuff.HashTable<string, object>();

            for (int i = 0; i < 10; ++i)
            {
                table.Add((i + 100500).ToString(), new object());
            }
        }

        [TestMethod]
        public void AddingElementUsingIndexerMustWork()
        {
            var table = new HashTableStuff.HashTable<string, object>();

            for (int i = 0; i < 10; ++i)
            {
                table[(i + 100500).ToString()] = new object();
            }
        }

        [TestMethod]
        public void ElementCheckMustWorkCorrectlyOnExistingElement()
        {
            var table = new HashTableStuff.HashTable<string, object>();
            table["testproba"] = new object();

            Assert.IsTrue(table.IsInTable("testproba"));
        }

        [TestMethod]
        public void FillFactorMustBeZeroInEmptyTableAndPositiveInNonEmpty()
        {
            var table = new HashTableStuff.HashTable<string, object>();
            Assert.AreEqual(0.0, table.FillFactor);

            table.Add("testproba555spbu", new object());
        }

        [TestMethod]
        public void ElementEraseMustWorkOnSingleElement()
        {
            var table = new HashTableStuff.HashTable<string, object>();
            table["testproba"] = new object();

            table.Erase("testproba");

            const double epsilon = 1e-5;
            Assert.IsTrue(table.FillFactor < epsilon);
        }

        // Regular tests
        [TestMethod]
        public void AddingAndErasingMultipleElementsMustWork()
        {
            var table = new HashTableStuff.HashTable<string, int>();
            for (int i = -10000; i <= 10000; ++i)
            {
                table.Add(i.ToString(), i - 100);
            }

            for (int i = -10000; i <= 10000; ++i)
            {
                Assert.IsTrue(table.IsInTable(i.ToString()));

                table.Erase(i.ToString());
            }

            Assert.AreEqual(0, table.FillFactor, 1e-5);
        }

        [TestMethod]
        public void TwoElementsWithTheSameHashShouldExistSeparately()
        {
            var table = new HashTableStuff.HashTable<string, int>(HashTableStuff.StringHashFunctions.SimpleHash);

            table.Add("+-", 100);
            table.Add("X", 200);

            // In order to demonstate that tests are correct
            Assert.AreEqual(
                HashTableStuff.StringHashFunctions.SimpleHash("+-"),
                HashTableStuff.StringHashFunctions.SimpleHash("X"));

            Assert.IsTrue(table.IsInTable("+-"), "`+-` does not exists");
            Assert.IsTrue(table.IsInTable("X"), "`X` does not exists");

            Assert.AreEqual(100, table["+-"], "`+-` has invalid value");
            Assert.AreEqual(200, table["X"], "`X` has invalid value");
        }

        [TestMethod]
        public void ErasingTwoElementsWithTheSameHashMustWork()
        {
            var table = new HashTableStuff.HashTable<string, int>(HashTableStuff.StringHashFunctions.SimpleHash);

            table.Add("+-", 100);
            table.Add("X", 200);

            // In order to demonstate that tests are correct
            Assert.AreEqual(
                HashTableStuff.StringHashFunctions.SimpleHash("+-"),
                HashTableStuff.StringHashFunctions.SimpleHash("X"));

            const double epsilon = 1e-5;

            table.Erase("+-");
            Assert.IsTrue(table.FillFactor > epsilon);

            table.Erase("X");
            Assert.IsTrue(table.FillFactor < epsilon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void GettingNonexistentElementShouldCauseException()
        {
            var table = new HashTableStuff.HashTable<string, object>();
	        var t = table["nonexistent"];
        }

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ErasingNonexistentElementShouldCauseException()
        {
            var table = new HashTableStuff.HashTable<string, object>();
			
            table.Erase("blahblah");
        }

		[TestMethod]
		public void AddingExistentElementUsingMethodMustReplaceTheValue()
        {
            var table = new HashTableStuff.HashTable<string, object>();

            table.Add("testproba", 100);
            table.Add("testproba", 200);

            Assert.AreEqual(200, table["testproba"]);
        }
    }
}