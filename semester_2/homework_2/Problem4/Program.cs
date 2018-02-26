namespace Problem4
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter numeric expression (with spaces!!!):");
            var expression = Console.ReadLine();

            var calculator = new StackCalculatorStuff.StackCalculator(
                new StackStuff.Stack<double>(), expression);

            var result = calculator.CalculateResult();

            Console.WriteLine($"Result == {result}");
        }
    }
}