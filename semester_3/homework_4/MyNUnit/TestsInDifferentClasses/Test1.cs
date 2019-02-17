namespace MyNUnit.TestProjects
{
    using MyNUnit.TestTools;

    [TestClass]
    public class Test1
    {
        [Test]
        public void TestInFirstClassWhichShouldWorkGood()
        {
            var a = 5 + 5 * 5;
            ++a;
        }

        [Test]
        public void TestInFirstClassWhichShouldWorkBad()
        {
            var b = 6 + 6 * 6;
            --b;

            Assert.Fail("It failed :(");
        }
    }
}
