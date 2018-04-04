namespace StackCalculatorStuff
{
    using System;

    public class StackCalculator
    {
        private const double epsilon = double.Epsilon * 1000;

        StackStuff.IStack<double> stack;
        string[] lexems;

        // The expression to be calculated
        public string Expression
        {
            get
            {
                var result = "";
                foreach (string lexem in this.lexems)
                {
                    result += lexem + ' ';
                }

                return result;
            }

            set => this.lexems = ParseExpression(value);
        }

        public StackCalculator(StackStuff.IStack<double> stack, string expression)
        {
            this.stack = stack;
            this.lexems = ParseExpression(expression);
        }

        // Calculates given expression (in postfix notation)
        public double CalculateResult()
        {
            foreach (var currentLexem in lexems)
            {
                var isNumber = int.TryParse(currentLexem, out int number);
                if (isNumber)
                {
                    stack.Push((double)number);
                }
                else if (currentLexem.Length == 1 && "+-*/".IndexOf(currentLexem) != -1)
                {
                    double lastNumber;
                    double beforeLastNumber;

                    try
                    {
                        lastNumber = stack.Pop();
                        beforeLastNumber = stack.Pop();
                    }
                    catch (InvalidOperationException)
                    {
                        throw new ArgumentException
                            ("Not enough operands for operation!");
                    }

                    switch (currentLexem)
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
                            if (Math.Abs(lastNumber) < epsilon)
                            {
                                throw new ArgumentException(
                                    "Dividing by zero is not permitted!");
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