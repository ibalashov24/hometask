namespace MyNUnit.TestProjects
{
    public class NotATestClass
    {
        public NotATestClass()
        {
            var a = 5 * 5 + 5;
            ++a;
        }

        public int AMethod()
        {
            var b = 6 * 6 + 6;
            --b;

            return b;
        }
    }
}
