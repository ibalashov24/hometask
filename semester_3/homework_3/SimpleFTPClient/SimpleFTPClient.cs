namespace SimpleFTP
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Net.Sockets;

    public class SimpleFTPClient
    {
		private TcpClient client = new TcpClient();

		private StreamReader inputStream;
		private StreamWriter outputStream;

		private string serverHostname;
		private int serverPort;

		public bool IsConnected => client.Connected;

		/// <summary>
        /// Initializes a new instance of the <see cref="T:SimpleFTPClient.SimpleFTPClient"/> class.
        /// </summary>
        /// <param name="serverHostname">IP of the SimpleFTP server</param>
        /// <param name="port">Server port</param>
		public SimpleFTPClient(string serverHostname, int port)
        {
			this.serverHostname = serverHostname;
			this.serverPort = port;
        }

		public bool ReceiveFile(string pathToFile, string pathToSaveFile)
		{
			// Block size in bytes
			const int blockSize = 1024 * 1024;

			this.ConnectToServer();

			this.outputStream.WriteLine('2' + pathToFile);
			this.outputStream.Flush();

			Console.WriteLine("Request to get a file is sent");

			var firstChar = this.inputStream.Peek();

			if (firstChar == '-')
			{
				this.inputStream.ReadLine();
				this.DisconnectFromServer();
				return false;
			}

			Console.WriteLine("Answer is received");

			int leftBytesToRead;
			try
			{
				leftBytesToRead = this.ReadIntegerFromInputStream();
			}
			catch (FormatException)
			{
				this.inputStream.ReadLine();
				this.DisconnectFromServer();
				return false;
			}
			         
			Console.WriteLine("Downloading {0}B file", leftBytesToRead);

			var resultFileStream = File.Create(pathToSaveFile);
			this.inputStream.BaseStream.CopyTo(resultFileStream, blockSize);

			Console.WriteLine("File saved");

			this.DisconnectFromServer();

			return true;
		}

		public List<FileMetaInfo> ReceiveFileList(string pathToDirectory)
		{
			this.ConnectToServer();

			this.outputStream.WriteLine('1' + pathToDirectory);
			this.outputStream.Flush();

			Console.WriteLine("Request to get directory content is sent");

			string[] directoryContent = this.inputStream.ReadLine().Split(' ');

			if (directoryContent.Length == 0 || 
			    directoryContent.Length != 0 && directoryContent[0] == "-1")
			{
				return null;
			}

			int fileCount;
			if (!int.TryParse(directoryContent[0], out fileCount))
			{
				return null;
			}

			Console.WriteLine("Directory content is received");

			var result = new List<FileMetaInfo>();

			for (int i = 0; i < fileCount; ++i)
			{
				var fileName = directoryContent[2 * i + 1].Replace('/', ' ');
				var isDirectoryString = directoryContent[2 * i + 2];

				bool isDirectory;
				if (!bool.TryParse(isDirectoryString, out isDirectory))
				{
					return null;
				}

				result.Add(new FileMetaInfo(fileName, isDirectory));
			}

			this.DisconnectFromServer();

			return result;
		}

        private int ReadIntegerFromInputStream()
		{
			string fileSizeString = "";
            while (!fileSizeString.EndsWith(" ", StringComparison.Ordinal))
            {
                fileSizeString += (char)this.inputStream.Read();
            }

			int resultInteger;
            if (!int.TryParse(fileSizeString, out resultInteger))
            {
                this.inputStream.ReadToEnd();
				throw new FormatException();
            }

			return resultInteger;
		}

		private void ConnectToServer()
		{
			this.client.Connect(this.serverHostname, this.serverPort);
            this.inputStream = new StreamReader(this.client.GetStream());
            this.outputStream = new StreamWriter(this.client.GetStream());
		}

        private void DisconnectFromServer()
        {
            this.outputStream.Close();
            this.inputStream.Close();
            this.client.Close();
        }
    }
}
