using System;

namespace Program
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			var server = new SimpleFTP.SimpleFTPServer(12345, 1);

			server.Start();

            Console.WriteLine("Hello World!");
        }
    }
}
