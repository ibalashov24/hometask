namespace MyNUnit.TestProjects
{
    using System;
    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        private int afterExcecutionCount = 0;

        [Before]
        public void AfterMethod()
        {
            if (this.afterExcecutionCount == 1)
            {
                throw new AggregateException("BeforeMethod failed");
            }

            ++this.afterExcecutionCount;
        }


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
    }
}
