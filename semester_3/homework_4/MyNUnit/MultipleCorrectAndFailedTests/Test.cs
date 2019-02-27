namespace MyNUnit.TestProjects
{
    using System;
    using MyNUnit.TestTools;

    [TestClass]
    public class Test
    {
        // Must succeed
        [Test]
        public void TestWhichMustWorkGood()
        {
            var a = 5 + 5 * 5;
            ++a;
        }

        // Must fail
        [Test]
        public void TestWhichMustWorkBad()
        {
            var b = 6 + 6 * 6;
            --b;

            Assert.Fail("It failed :(");
        }

        // Must fail
        [Test]
        public void DivisionByZero()
        {
            var a = 5 * 34;
            var b = 0;
            var c = a / b;
        }

        // Must succeed
        [Test]
        public void LongCycle()
        {
            var sum = 0;
            for (int i = 1; i <= 15000; ++i)
            {
                sum += i;
            }

            Assert.AreEqual((1 + 15000) * 15000 / 2, sum);
        }

        // Must succeed 
        [Test]
        public void Recursion()
        {
            int Fibonacci(int n)
            {
                if (n <= 2)
                {
                    return 1;
                }

                return Fibonacci(n - 1) + Fibonacci(n - 2);
            }

            Assert.AreEqual(6765, Fibonacci(20));
        }

        // Must fail
        [Test]
        public void ThrowingException()
        {
            throw new AggregateException("Something went wrong");
        }
    }
}
