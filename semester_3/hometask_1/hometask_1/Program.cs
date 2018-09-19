namespace Program
{
    using System;
    using System.Threading;

    public static class Program
    { 
        public static void Main()
        {
            var usualLazy = LazyStuff.LazyFactory<int>.CreateLazy(TestFunction);
            var parallelLazy = LazyStuff.LazyFactory<int>.CreateParallelLazy(TestFunction);

            for (int i = 0; i < 5; ++i)
            {
                Console.Write("Usual lazy: ");
                Console.WriteLine(usualLazy.Get());
            }

            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(() =>
                {
                    Console.Write("Parallel lazy: ");
                    Console.WriteLine(parallelLazy.Get());
                });
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }
        }

        private static int TestFunction()
        {
            return 42;
        }
    }
}