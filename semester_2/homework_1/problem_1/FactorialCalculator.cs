using System;

namespace Factorial
{
    class FactorialCalculator
    {
        public static void Main()
        {
            Console.Write("Enter positive integer: ");
            var n = int.Parse(Console.ReadLine());

            var result = 1;
            for (var i = 2; i <= n; ++i)
            {
                result *= i;
            }

            Console.WriteLine("{0}! == {1}", n, result);
        }
    }
}