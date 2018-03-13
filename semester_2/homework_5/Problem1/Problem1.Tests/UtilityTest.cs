using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problem1.Tests
{
    [TestClass]
    public class UtilityTest
    {
        private void CheckFilter<T>(List<T> list, Predicate<T> predicate)
        {
            var filteredList = Utility.ListUtilities.Filter(list,
                predicate);
            list.RemoveAll(x => !predicate(x));

            CollectionAssert.AreEqual(list, filteredList);
        }

        // Smoke tests
        [TestMethod]
        public void SimpleTestForFilter()
        {
            Predicate<int> predicate = element => element % 15 == 0;
            var list = new List<int> { 1, 2, 3, 4, 5, 15 };

            CheckFilter(list, predicate);
        }

        [TestMethod]
        public void SimpleTestForFold()
        {
            Func<int, int, int> accumulator = (acc, next) => acc + next;

            var list = new List<int> { 1, 2, 3, 4, 5, 15 };

            var systemResult = list.Aggregate(15, accumulator);
            var ourResult = Utility.ListUtilities.Fold<int, int>(list, 15, accumulator);

            Assert.AreEqual(systemResult, ourResult);
        }

        [TestMethod]
        public void SimpleTestForMap()
        {
            Func<int, int> handler = element => 5 * element;

            var list = new List<int> { 1, 5, 6, 15, 100 };

            var modifiedList = new List<int>();
            foreach (var element in list)
            {
                modifiedList.Add(handler(element));
            }

            list = Utility.ListUtilities.Map<int>(list, handler);

            CollectionAssert.AreEqual(list, modifiedList);
        }

        // Regular tests
        [TestMethod]
        public void ReferenceTypeMap()
        {
            Func<string, string> handler = element => '5' + element;

            var list = new List<string> { "test", "proba", "12345", "erere", "lalala" };

            var modifiedList = new List<string>();
            foreach (var element in list)
            {
                modifiedList.Add(handler(element));
            }

            list = Utility.ListUtilities.Map<string>(list, handler);

            CollectionAssert.AreEqual(list, modifiedList);
        }

        [TestMethod]
        public void ReferenceTypeFold()
        {
            Func<string, string, string> accumulator = (acc, next) => acc + next;

            var list = new List<string> { "lalal", "kekek", "12345", "gfsgfdg", "\n\nfff"};

            var systemResult = list.Aggregate("15", accumulator);
            var ourResult = Utility.ListUtilities.Fold<string, string>(list, "15", accumulator);

            Assert.AreEqual(systemResult, ourResult);
        }

        [TestMethod]
        public void ReferenceTypeFilter()
        {
            Predicate<string> predicate = element => element.Length > 5;
            var list = new List<string> { "lalal", "kekek", "12345", "gfsgfdg", "\n\nfff" };

            CheckFilter(list, predicate);
        }

        [TestMethod]
        public void DifferentTypesInFold()
        {
            Func<string, int, string> accumulator = (acc, next) => acc + next.ToString();

            var list = new List<int> { 1, 2, 3, 4, 5, 100500, -134325 };

            var systemResult = list.Aggregate("15", accumulator);
            var ourResult = Utility.ListUtilities.Fold<int, string>(list, "15", accumulator);

            Assert.AreEqual(systemResult, ourResult);
        }

        [TestMethod]
        public void EmptyListAsAResultInFilter()
        {
            Predicate<int> predicate = element => element % 15 == 0;
            var list = new List<int> { 1, 2, 3, 4, 5 };

            CheckFilter(list, predicate);
        }

        [TestMethod]
        public void EmptyInputInFilter()
        {
            Predicate<int> predicate = element => element % 10 == 0;
            var list = new List<int>();

            CheckFilter(list, predicate);
        }
    }
}
