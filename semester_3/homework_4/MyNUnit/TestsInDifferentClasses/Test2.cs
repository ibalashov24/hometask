namespace MyNUnit.TestProjects
{
    using MyNUnit.TestTools;

    [TestClass]
    public class Test2
    {
        [Test]
        public void TestInSecondClassWhichShouldWorkGood()
        {
            var a = 5 + 5 * 5;
            ++a;
        }

        [Test]
        public void TestInSecondClassWhichShouldWorkBad()
        {
            var b = 6 + 6 * 6;
            --b;

            Assert.Fail("It failed :(");
        }
    }
}
