namespace Problem4
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter numeric expression (with spaces!!!):");
            var expression = Console.ReadLine();

            var result = CalculateResult(expression);

            Console.WriteLine($"Result == {result}");
        }

        // Calculates given expression (in postfix notation)
        private static double CalculateResult(string expression)
        {
            // Could be replaced with 'new StackStuff.ArrayStack<double>'
            StackStuff.IStack<double> stack = new StackStuff.ArrayStack<double>();

            var parsedExpression = ParseExpression(expression);

            foreach (var lexem in parsedExpression)
            {
                var isNumber = int.TryParse(lexem, out int number);
                if (isNumber)
                {
                    stack.Push((double)number);
                }
                else if (lexem.Length == 1 && "+-*/".IndexOf(lexem) != -1)
                {
                    var lastNumber = .0;
                    var beforeLastNumber = .0;

                    try
                    {
                        lastNumber = stack.Pop();
                        beforeLastNumber = stack.Pop();
                    }
                    catch (InvalidOperationException)
                    {
                        throw new ArgumentException("Not enough operands for operation!");
                    }

                    switch (lexem)
                    {
                        case "+":
                            stack.Push(lastNumber + beforeLastNumber);
                            break;

                        case "-":
                            stack.Push(beforeLastNumber - lastNumber);
                            break;

                        case "*":
                            stack.Push(lastNumber * beforeLastNumber);
                            break;

                        case "/":
                            if (lastNumber == 0)
                            {
                                throw new DivideByZeroException("Dividing by zero!");
                            }

                            stack.Push(beforeLastNumber / lastNumber);
                            break;
                    }
                }
                else
                {
                    throw new ArgumentException(
                        "Expression contains symbols which are not permitted");
                }
            }

            var result = stack.Pop();
            if (!stack.IsEmpty())
            {
                throw new ArgumentException("Too many numbers!");
            }

            return result;
        }

        // Splits expression into operands and operations
        private static string[] ParseExpression(string expression)
        {
            var result = expression.Split(' ');
            return result;
        }
    }
}