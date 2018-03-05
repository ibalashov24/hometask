using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problem3.Tests
{
    [TestClass]
    public class ListTests
    {
        // Smoke tests
        [TestMethod]
        public void AddingAndErasingOneElementMustNotFail()
        {
            var list = new ListStuff.List<object>();

            list.Insert(new object(), 0);
            list.Erase(0);
        }

        [TestMethod]
        public void SizeOfTheListMustBeActual()
        {
            var list = new ListStuff.List<int>();
            for (int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(i, list.Size(), $"Incorrect size: {list.Size()} ({i} expected)");
                list.Insert(i, 0);
            }
            Assert.AreEqual(10, list.Size(), $"Incorrect size: {list.Size()} ({10} expected)");
        }
        
        [TestMethod]
        public void IsEmptyMustReturnActualResult()
        {
            var list = new ListStuff.List<int>();
            Assert.IsTrue(list.IsEmpty(), "List must be empty");
            for (int i = 0; i < 10; ++i)
            {
                list.Insert(i, 0);
                Assert.IsFalse(list.IsEmpty(), $"List must not be empty (actual size == {list.Size()}");
            }
        }

        [TestMethod]
        public void AddingAndErasingOfMultipleElementsMustWork()
        {
            var list = new ListStuff.List<int>();
            for (int i = -1500; i <= 1500; ++i)
            {
                list.Insert(i, 0);
            }

            for (int i = 1500; i >= -1500; --i)
            {
                list.Erase(0);
            }

            Assert.IsTrue(list.IsEmpty(), "List must be empty");
        }

        // Main tests
        [TestMethod]
        public void RandomInsertionMustWorkCorrectly()
        {
            var list = new ListStuff.List<int>();
            for (int i = 1; i <= 1000; i += 2)
            {
                list.Insert(i, list.Size());
            }

            for (int i = 2; i <= 1000; i += 2)
            {
                list.Insert(i, i - 1);
            }

            int counter = 1;
            foreach (int element in list)
            {
                Assert.AreEqual(counter, element);
                ++counter;
            }
        }

        public void CheckDeleting(ListStuff.List<int> list, int position = 0)
        {
            try
            {
                list.Erase(position);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void DeletingFromEmptyListMustCauseException()
        {
            var list = new ListStuff.List<int>();
            CheckDeleting(list, 0);
        }

        [TestMethod]
        public void DeletingFromInvalidPositionMustCauseException()
        {
            var list = new ListStuff.List<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Insert(i, i);
            }

            for (int i = -100; i < 0; i++)
            {
                CheckDeleting(list, i);
            }

            for (int i = 10; i < 100; i++)
            {
                CheckDeleting(list, i);
            }
        }

        [TestMethod]
        public void CleanListMustWork()
        {
            var list = new ListStuff.List<int>();
            for (int i = 0; i < 10; ++i)
            {
                list.Insert(i, 0);
            }

            list.Clean();

            Assert.IsTrue(list.IsEmpty());
        }

        [TestMethod]
        public void ForeachCycleShouldNotPerformAnyIterationsOnAnEmptyList()
        {
            var list = new ListStuff.List<object>();

            bool isIteration = false;
            foreach (var element in list)
            {
                isIteration = true;
            }

            Assert.IsFalse(isIteration);
        }

        [TestMethod]
        public void ListIteratorShouldPerformCorrectCountOfIterations()
        {
            var list = new ListStuff.List<int>();

            for (int i = 0; i < 100; ++i)
            {
                list.Insert(i, i);
            }

            int counter = 0;
            foreach (var element in list)
            {
                ++counter;
            }

            Assert.AreEqual(list.Size(), counter);
        }

        public void ListIteratorShouldWorkCorrectlyOnTheSecondRun()
        {
            var list = new ListStuff.List<int>();

            for (int i = 0; i < 100; ++i)
            {
                list.Insert(i, 0);
            }

            foreach (var element in list) ;

            for (int i = 0; i < 10; ++i)
            {
                list.Erase(5 * i);
            }

            for (int i = 0; i < 5; ++i)
            {
                list.Insert(i, 3 * i);
            }

            int counter = 0;
            foreach (var element in list)
            {
                ++counter;
            }

            Assert.AreEqual(list.Size(), counter);
        }
    }
}