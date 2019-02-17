using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SimpleTests
    {
        [TestMethod]
        public void LazyShouldWorkOnSimpleTest()
        {
            var lazy = LazyStuff.LazyFactory<int>.CreateLazy(() => { return 48; });

            Assert.AreEqual(48, lazy.Get());
        }

        [TestMethod]
        public void LazyParallelShouldWorkOnSimpleTest()
        {
            var lazy = LazyStuff.LazyFactory<int>.CreateParallelLazy(() => { return 242342; });

            Assert.AreEqual(242342, lazy.Get());
        }

        [TestMethod]
        public void LazyShouldWorkOnMultipleCalls()
        {
            var lazy = LazyStuff.LazyFactory<string>.CreateLazy(() => { return "Hello world!!!"; });

            for (int i = 0; i < 100; ++i)
            {
                Assert.AreEqual("Hello world!!!", lazy.Get());
            }
        }

        [TestMethod]
        public void LazyParallelShouldWorkOnMultipleCalls()
        {
            var lazy = LazyStuff.LazyFactory<string>.CreateParallelLazy(() => { return "Hello world!!!"; });

            for (int i = 0; i < 100; ++i)
            {
                Assert.AreEqual("Hello world!!!", lazy.Get());
            }
        }

        [TestMethod]
        public void LazyShouldWorkCorrectlyWhenSupplierReturnsZero()
        {
            var lazy = LazyStuff.LazyFactory<object>.CreateLazy(() => { return null; });

            Assert.IsNull(lazy.Get());
        }

        [TestMethod]
        public void LazyParallelShouldWorkCorrectlyWhenSupplierReturnsZero()
        {
            var lazy = LazyStuff.LazyFactory<object>.CreateParallelLazy(() => { return null; });

            Assert.IsNull(lazy.Get());
        }

        [TestMethod]
        public void LazyShouldReturnSameObjectAfterEveryCall()
        {
            var lazy = LazyStuff.LazyFactory<int[]>.CreateLazy(()
                => { return new int[] { 5, 4, 3, 2, 1 }; });

            var firstGetResult = lazy.Get();
            for (int i = 0; i < 100; ++i)
            {
                Assert.AreEqual(firstGetResult, lazy.Get());
            }
        }

        [TestMethod]
        public void LazyParallelShouldReturnSameObjectAfterEveryCall()
        {
            var lazy = LazyStuff.LazyFactory<int[]>.CreateParallelLazy(()
                => { return new int[] { 5, 4, 3, 2, 1 }; });

            var firstGetResult = lazy.Get();
            for (int i = 0; i < 100; ++i)
            {
                Assert.AreEqual(firstGetResult, lazy.Get());
            }
        }

        [TestMethod]
        public void LazyShouldCallSupplierOnlyOneTime()
        {
            var callCount = 0;

            var lazy = LazyStuff.LazyFactory<object>.CreateLazy(() =>
            {
                callCount++;
                return null;
            });

            for (int i = 0; i < 100; ++i)
            {
                lazy.Get();
            }

            Assert.AreEqual(1, callCount);
        }

        [TestMethod]
        public void LazyParallelShouldCallSupplierOnlyOneTime()
        {
            var callCount = 0;

            var lazy = LazyStuff.LazyFactory<object>.CreateParallelLazy(() =>
            {
                callCount++;
                return null;
            });

            for (int i = 0; i < 100; ++i)
            {
                lazy.Get();
            }

            Assert.AreEqual(1, callCount);
        }
    }
}