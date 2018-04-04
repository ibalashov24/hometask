namespace Problem4
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter numeric expression (with spaces!!!):");
            var expression = Console.ReadLine();

            StackCalculatorStuff.StackCalculator calculator;

            Console.WriteLine("Enter 0 to use array-based stack " +
                "or any another string to use reference-based stack");
            var chosenStack = Console.ReadLine();
    
            if (chosenStack == "0")
            {
                Console.WriteLine("Using array-based stack");
                calculator = new StackCalculatorStuff.StackCalculator
                    (new StackStuff.ArrayStack<double>(), expression);
            }
            else
            {
                Console.WriteLine("Using reference-based stack");
                calculator = new StackCalculatorStuff.StackCalculator
                    (new StackStuff.Stack<double>(), expression);
            }
            
            var result = calculator.CalculateResult();

            Console.WriteLine($"Result == {result}");
        }
    }
}