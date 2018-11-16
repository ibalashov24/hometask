namespace MyNUnit.TestProjects
{
    using System;
    using System.IO;

    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        private FileStream log;

        public Test()
        {
            this.log = new FileStream(
                Path.Combine(Path.GetTempPath(), "MyNUnitFailInBeforeClass.tst"),
                FileMode.Append, FileAccess.Write, FileShare.Read);
        }

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
        public void BadBeforeClassMethod()
        {
            throw new AggregateException("Before Class method execution failed");
        }

        [Test]
        public void TestWhichMustWorkGood()
        {
            var a = 5 + 5 * 5;
            ++a;

            this.log.WriteByte(0);
        }

        [Test]
        public void TestWhichMustWorkBad()
        {
            var b = 6 + 6 * 6;
            --b;

            this.log.WriteByte(0);
            this.log.Flush();

            Assert.Fail("It failed :(");
        }

        [After]
        public void MethodWhichShouldNotBeCalled()
        {
            this.log.WriteByte(0);
            this.log.Flush();
        }

        [AfterClass]
        public void AnotherMethodWhichShouldNotBeCalled()
        {
            this.log.WriteByte(0);
            this.log.Flush();
        }
    }
}