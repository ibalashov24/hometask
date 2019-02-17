namespace MyNUnit.TestProjects
{
    using System;
    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        [Test]
        public void TestWhichMustWorkGood()
        {
            var a = 5 + 5 * 5;
            ++a;
        }

        [Test(IgnoreReason = "Testing")]
        public void FirstTestWhichShouldBeIgnored()
        {
            var b = 6 + 6 * 6;
            --b;
        }

        [Test(IgnoreReason = "Testing")]
        public void SecondTestWhichShouldBeIgnored()
        {
            Assert.Fail("It failed :(");
        }

        [Test(Expected=typeof(AggregateException), IgnoreReason = "Testing")]
        public void ThirdTestWhichShouldBeIgnored()
        {
            var c = 7 + 7 * 7;
            ++c;
        }
    }
}
