namespace MyNUnit.TestProjects
{
    using System.IO;

    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        private static int sharedVariable = 0;

        [BeforeClass]
        public static void FirstBeforeClass()
        {
            sharedVariable += 5 + 5 * 5; // 30
            ++sharedVariable; // 31
        }

        [BeforeClass]
        public static void SecondBeforeClass()
        {
            sharedVariable += 6 + 6 * 6; // 73
            --sharedVariable; // 72
        }

        // 72 after BeforeClass section

        [Before]
        public void FirstBefore()
        {
            ++sharedVariable;
        }

        [Before]
        public void SecondBefore()
        {
            sharedVariable += 2;
        }

        // +3 after Before section

        [Test]
        public void Test1()
        {
            sharedVariable *= 29;
        }

        // 2175 after first test
        // 2171 after first After section execution

        [Test]
        public void Test2()
        {
            sharedVariable *= 29;
        }

        // 63046 after second test
        // 63042 after second After section execution

        [After]
        public void FirstAfter()
        {
            sharedVariable -= 7;
        }

        [After]
        public void SecondAfter()
        {
            sharedVariable += 3;
        }

        // -4 after After section

        [AfterClass]
        public static void FirstAfterClass()
        {
            sharedVariable /= 1000;

            if (sharedVariable == 945)
            {
                SaveResultFile(945);
            }
        }

        // 63 after first AfterClass method

        [AfterClass]
        public static void SecondAfterClass()
        {
            sharedVariable *= 15;

            if (sharedVariable == 945)
            {
                SaveResultFile(945);
            }
        }

        // 945 after second AfterClass method

        private static void SaveResultFile(int result)
        {
            var file = 
                File.Create(Path.Combine(Path.GetTempPath(), "MyNUnitTestBeforeAfter.tst"));

            file.WriteByte((byte)result);
            file.Flush();

            file.Close();
        }
    }
}
