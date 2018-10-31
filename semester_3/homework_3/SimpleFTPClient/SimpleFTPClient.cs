namespace SimpleFTP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;

    public class SimpleFTPClient
    {
        private readonly string serverHostname;
        private readonly int serverPort;

        private TcpClient client;
        private StreamReader inputStream;
        private StreamWriter outputStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleFTPClient"/> class.
        /// </summary>
        /// <param name="serverHostname">IP of the SimpleFTP server</param>
        /// <param name="port">Server port</param>
        public SimpleFTPClient(string serverHostname, int port)
        {
            this.serverHostname = serverHostname;
            this.serverPort = port;
        }

        public bool IsConnected => this.client.Connected;

        public bool ReceiveFile(string pathToFile, string pathToSaveFile)
        {
            // Block size in bytes
            const int blockSize = 1024 * 1024;

            try
            {
                this.ConnectToServer();
            }
            catch (ObjectDisposedException)
            {
                this.DisconnectFromServer();
                return false;
            }

            try
            {
                this.outputStream.WriteLine('2' + pathToFile);
                this.outputStream.Flush();
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("Connection broken.");
                this.DisconnectFromServer();
                return false;
            }

            Console.WriteLine("Request to get a file is sent");

            int firstChar;
            try
            {
                firstChar = this.inputStream.Peek();
            }
            catch (IOException)
            {
                Console.WriteLine("Connection corrupted");
                this.DisconnectFromServer();
                return false;
            }

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
                try
                {
                    this.inputStream.ReadLine();
                }
                catch (IOException) {}

                this.DisconnectFromServer();
                return false;
            }

            Console.WriteLine("Downloading file ({0}B)", leftBytesToRead);

            FileStream resultFileStream;
            try
            {
                resultFileStream = File.Create(pathToSaveFile);
            }

            // It is bad, but there are 7 different exceptions possible
            catch (SystemException)
            {
                Console.WriteLine("Cannot create file on disk");
                this.DisconnectFromServer();
                return false;
            }

            try
            {
                this.inputStream.BaseStream.CopyTo(resultFileStream, blockSize);
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("Connection broken");
                this.DisconnectFromServer();
                return false;
            }
            catch (IOException)
            {
                Console.WriteLine("IO exception occured");
                this.DisconnectFromServer();
                return false;
            }

            Console.WriteLine("File saved");

            resultFileStream.Close();
            this.DisconnectFromServer();
            return true;
        }

        public List<FileMetaInfo> ReceiveFileList(string pathToDirectory)
        {
            try
            {
                this.ConnectToServer();
            }
            catch (ObjectDisposedException)
            {
                this.DisconnectFromServer();
                return null;
            }

            try
            {
                this.outputStream.WriteLine('1' + pathToDirectory);
                this.outputStream.Flush();
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("Connection broken");
                this.DisconnectFromServer();
                return null;
            }

            Console.WriteLine("Request to get directory content is sent");

            string[] directoryContent;
            try
            {
                directoryContent = this.inputStream.ReadLine().Split(' ');
            }
            catch (IOException)
            {
                Console.WriteLine("Connection corrupter.");
                this.DisconnectFromServer();
                return null;
            }

            Console.WriteLine("Answer is received");

            if (directoryContent.Length == 0 ||
                (directoryContent.Length != 0 && directoryContent[0] == "-1"))
            {
                Console.WriteLine("Directory does not exists");
                this.DisconnectFromServer();
                return null;
            }

            if (!int.TryParse(directoryContent[0], out int fileCount))
            {
                this.DisconnectFromServer();
                return null;
            }

            Console.WriteLine("Directory content is received");

            var result = new List<FileMetaInfo>();

            for (int i = 0; i < fileCount; ++i)
            {
                var fileName = directoryContent[(2 * i) + 1].Replace('/', ' ');
                var isDirectoryString = directoryContent[(2 * i) + 2];

                bool isDirectory;
                if (!bool.TryParse(isDirectoryString, out isDirectory))
                {
                    this.DisconnectFromServer();
                    return null;
                }

                result.Add(new FileMetaInfo(fileName, isDirectory));
            }

            this.DisconnectFromServer();
            return result;
        }

        private int ReadIntegerFromInputStream()
        {
            string fileSizeString = string.Empty;

            try
            {
                while (!fileSizeString.EndsWith(" ", StringComparison.Ordinal))
                {
                    fileSizeString += (char)this.inputStream.Read();
                }
            }
            catch (ObjectDisposedException)
            {
                throw;
            }

            if (!int.TryParse(fileSizeString, out int resultInteger))
            {
                this.inputStream.ReadToEnd();
                throw new FormatException();
            }

            return resultInteger;
        }

        private void ConnectToServer()
        {
            try
            {
                this.client = new TcpClient(this.serverHostname, this.serverPort);
                this.inputStream = new StreamReader(this.client.GetStream());
                this.outputStream = new StreamWriter(this.client.GetStream());
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("Failed to connect to server.");
                throw;
            }
        }

        private void DisconnectFromServer()
        {
            this.outputStream.Close();
            this.inputStream.Close();
            this.client.Close();
        }
    }
}
