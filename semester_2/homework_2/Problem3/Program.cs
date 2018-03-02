namespace Problem3
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            PrintProgramName();
            var hashFunction = ChooseHashFunction();
            PrintHelp();

            var hashTable = new HashTableStuff.HashTable<string, int>(hashFunction);

            while (true)
            {
                Console.Write("Enter command: ");
                if (!int.TryParse(Console.ReadLine(), out int currentCommand))
                {
                    Console.WriteLine("Invalid command!");
                    PrintHelp();
                }

                if (!HashTableHelp.HelpCommands.IsDefined(typeof(HashTableHelp.HelpCommands), currentCommand))
                {
                    Console.WriteLine("Invalid command!");
                    PrintHelp();
                }
                else
                {
                    switch ((HashTableHelp.HelpCommands)currentCommand)
                    {
                        case HashTableHelp.HelpCommands.ExitProgram:
                            Console.WriteLine("Good bye!");
                            return;

                        case HashTableHelp.HelpCommands.CheckInTable:
                            Console.Write("Enter key to check: ");
                            var checkKey = Console.ReadLine();
                            Console.WriteLine(hashTable.IsInTable(checkKey) ? "In table" : "Not in table");
                            break;

                        case HashTableHelp.HelpCommands.InsertElement:
                            Console.Write("Enter key to insert (or modify): ");
                            var insertKey = Console.ReadLine();
                            Console.Write($"Enter new value of table[\"{insertKey}\"] (int): ");
                            var insertValue = int.Parse(Console.ReadLine());
                            hashTable[insertKey] = insertValue;
                            break;

                        case HashTableHelp.HelpCommands.EraseElement:
                            Console.Write("Enter key to erase: ");
                            var eraseKey = Console.ReadLine();
                            hashTable.Erase(eraseKey);
                            break;

                        case HashTableHelp.HelpCommands.Factor:
                            Console.WriteLine($"Fill factor == {hashTable.FillFactor}");
                            break;

                        default:
                            PrintHelp();
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Asks user for hash function and returns it
        /// </summary>
        /// <returns>
        /// Delegate to chosen hash function
        /// </returns>
        private static HashTableStuff.HashFunctionType<string> ChooseHashFunction()
        {
            while (true)
            {
                PrintAvailableHashFunctions();

                Console.Write("Enter name of chosen hash function (or '0' to use default): ");
                var chosenHelpFunctionName = Console.ReadLine();

                if (chosenHelpFunctionName == "0")
                {
                    return null;
                }

                var hashFunctionInfo = typeof(HashTableStuff.StringHashFunctions)
                    .GetMethod(chosenHelpFunctionName);
                if (hashFunctionInfo == null)
                {
                    Console.WriteLine("Incorrect function!");
                }
                else
                {
                    return (HashTableStuff.HashFunctionType<string>)hashFunctionInfo.CreateDelegate(
                        typeof(HashTableStuff.HashFunctionType<string>));
                }
            }
        }

        /// <summary>
        /// Prints names of all available hash functions (except <see cref="object.GetHashCode()"/>)
        /// </summary>
        private static void PrintAvailableHashFunctions()
        {
            Console.WriteLine("Available hash functions: ");
            foreach (var method in typeof(HashTableStuff.StringHashFunctions).GetMethods(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Static))
            {
                Console.WriteLine(method.Name);
            }
        }


        /// <summary>
        /// Prints program name
        /// </summary>
        private static void PrintProgramName()
        {
            Console.WriteLine("Welcome to the hash table test program!");
        }

        /// <summary>
        /// Prints list of all available commands of the program
        /// </summary>
        private static void PrintHelp()
        {
            foreach (var instance in HashTableHelp.Instanses)
            {
                Console.WriteLine($"Enter {(int)instance.CommandNumber} to {instance.Message}");
            }
        }
    }
}