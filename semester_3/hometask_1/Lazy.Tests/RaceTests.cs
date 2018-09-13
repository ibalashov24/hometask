using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class RaceTests
    {
        [TestMethod]
        public void LazyParallelShouldNotAllowRaces()
        {
            var callCount = 0;
            var lazy = LazyStuff.LazyFactory<int>.CreateParallelLazy(() =>
            {
                callCount++;
                return 137;
            });

            var threads = new Thread[1000];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < 100; ++j)
                    {
                        Assert.AreEqual(137, lazy.Get(), "Wrong return value");
                    }
                });
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            for (int i = 0; i < 10; ++i)
            {
                lazy.Get();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Assert.AreEqual(1, callCount, "Supplier was called more than once!");
        }
    }
}
