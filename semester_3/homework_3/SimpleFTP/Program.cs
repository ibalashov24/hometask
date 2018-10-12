using System;

namespace Program
{
    class MainClass
    {
        public static void Main()
        {
			var server = new SimpleFTP.SimpleFTPServer(12345, 2);

			server.Start();

            Console.WriteLine("Hello World!");
        }
    }
}
