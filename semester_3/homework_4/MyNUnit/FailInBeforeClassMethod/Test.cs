namespace MyNUnit.TestProjects
{
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

        [Test]
        public void TestWhichMustWorkBad()
        {
            var b = 6 + 6 * 6;
            --b;

            Assert.Fail("It failed :(");
        }
    }
}
