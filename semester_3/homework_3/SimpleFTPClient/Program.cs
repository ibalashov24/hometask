using System;

namespace Program
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			Console.WriteLine("SimpleFTPClient started");

			var client = new SimpleFTP.SimpleFTPClient("localhost", 12345);

			Console.WriteLine("Enter 1 if you want to get directory content");
			Console.WriteLine("Enter anything else to get file");
			Console.Write("Enter command: ");
			var command = Console.ReadLine();

			if (command == "1")
			{
				Console.Write("Choose directory to get content: ");
                var path = Console.ReadLine();

                var fileList = client.ReceiveFileList(path);
                foreach (var currentFile in fileList)
                {
                    Console.WriteLine("{0} {1}",
                                      currentFile.Name,
                                      currentFile.IsDirectory.ToString());
                }	
			}
			else
			{
				Console.Write("Choose file to download: ");
				var path = Console.ReadLine();

				if (client.ReceiveFile(path, "receivedFile.file"))
				{
					Console.WriteLine("Success");
				}
				else
				{
					Console.WriteLine("Download failed");
				}
			}
        }
    }
}
