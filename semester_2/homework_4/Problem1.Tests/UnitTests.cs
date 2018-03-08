using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problem1.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DividingByZeroShouldBeCathed()
        {
            var tree = new Tree.ExpressionTree("(/ 1 0)");
            var treeValue = tree.Value;
        }

    }
}