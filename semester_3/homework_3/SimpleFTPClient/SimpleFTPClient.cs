namespace SimpleFTP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// Simple FTP Client which can handle 2 commands
    /// </summary>
    public class SimpleFTPClient
    {
        /// <summary>
        /// The server hostname
        /// </summary>
        private readonly string serverHostname;

        /// <summary>
        /// The server port
        /// </summary>
        private readonly int serverPort;

        /// <summary>
        /// Current client instance
        /// </summary>
        private TcpClient client;

        /// <summary>
        /// The output stream of a client
        /// </summary>
        private StreamReader inputStream;

        /// <summary>
        /// The output stream of a client
        /// </summary>
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

        /// <summary>
        /// Gets a value indicating whether client
        /// <see cref="T:SimpleFTP.SimpleFTPClient"/> is connected to server
        /// </summary>
        /// <value><c>true</c> if is connected; otherwise, <c>false</c>.</value>
        public bool IsConnected => this.client.Connected;

        /// <summary>
        /// Receives the file from a server
        /// </summary>
        /// <returns><c>true</c>, if file was received, <c>false</c> otherwise.</returns>
        /// <param name="pathToFile">Path to file on a server</param>
        /// <param name="pathToSaveFile">Path to save receive file</param>
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

        /// <summary>
        /// Receives directory content from a server
        /// </summary>
        /// <returns>Directory content</returns>
        /// <param name="pathToDirectory">Path to directory on a server</param>
        public List<FileMetaInfo> ReceiveFileList(string pathToDirectory)
        {
            this.ConnectToServer();

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

                if (!bool.TryParse(isDirectoryString, out bool isDirectory))
                {
                    this.DisconnectFromServer();
                    return null;
                }

                result.Add(new FileMetaInfo(fileName, isDirectory));
            }

            this.DisconnectFromServer();
            return result;
        }

        /// <summary>
        /// Reads single integer from input stream.
        /// </summary>
        /// <returns>The integer from input stream.</returns>
        private int ReadIntegerFromInputStream()
        {
            string fileSizeString = string.Empty;
            while (!fileSizeString.EndsWith(" ", StringComparison.Ordinal))
            {
                if (!this.inputStream.EndOfStream)
                {
                    var t = this.inputStream.Read();
                    fileSizeString += (char)t;
                }
            }


            if (!int.TryParse(fileSizeString, out int resultInteger))
            {
                this.inputStream.ReadToEnd();
                throw new FormatException();
            }

            return resultInteger;
        }

        /// <summary>
        /// Estabilishing connection to the server
        /// </summary>
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

        /// <summary>
        /// Disconnects from server
        /// </summary>
        private void DisconnectFromServer()
        {
            this.outputStream.Close();
            this.inputStream.Close();
            this.client.Close();
        }
    }
}
