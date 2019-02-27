namespace Program
{
    using System;

    public class Program
    {
        public static void Main()
        {
            var server = new SimpleFTP.SimpleFTPServer(12345, 100);

            server.Start().GetAwaiter().GetResult();

            Console.WriteLine("Hello World!");
        }
    }
}
