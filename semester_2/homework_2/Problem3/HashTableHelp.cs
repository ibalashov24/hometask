namespace Problem3
{
    internal class HashTableHelp
    {
        // All available messages of the program
        public static readonly HelpMessage[] Instanses = new HelpMessage[]
        {
            new HelpMessage(HelpCommands.ExitProgram, "exit program"),
            new HelpMessage(HelpCommands.InsertElement, "insert element to hash table"),
            new HelpMessage(HelpCommands.EraseElement, "erase element"),
            new HelpMessage(HelpCommands.Factor, "print fill factor"),
            new HelpMessage(HelpCommands.CheckInTable, "check if value is in table")
        };

        // Available commands of the program
        internal enum HelpCommands
        {
            ExitProgram,
            InsertElement,
            EraseElement,
            Factor,
            CheckInTable
        }

        // Single help message in help function
        internal class HelpMessage
        {
            public HelpCommands CommandNumber;
            public string Message;

            public HelpMessage(HelpCommands command, string message)
            {
                this.CommandNumber = command;
                this.Message = message;
            }
        }
    }
}