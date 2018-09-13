using System;
using System.Collections.Generic;

namespace Problem1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter count of primes to print: ");
            var primeCount = 0;
            if (!int.TryParse(Console.ReadLine(), out primeCount))
            {
                Console.Write("Incorrect count");
                return;
            }

            Console.WriteLine("Primes:");

            var printedPrimeCount = 0;
            foreach (var prime in NextPrime())
            {
                if (printedPrimeCount >= primeCount)
                {
                    return;
                }

                Console.WriteLine(prime);
                ++printedPrimeCount;
            }
        }

        /// <summary>
        /// Yields next prime number in each call
        /// </summary>
        /// <returns>Next prime</returns>
        public static IEnumerable<int> NextPrime()
        {
            /// <summary>
            /// Yields next prime number in each call
            /// First number == 1
            /// </summary>
            /// <returns>Next integer</returns>
            IEnumerable<int> NextInteger()
            {
                var currentInteger = 1;
                while (true)
                {
                    yield return currentInteger;
                    ++currentInteger;
                }
            }

            /// <summary>
            /// Returns true if given number is a prime
            /// </summary>
            /// <param name="number">Number to check</param>
            /// <returns>True if prime</returns>
            bool IsPrimeNumber(int number)
            {
                if (number == 1)
                {
                    return false;
                }

                for (int i = 2; i <= Math.Sqrt(number); ++i)
                {
                    if (number % i == 0)
                    {
                        return false;
                    }
                }

                return true;
            }

            foreach (var primeNumber in
                    Utility.ListUtilities.Filter(NextInteger(), IsPrimeNumber))
            {
                yield return primeNumber;
            }
        }
    }
}
