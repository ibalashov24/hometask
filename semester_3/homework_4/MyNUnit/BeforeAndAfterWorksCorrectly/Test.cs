namespace MyNUnit.TestProjects
{
    using System.IO;

    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        private int sharedVariable = 0;

        [BeforeClass]
        public void FirstBeforeClass()
        {
            this.sharedVariable += 5 + 5 * 5; // 30
            ++this.sharedVariable; // 31
        }

        [BeforeClass]
        public void SecondBeforeClass()
        {
            this.sharedVariable += 6 + 6 * 6; // 73
            --this.sharedVariable; // 72
        }

        // 72 after BeforeClass section

        [Before]
        public void FirstBefore()
        {
            ++this.sharedVariable;
        }

        [Before]
        public void SecondBefore()
        {
            this.sharedVariable += 2;
        }

        // +3 after Before section

        [Test]
        public void Test1()
        {
            this.sharedVariable *= 29;
        }

        // 2175 after first test
        // 2171 after first After section execution

        [Test]
        public void Test2()
        {
            this.sharedVariable *= 29;
        }

        // 63046 after second test
        // 63042 after second After section execution

        [After]
        public void FirstAfter()
        {
            this.sharedVariable -= 7;
        }

        [After]
        public void SecondAfter()
        {
            this.sharedVariable += 3;
        }

        // -4 after After section

        [AfterClass]
        public void FirtsAfterClass()
        {
            this.sharedVariable /= 1000;

            if (this.sharedVariable == 945)
            {
                this.SaveResultFile(945);
            }
        }

        // 63 after first AfterClass method

        [AfterClass]
        public void SecondAfterClass()
        {
            this.sharedVariable *= 15;

            if (this.sharedVariable == 945)
            {
                this.SaveResultFile(945);
            }
        }

        // 945 after second AfterClass method

        private void SaveResultFile(int result)
        {
            var file = 
                File.Create(Path.Combine(Path.GetTempPath(), "MyNUnitTestBeforeAfter.tst"));

            file.WriteByte((byte)result);
            file.Flush();

            file.Close();
        }
    }
}
