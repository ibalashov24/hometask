using System;

namespace Fibonacci
{
    class FibonacciCalculator
    {
        public static void Main()
        {
            Console.Write("Enter Fibonacci number position (>= 1): ");
            var position = int.Parse(Console.ReadLine());

            var result = GetFibonacciNumber(position);

            Console.WriteLine("Fibonacci[{0}] == {1}", position, result);
        }

        private static int GetFibonacciNumber(int position)
        {
            int previousNumber = 1;
            int currentNumber = 1;

            for (int i = 2; i < position; ++i)
            {
                currentNumber += previousNumber;
                previousNumber = currentNumber - previousNumber;
            }

            return currentNumber;
        }

    }
}