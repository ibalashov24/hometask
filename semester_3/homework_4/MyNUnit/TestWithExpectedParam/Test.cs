namespace MyNUnit.TestProjects
{
    using System;
    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        [Test(Expected = typeof(AggregateException))]
        public void TestWhichMustWorkGood()
        {
            var a = 5 + 5 * 5;
            ++a;

            throw new AggregateException("Good test crashed");
        }

        [Test(Expected = typeof(AggregateException))]
        public void TestWhichMustWorkBad()
        {
            var b = 6 + 6 * 6;
            --b;

            // Bad test is not crashed
        }
    }
}
