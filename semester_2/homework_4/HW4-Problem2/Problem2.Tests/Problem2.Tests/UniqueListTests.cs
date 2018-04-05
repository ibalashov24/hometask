using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problem2.Tests
{
    [TestClass]
    public class UniqueListTests
    {
        private ListStuff.UniqueList<string> list;

        [TestInitialize]
        public void InitUniqueList()
        {
            this.list = new ListStuff.UniqueList<string>();
        }

        [TestMethod]
        public void SimpleTest()
        {
            this.list.Insert("1", 0);
            this.list.Erase(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ListStuff.Exception.AlreadyExistException))]
        public void AddingNotUniqueElementShouldCauseException()
        {
            this.list.Insert("spbu", 0);
            this.list.Insert("12345", 1);
            this.list.Insert("abcdef", 0);
            this.list.Insert("", 2);

            this.list.Insert("abcdef", 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ListStuff.Exception.ElementDoesNotExistException))]
        public void ErasingOfNonExistingElementShouldCauseException()
        {
            this.list.Insert("spbu", 0);

            this.list.Erase(5);
        }

        [TestMethod]
        public void AddingAndErasingManyUniqueElementsShouldWork()
        {
            for (int i = 0; i < 1000; ++i)
            {
                this.list.Insert((i * 3456 + 234).ToString(), i);
            }

            int counter = 0;
            foreach (var element in list)
            {
                Assert.AreEqual((counter * 3456 + 234).ToString(), element);
                ++counter;
            }

            for (int i = 0; i < 1000; ++i)
            {
                this.list.Erase(0);
            }
        }
    }
}
