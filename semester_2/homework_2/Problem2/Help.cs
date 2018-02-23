namespace Problem2
{
    internal class Help
    {
        public static readonly HelpMessage[] Instanses = new HelpMessage[]
        {
            new HelpMessage(HelpCommands.ExitProgram, "exit program"),
            new HelpMessage(HelpCommands.InsertElement, "insert element to the list"),
            new HelpMessage(HelpCommands.EraseElement, "erase element"),
            new HelpMessage(HelpCommands.CleanList, "clean list"),
            new HelpMessage(HelpCommands.Size, "print list size"),
            new HelpMessage(HelpCommands.PrintList, "print list content")
        };

        internal enum HelpCommands
        {
            ExitProgram,
            InsertElement,
            EraseElement,
            CleanList,
            Size,
            PrintList
        }

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