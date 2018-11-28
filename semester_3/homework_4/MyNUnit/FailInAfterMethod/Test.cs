namespace MyNUnit.TestProjects
{
    using System;
    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        private static int AfterExcecutionCount = 0;
        private static object LockObject = new object();

        [Test]
        public void FirstCorrectTest()
        {
            var a = 5 + 5 * 5;
            ++a;
        }

        [Test]
        public void SecondCorrectTest()
        {
            var b = 6 + 6 * 6;
            --b;
        }

        [Test]
        public void ThirdCorrectTest()
        {
            var c = 7 + 7 * 7;
            ++c;
        }

        [Test]
        public void FourthCorrectTest()
        {
            var d = 8 + 8 * 8;
            --d;
        }

        [After]
        public void AfterMethod()
        {
            lock (LockObject)
            {
                if (AfterExcecutionCount == 2)
                {
                    throw new AggregateException("AfterMethod failed");
                }

                ++AfterExcecutionCount;
            }            
        }
    }
}
