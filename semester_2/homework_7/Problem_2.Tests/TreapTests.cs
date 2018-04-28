using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problem_2.Tests
{
    [TestClass]
    public class TreapTests
    {
        CustomSet.Treap<int> treap;

        [TestInitialize]
        public void InitTreap()
        {
            this.treap = new CustomSet.Treap<int>();
            this.treap.RandomSeed = 1234567;
        }

        [TestMethod]
        public void SimpleTestShouldWork()
        {
            this.treap.Add(15);
            this.treap.Add(30);
            this.treap.Add(45);

            Assert.AreEqual(3, this.treap.Count);
            Assert.IsTrue(this.treap.Contains(15));
            Assert.IsTrue(this.treap.Contains(30));
            Assert.IsTrue(this.treap.Contains(45));
            Assert.IsFalse(this.treap.Contains(65));
        }

        [TestMethod]
        public void SimpleRemoveShouldWork()
        {
            this.treap.Add(15);
            this.treap.Add(30);
            this.treap.Add(45);

            Assert.IsTrue(this.treap.Remove(30));
            Assert.IsTrue(this.treap.Contains(15));
            Assert.IsTrue(this.treap.Contains(45));
            Assert.IsFalse(this.treap.Contains(30));
        }

        [TestMethod]
        public void AddingOfTheLowestShouldWork()
        {
            this.treap.Add(15);
            this.treap.Add(30);
            this.treap.Add(-2453563);

            Assert.AreEqual(3, this.treap.Count);
            Assert.IsTrue(this.treap.Contains(15));
            Assert.IsTrue(this.treap.Contains(30));
            Assert.IsTrue(this.treap.Contains(-2453563));
        }

        [TestMethod]
        public void AddingOfExistingValueShouldNotChangeAnything()
        {
            this.treap.Add(15);
            Assert.AreEqual(1, this.treap.Count);
            this.treap.Add(15);
            Assert.AreEqual(1, this.treap.Count);
            Assert.IsTrue(this.treap.Contains(15));
        }
        
        [TestMethod]
        public void RemovingOfNonExistingElementShouldWork()
        {
            this.treap.Add(15);
            this.treap.Add(-20);
            this.treap.Remove(3242523);

            Assert.AreEqual(2, this.treap.Count);
            Assert.IsTrue(this.treap.Contains(15));
            Assert.IsTrue(this.treap.Contains(-20));
            Assert.IsFalse(this.treap.Contains(3242523));
        }

        [TestMethod]
        public void RedeletingElementShouldNotChangeAnything()
        {
            this.treap.Add(15);
            this.treap.Add(20);
            this.treap.Add(1252);

            Assert.IsTrue(this.treap.Remove(20));
            Assert.IsFalse(this.treap.Remove(20));

            Assert.AreEqual(2, this.treap.Count);
            Assert.IsTrue(this.treap.Contains(15));
            Assert.IsTrue(this.treap.Contains(1252));
            Assert.IsFalse(this.treap.Contains(20));
        }

        [TestMethod]
        public void RemovingFromEmptyTreapShouldWorkProperly()
        {
            this.treap.Remove(3443);
            Assert.AreEqual(0, this.treap.Count);
        }

        [TestMethod]
        public void RemovingOfTheRootShouldWorkProperly()
        {
            this.treap.Add(15);
            this.treap.Add(-20);
            this.treap.Add(25);
            this.treap.Add(86);
            this.treap.Add(0);

            this.treap.Remove(86);

            Assert.AreEqual(4, this.treap.Count);
            Assert.IsTrue(this.treap.Contains(15));
            Assert.IsTrue(this.treap.Contains(-20));
            Assert.IsTrue(this.treap.Contains(25));
            Assert.IsTrue(this.treap.Contains(0));
            Assert.IsFalse(this.treap.Contains(86));
        }

        [TestMethod]
        public void SimpleForeachShouldWorkProperly()
        {
            var test = new int[5] { 5, 4, 2, 1, 3 };
            foreach (var item in test)
            {
                this.treap.Add(item);
            }

            Array.Sort(test);
            var i = 0;
            foreach (var item in this.treap)
            {
                Assert.AreEqual(test[i], item);
                i++;
            }
        }

        [TestMethod]
        public void SimpleForeachTest()
        {
            for (int i = 1; i <= 5; ++i)
            {
                this.treap.Add(i);
            }

            int counter = 1;
            foreach (var item in this.treap)
            {
                Assert.AreEqual(counter, item);
                counter++;
            }
        }

        [TestMethod]
        public void ForeachShouldReturnElementsInRightOrder()
        {
            this.treap.Add(34);
            this.treap.Add(-5423);
            this.treap.Add(9);
            this.treap.Add(0);
            this.treap.Add(15);

            var testArray = new int[5];
            this.treap.CopyTo(testArray, 5);

            CollectionAssert.AreEqual(
                new List<int> { -5423, 0, 9, 15, 34 },
                testArray
            );
        }

        [TestMethod]
        public void ForeachOverBigTreapShouldWorkAndShouldDoItFast()
        {
            for (int i = 1000000; i >= 0; --i)
            {
                this.treap.Add(5 * i);
            }

            Assert.AreEqual(1000001, this.treap.Count);

            var test = new List<int>();
            foreach (var item in this.treap)
            {
                test.Add(item);
            }

            for (int i = 0; i <= 1000000; ++i)
            {
                Assert.AreEqual(5 * i, test[i]);
            }
        }

        [TestMethod]
        public void ForeachOverEmptyTreapShouldWorkProperly()
        {
            foreach (var item in this.treap)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void RepeatedForeachShouldWorkProperly()
        {
            for (int i = 5; i >= 1; --i)
            {
                this.treap.Add(i);
            }

            var counter = 1;
            foreach (var item in this.treap)
            {
                Assert.AreEqual(counter, item);
                counter++;
            }

            counter = 1;
            foreach (var item in this.treap)
            {
                Assert.AreEqual(counter, item);
                counter++;
            }
        }

        [TestMethod]
        public void ForeachShouldWorkCorrectlyAfterRepeatedAdd()
        {
            this.treap.Add(5);
            this.treap.Add(6);
            this.treap.Add(6);

            var testResult = new int[2];
            var i = 0;
            foreach (var item in this.treap)
            {
                testResult[i] = item;
                i++;
            }

            Assert.AreEqual(2, i);
            Assert.AreEqual(5, testResult[0]);
            Assert.AreEqual(6, testResult[1]);
        }

        [TestMethod]
        public void ForeachShouldWorkProperlyAfterRepeatedRemove()
        {
            this.treap.Add(15);
            this.treap.Add(20);
            this.treap.Add(1252);

            this.treap.Remove(20);
            this.treap.Remove(20);

            var testResult = new int[2];
            var i = 0;
            foreach (var item in this.treap)
            {
                testResult[i] = item;
                i++;
            }

            Assert.AreEqual(2, i);
            Assert.AreEqual(15, testResult[0]);
            Assert.AreEqual(1252, testResult[1]);
        }

        [TestMethod]
        public void ContainsFromEmptyTreapShouldWork()
        {
            for (int i = -50; i < 50; ++i)
            {
                Assert.IsFalse(
                    this.treap.Contains((54254242 * i) % 372 + 34));
            }
        }

        [TestMethod]
        public void AnyTreapShouldNotContainNull()
        {
            var newTreap = new CustomSet.Treap<object>();
            Assert.IsFalse(newTreap.Contains(null));
        }

        [TestMethod]
        public void AddingNullShouldNotChangeAnything()
        {
            var newTreap = new CustomSet.Treap<object>();
            newTreap.Add(null);

            Assert.AreEqual(0, newTreap.Count);
            Assert.IsFalse(newTreap.Contains(null));
        }

        [TestMethod]
        public void RemovingNullShouldNotChangeAnything()
        {
            var newTreap = new CustomSet.Treap<string>();
            newTreap.Add("dfsdf");
            newTreap.Add("rwerewr");

            Assert.IsFalse(newTreap.Remove(null));
            Assert.AreEqual(2, newTreap.Count);
        }

        [TestMethod]
        public void ClearMethodShouldWorkProperly()
        {
            for (int i = 0; i < 10; ++i)
            {
                this.treap.Add(i);
            }
            Assert.AreEqual(10, this.treap.Count);

            this.treap.Clear();
            Assert.AreEqual(0, this.treap.Count);
        }

        [TestMethod]
        public void ClearMethodShouldWorkForEmptyTreap()
        {
            this.treap.Clear();
            Assert.AreEqual(0, this.treap.Count);
        }

        [DataTestMethod]
        [DataRow(100)]
        [DataRow(50)]
        [DataRow(0)]
        public void CopyToMethodShouldWorkProperly(int upperBound)
        {
            for (int i = 0; i < upperBound; ++i)
            {
                this.treap.Add(i);
            }

            var testArray = new int[upperBound];
            this.treap.CopyTo(testArray, upperBound);

            Assert.AreEqual(upperBound, this.treap.Count);

            for (int i = 0; i < upperBound; ++i)
            {
                Assert.AreEqual(i, testArray[i]);
            }
        }

        [DataTestMethod]
        [DataRow(new int[0])]
        [DataRow(new int[5] { 1, 2, 3, 4, 5 })]
        [DataRow(new int[5] { 1, 2, 3, 3, 3 })]
        public void ExceptWithMethodShouldWorkProperly(IEnumerable<int> input)
        {
            for (int i = 1; i <= 5; ++i)
            {
                this.treap.Add(i);
            }

            this.treap.ExceptWith(input);

            var shouldBeInTreap = new bool[5];
            for (int i = 0; i < 5; ++i)
            {
                shouldBeInTreap[i] = true;
            }

            foreach (var item in input)
            {
                shouldBeInTreap[item - 1] = false;
            }

            var counter = 0;
            foreach (var item in this.treap)
            {   
                Assert.IsTrue(
                    shouldBeInTreap[counter] && this.treap.Contains(counter + 1) ||
                    !shouldBeInTreap[counter] && !this.treap.Contains(counter + 1));
            }
        }


        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5})]
        [DataRow(new int[] { 1, 2, 3, 9 }, new int[] { 1, 2, 3 })]
        [DataRow(new int[0], new int[0])]
        [DataRow(new int[] {10, 11, 12}, new int[0])]
        public void IntersectWithMethodShouldWorkProperly(
            IEnumerable<int> input,
            int[] expectedResult)
        {
            for (int i = 1; i <= 5; ++i)
            {
                this.treap.Add(i);
            }

            this.treap.IntersectWith(input);

            var result = new int[this.treap.Count];
            this.treap.CopyTo(result, this.treap.Count);

            CollectionAssert.AreEqual(result, expectedResult);
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, true)]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, false)]
        [DataRow(new int[] { 3, 4 }, true)]
        [DataRow(new int[0], true)]
        [DataRow(new int[] {7, 8, 9}, false)]
        public void IsSupersetOfMethodShouldWorkProperly(
            IEnumerable<int> input,
            bool expectedResult)
        {
            for (int i = 1; i <= 5; ++i)
            {
                this.treap.Add(i);
            }

            Assert.AreEqual(expectedResult,
                    this.treap.IsSupersetOf(input));
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, false)]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, false)]
        [DataRow(new int[] { 3, 4 }, true)]
        [DataRow(new int[0], true)]
        [DataRow(new int[] { 7, 8, 9 }, false)]
        public void IsProperSupersetOfMethodShouldWorkProperly(
            IEnumerable<int> input,
            bool expectedResult)
        {
            for (int i = 1; i <= 5; ++i)
            {
                this.treap.Add(i);
            }

            Assert.AreEqual(
                expectedResult, 
                this.treap.IsProperSupersetOf(input));
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, false)]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, true)]
        [DataRow(new int[] { 3, 4 }, false)]
        [DataRow(new int[] { -1, -2, 5, 4, 3, 2, 1 }, true)]
        [DataRow(new int[0], false)]
        [DataRow(new int[] { 4, 5, 6, 7, 8 }, false)]
        public void IsProperSubsetOfMethodShouldWorkProperly(
            IEnumerable<int> input,
            bool expectedResult)
        {
            for (int i = 1; i <= 5; ++i)
            {
                this.treap.Add(i);
            }

            Assert.AreEqual(
                expectedResult, 
                this.treap.IsProperSubsetOf(input));
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, true)]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, true)]
        [DataRow(new int[] { 3, 4 }, false)]
        [DataRow(new int[] { -1, -2, 5, 4, 3, 2, 1 }, true)]
        [DataRow(new int[0], false)]
        [DataRow(new int[] { 4, 5, 6, 7, 8 }, false)]
        public void IsSubsetOfMethodShouldWorkProperly(
            IEnumerable<int> input,
            bool expectedResult)
        {
            for (int i = 1; i <= 5; ++i)
            {
                this.treap.Add(i);
            }

            Assert.AreEqual(
                expectedResult, 
                this.treap.IsSubsetOf(input));
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, true)]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, true)]
        [DataRow(new int[] { 3, 4 }, true)]
        [DataRow(new int[] { -1, -2, 5, 4, 3, 2, 1 }, true)]
        [DataRow(new int[0], false)]
        [DataRow(new int[] { -1, 6, 7 }, false)]
        public void OverlapsMethodShouldWorkProperly(
            IEnumerable<int> input,
            bool expectedResult)
        {
            for (int i = 1; i <= 5; ++i)
            {
                this.treap.Add(i);
            }

            Assert.AreEqual(
                expectedResult, 
                this.treap.Overlaps(input));
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2 }, new int[] { 1, 2 }, true)]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 },
                new int[] { 3, 4 }, false)]
        [DataRow(new int[0], new int[0], true)]
        [DataRow(new int[0], new int[] { -1, 6, 7 }, false)]
        public void SetEqualsShouldWorkProperly(
            IEnumerable<int> treapInput,
            IEnumerable<int> toCompare,
            bool expectedResult)
        {
            foreach (var item in treapInput)
            {
                this.treap.Add(item);
            }

            Assert.AreEqual(
                expectedResult, this.treap.SetEquals(toCompare));
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2 }, new int[] { 3, 4, 5 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 
                new int[] { 6, 7 })]
        [DataRow(new int[0], new int[] { 1, 2, 3, 4, 5 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, new int[0])]
        public void SymmetricDifferenceMethodShouldWorkProperly(
            IEnumerable<int> input,
            int[] expectedResult)
        {
            for (int i = 1; i <= 5; ++i)
            {
                this.treap.Add(i);
            }

            this.treap.SymmetricExceptWith(input);

            var resultArray = new int[this.treap.Count];
            this.treap.CopyTo(resultArray, this.treap.Count);

            CollectionAssert.AreEqual(resultArray, expectedResult);
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2 },
                new int[] {1, 2, 3, 4, 5})]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 
                new int[] { 1, 2, 3, 4, 5, 6, 7 })]
        [DataRow(new int[] {-1, 0, 6, 7},
                new int[] { -1, 0, 1, 2, 3, 4, 5, 6, 7})]
        [DataRow(new int[0],
                new int[] { 1, 2, 3, 4, 5 })]
        public void UnionMethodShouldWorkCorrectly(
            IEnumerable<int> input,
            int[] expectedResult)
        {
            for (int i = 1; i <= 5; ++i)
            {
                this.treap.Add(i);
            }

            this.treap.UnionWith(input);

            var resultArray = new int[this.treap.Count];
            this.treap.CopyTo(resultArray, this.treap.Count);

            CollectionAssert.AreEqual(resultArray, expectedResult);
        }

        [TestMethod]
        public void UnionEmptyWithEmptyShouldWork()
        {
            this.treap.UnionWith(new int[0]);

            Assert.AreEqual(0, this.treap.Count);
        }

        [TestMethod]
        public void UnionEmptyWithNonEmptyShouldWork()
        {
            this.treap.UnionWith(new int[] { 1, 2, 3, 4, 5 });

            var i = 1;
            foreach (var item in this.treap)
            {   
                Assert.AreEqual(i, item);
                i++;
            }
        }
    }
}
