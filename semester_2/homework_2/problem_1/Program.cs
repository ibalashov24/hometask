namespace Program
{
    using System;

    internal class Program
    {
        private static readonly Command[] Commands = new Command[]
        {
            new Command(CommandNumbers.Help, "print this help"),
            new Command(CommandNumbers.Push, "push element to the stack"),
            new Command(CommandNumbers.Pop, "get (and delete) top element from the set"),
            new Command(CommandNumbers.Exit, "exit the program")
        };

        private enum CommandNumbers
        {
            Exit,
            Help,
            Push,
            Pop
        }

        public static void Main(string[] args)
        {
            PrintHelp();

            var stack = new StackStuff.Stack<int>();

            while (true)
            {
                Console.Write("Enter command: ");
                var enteredCommand = int.Parse(Console.ReadLine());

                if (CommandNumbers.IsDefined(typeof(CommandNumbers), enteredCommand))
                {
                    switch ((CommandNumbers)enteredCommand)
                    {
                        case CommandNumbers.Help:
                            PrintHelp();
                            break;
                        case CommandNumbers.Push:
                            Console.Write("Enter value: ");
                            stack.Push(int.Parse(Console.ReadLine()));
                            break;
                        case CommandNumbers.Pop:
                            if (stack.IsEmpty())
                            {
                                Console.WriteLine("The stack is empty!!!");
                            }
                            else
                            {
                                var topValue = stack.Pop();
                                Console.WriteLine($"Top value is: {topValue}");
                            }

                            break;
                        default:
                            return;
                    }

                    Console.WriteLine("Success!");
                }
                else
                {
                    PrintHelp();
                }
            }
        }

        private static void PrintHelp()
        {
            foreach (var command in Commands)
            {
                Console.WriteLine($"Print {(int)command.Number} to {command.HelpMessage}");
            }
        }

        private struct Command
        {
            public CommandNumbers Number;
            public string HelpMessage;

            public Command(CommandNumbers number, string message)
            {
                this.Number = number;
                this.HelpMessage = message;
            }
        }
    }
}