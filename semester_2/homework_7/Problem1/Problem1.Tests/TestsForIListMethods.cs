using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problem1.Tests
{
    [TestClass]
    public class TestsForIListMethods
    {
        private ListStuff.List<string> list;

        [TestInitialize]
        public void InitializeList()
        {
            this.list = new ListStuff.List<string>();
        }

        // Contains() tests

        [TestMethod]
        public void AddedElementShouldBeInTheList()
        {
            for (int i = 0; i < 5; ++i)
            {
                this.list.Add((1234344 * i).ToString());
            }

            for (int i = 0; i < 5; ++i)
            {
                Assert.IsTrue(this.list.Contains((1234344 * i).ToString()));
            }
        }

        [TestMethod]
        public void NotAddedElementsShouldNotBeInTheList()
        {
            for (int i = 0; i < 5; ++i)
            {
                this.list.Add((1234344 * i).ToString());
            }

            for (int i = 0; i < 5; ++i)
            {
                Assert.IsFalse(this.list.Contains((1234 * i + 1).ToString()));
            }
        }

        [TestMethod]
        public void RemovedElementShouldNotBeInTheList()
        {
            this.list.Add("abacaba");
            Assert.IsTrue(this.list.Contains("abacaba"));

            this.list.RemoveAt(0);
            Assert.IsFalse(this.list.Contains("abacaba"));
        }

        // CopyTo() tests

        [TestMethod]
        public void CopyingToAnArrayInZeroPoistionShouldWork()
        {
            string[] destination = new string[5];

            for (int i = 0; i < 5; ++i)
            {
                this.list.Add((1234344 * i).ToString());
            }

            this.list.CopyTo(destination, 0);
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual((1234344 * i).ToString(), destination[5 - i - 1]);
            }
        }

        [TestMethod]
        public void CopyingToAnArrayInAnyPoistionShouldWork()
        {
            string[] destination = new string[10];
            for (int i = 0; i < 5; ++i)
            {
                destination[i] = "TEST";
            }

            for (int i = 0; i < 5; ++i)
            {
                this.list.Add((1234344 * i).ToString());
            }

            this.list.CopyTo(destination, 5);
            for (int i = 0; i < 5; ++i)
            {
                Assert.AreEqual((1234344 * i).ToString(), destination[10 - i - 1]);
            }

            for (int i = 0; i < 5; ++i)
            {
                Assert.AreEqual("TEST", destination[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CopyingToAnArraySmallerThanListSizeShouldCauseException()
        {
            string[] destination = new string[3];

            for (int i = 0; i < 5; ++i)
            {
                this.list.Add((1234344 * i).ToString());
            }

            this.list.CopyTo(destination, 0);
        }

        // IndexOf() tests

        [TestMethod]
        public void IndexOfShouldReturnCurrentIndexes()
        {
            for (int i = 0; i < 5; ++i)
            {
                this.list.Add((5555 * i).ToString());
            }

            for (int i = 0; i < 5; ++i)
            {
                Assert.AreEqual(
                    5 - i - 1,
                    this.list.IndexOf((5555 * i).ToString()));
            }
        }

        [TestMethod]
        public void IndexOfTheElementNotFromTheListShouldBeMinusOne()
        {
            for (int i = 0; i < 5; ++i)
            {
                this.list.Add((5555 * i).ToString());
            }

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(-1, this.list.IndexOf((i + 1).ToString()));
            }
        }

        // Remove() tests

        [TestMethod]
        public void RemovingExistingElementWithoutRepeatsShouldWork()
        {
            for (int i = 0; i < 10; ++i)
            {
                this.list.Add((9995555 * i).ToString());
            }

            this.list.Remove((9995555 * 3).ToString());
            this.list.Remove((9995555 * 7).ToString());

            Assert.AreEqual(this.list.Count, 8);
            Assert.IsFalse(this.list.Contains((9995555 * 3).ToString()));
            Assert.IsFalse(this.list.Contains((9995555 * 7).ToString()));
        }

        [TestMethod]
        public void RemovingExistingElementWithRepeatsShouldWork()
        {
            for (int i = 0; i < 5; ++i)
            {
                this.list.Add((9995555 * i).ToString());
            }
            this.list.Add((9995555 * 3).ToString());

            this.list.Remove((9995555 * 3).ToString());

            Assert.AreEqual(this.list.Count, 5);
            Assert.IsTrue(this.list.Contains((9995555 * 3).ToString()));
        }

        [TestMethod]
        public void RemovingNonExistionElementShouldReturnFalse()
        {
            for (int i = 0; i < 5; ++i)
            {
                this.list.Add((9995555 * i).ToString());
            }

            Assert.IsFalse(this.list.Remove("TEST"));
            Assert.AreEqual(5, this.list.Count);
        }

        [TestMethod]
        public void RemovingFromEmptyListShouldReturnFalse()
        {
            Assert.IsFalse(this.list.Remove("TEST"));
            Assert.AreEqual(0, this.list.Count);
        }

        // Indexer tests

        [TestMethod]
        public void AllElementShouldBeAccesibleWithAnIndexer()
        {
            for (int i = 0; i < 100; ++i)
            {
                this.list.Add((24234 * i).ToString());
            }

            for (int i = 0; i < 100; ++i)
            {
                Assert.AreEqual((24234 * i).ToString(), this.list[100 - i - 1]);
            }
        }

        [TestMethod]
        public void SettingElementsWithIndexerShouldWork()
        {
            for (int i = 0; i < 5; ++i)
            {
                this.list.Add("TEST");
            }

            this.list[3] = "TSET";
            this.list[0] = "TSET";

            Assert.AreEqual("TSET", this.list[3]);
            Assert.AreEqual("TSET", this.list[0]);
        }

        [DataRow(-1)]
        [DataRow(5)]
        [DataRow(1000)]
        [DataTestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerShouldCauseExceptionWhenGettingElementWithInvalidIndex(int index)
        {
            for (int i = 0; i < 5; ++i)
            {
                this.list.Add("TEST");
            }

            var returnValue = this.list[index];
        }

        [DataRow(-1)]
        [DataRow(5)]
        [DataRow(1000)]
        [DataTestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerShouldCauseExceptionWhenSettingElementWithInvalidIndex(int index)
        {
            this.list[index] = "TEST";
        }
    }
}