namespace MyNUnit.TestProjects
{
    using System;
    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        [BeforeClass]
        public int GoodBeforeClassMethod()
        {
            var result = 0;
            for (int i = 0; i <= 5; ++i)
            {
                result += i;
            }
            return result;
        }

        [BeforeClass]
        public void SecondGoodMethod()
        {
            int b = 5 * 5;
            b += 3;
        }

        [Test]
        public void TestWhichMustWorkGood()
        {
            var a = 5 + 5 * 5;
            ++a;
        }

        [Test]
        public void TestWhichMustWorkBad()
        {
            var b = 6 + 6 * 6;
            --b;

            Assert.Fail("It failed :(");
        }

        [Test]
        public void TestWhichAlsoMustWorkGood()
        {
            var a = 10 + 5 * 6;
            ++a;
        }

        [AfterClass]
        public void AnotherMethodWhichShouldNotBeCalled()
        {
            throw new AggregateException("Error in AfterClass method");
        }
    }
}
