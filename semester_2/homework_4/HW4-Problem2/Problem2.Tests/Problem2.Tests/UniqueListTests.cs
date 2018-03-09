using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problem2.Tests
{
    [TestClass]
    class UniqueListTests
    {
        private ListStuff.UniqueList<string> list;

        [TestInitialize]
        public void InitUniqueList()
        {
            var list = new ListStuff.UniqueList<string>();
        }

        [TestMethod]
        [ExpectedException(typeof(ListStuff.Exception.AlreadyExistException))]
        public void AddingNotUniqueElementShouldCauseException()
        {
            list.Insert("alalalaspbu", 0);
            list.Insert("12345", 1);
            list.Insert("abcdef", 0);
            list.Insert("", 2);

            list.Insert("abcdef", 2);
        }
    }
}
