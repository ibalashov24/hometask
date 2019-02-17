namespace MyNUnit.TestProjects
{
    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        [Test]
        public void AreEqualForStringsShouldPass()
        {
            var a = "testtest";
            Assert.AreEqual("testtest", a);
        }

        [Test]
        public void AreEqualForStringsShouldNotPass()
        {
            var a = "testtesttest";
            Assert.AreEqual("testtest", a);
        }

        [Test]
        public void AreEqualForObjectsShouldPass()
        {
            var a = 48;
            Assert.AreEqual(48, a);
        }

        [Test]
        public void AreEqualForObjectsShouldNotPass()
        {
            var a = 42;
            Assert.AreEqual(48, a);
        }
    }
}
