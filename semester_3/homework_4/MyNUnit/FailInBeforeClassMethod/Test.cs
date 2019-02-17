namespace MyNUnit.TestProjects
{
    using System;
    using System.IO;

    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        private static FileStream log;

        public Test()
        {
            log = new FileStream(
                Path.Combine(Path.GetTempPath(), "MyNUnitFailInBeforeClass.tst"),
                FileMode.Append, FileAccess.Write, FileShare.Read);
        }

        [BeforeClass]
        public static int GoodBeforeClassMethod()
        {
            var result = 0;
            for (int i = 0; i <= 5; ++i)
            {
                result += i;
            }
            return result;
        }

        [BeforeClass]
        public static void BadBeforeClassMethod()
        {
            throw new AggregateException("Before Class method execution failed");
        }

        [Test]
        public void TestWhichMustWorkGood()
        {
            var a = 5 + 5 * 5;
            ++a;
                
            log.WriteByte(0);
            log.Flush();
        }

        [Test]
        public void TestWhichMustWorkBad()
        {
            var b = 6 + 6 * 6;
            --b;

            log.WriteByte(0);
            log.Flush();

            Assert.Fail("It failed :(");
        }

        [After]
        public void MethodWhichShouldNotBeCalled()
        {
            log.WriteByte(0);
            log.Flush();
        }

        [AfterClass]
        public static void AnotherMethodWhichShouldNotBeCalled()
        {
            log.WriteByte(0);
            log.Flush();
        }
    }
}
