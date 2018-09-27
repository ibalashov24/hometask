using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyThreadPool.Tests
{
    [TestClass]
    public class MainTests
    {
        [TestMethod]
        public void SimpleTestShouldWork()
        {
            var pool = new CustomThreading.MyThreadPool(10);
            var task = pool.AddTask(() => 5);

            Assert.AreEqual(5, task.Result);
        }

        [TestMethod]
        public void ShouldWorkCorrectlyWhenTaskCountMoreThanThreadCount()
        {
            var pool = new CustomThreading.MyThreadPool(3);
            var tasks = new CustomThreading.IMyTask<int>[10];
            for (int i = 0; i < 10; ++i)
            {
                var localI = i;
                tasks[i] = pool.AddTask(() =>
                {
                    System.Threading.Thread.Sleep(100);
                    return localI;
                });
            }

            for (int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(i, tasks[i].Result);
            }
        }

        [TestMethod]
        public void PoolShutdownShouldNotAffectPreviousTasks()
        {
            var pool = new CustomThreading.MyThreadPool(10);

            var tasks = new CustomThreading.IMyTask<int>[10];
            for (int i = 0; i < 10; ++i)
            {
                var localI = i;
                tasks[i] = pool.AddTask(() =>
                {
                    System.Threading.Thread.Sleep(100);
                    return localI;
                });
            }

            pool.Shutdown();

            for (int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(i, tasks[i].Result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void PoolShouldNotAcceptNewTasksAfterShutdown()
        {
            var pool = new CustomThreading.MyThreadPool(10);
            pool.AddTask(() => 5);

            pool.Shutdown();

            var tasks = new CustomThreading.IMyTask<int>[10];
            for (int i = 0; i < 10; ++i)
            {
                var localI = i;
                tasks[i] = pool.AddTask(() => localI);
            }

            for (int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(i, tasks[i].Result);
            }
        }

        [TestMethod]
        public void FreeThreadCountShouldBeEqualToMaxThreadCountInEmptyPool()
        {
            var pool = new CustomThreading.MyThreadPool(10);
            Assert.AreEqual(pool.MaxThreadCount, pool.AvailableThreadCount);
        }

        [TestMethod]
        public void FreeThreadCounterShouldWorkCorrectlyInNonEmptyPool()
        {
            var pool = new CustomThreading.MyThreadPool(10);
            var task = pool.AddTask(() =>
            {
                System.Threading.Thread.Sleep(50);
                return 5;
            });

            Assert.AreEqual(pool.MaxThreadCount - 1, pool.AvailableThreadCount);

            var t = task.Result;
            Assert.AreEqual(pool.MaxThreadCount, pool.AvailableThreadCount);
        }

        [TestMethod]
        public void FreeThreadCountShouldBeZeroWhilePoolIsFullyBusy()
        {
            var pool = new CustomThreading.MyThreadPool(5);

            for (int i = 0; i < 10; ++i)
            {
                pool.AddTask(() =>
                {
                    System.Threading.Thread.Sleep(50);
                    return 5;
                });
            }

            Assert.AreEqual(0, pool.AvailableThreadCount);
        }

        [TestMethod]
        public void ContinueWithShouldWorkCorrectlyOnSimpleTest()
        {
            var pool = new CustomThreading.MyThreadPool(5);
            var task = pool.AddTask(() => 5);

            var t = task.Result;

            var nestedTask = task.ContinueWith(five => five + 5);
            Assert.AreEqual(10, nestedTask.Result);
            Assert.AreEqual(5, task.Result, "Result of main task was corrupted!");
        }

        [TestMethod]
        public void PoolShouldProceedSeveralNestedTasksFromOneTaskCorrectly()
        {
            var pool = new CustomThreading.MyThreadPool(5);
            var mainTask = pool.AddTask(() => 5);

            var nestedTasks = new CustomThreading.IMyTask<int>[10];
            for (int i = 0; i < 10; ++i)
            {
                var localI = i;
                nestedTasks[i] = mainTask.ContinueWith(mainValue => mainValue + localI);
            }

            for (int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(5 + i, nestedTasks[i].Result);
            }
        }

        [TestMethod]
        public void NestedTaskShouldWorkCorrectlyWithLongMainTask()
        {
            var pool = new CustomThreading.MyThreadPool(5);

            var mainTask = pool.AddTask(() =>
            {
                System.Threading.Thread.Sleep(100);
                return 10;
            });
            var nestedTask = mainTask.ContinueWith(mainValue => mainValue + 15);

            Assert.AreEqual(25, nestedTask.Result);
        }
    }
}
