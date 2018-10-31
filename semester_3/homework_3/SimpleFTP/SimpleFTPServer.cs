namespace SimpleFTP
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Security;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Implement simple FTP-like server with 2 commands
    /// </summary>
    public class SimpleFTPServer : IDisposable
    {
        private volatile int currentConnectionCount;

        private CancellationTokenSource cancellationToken
                = new CancellationTokenSource();

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SimpleFTPServer"/> class.
        /// </summary>
        /// <param name="maxConnectionCount">
        /// Maximal simultaneous
        /// connection count
        /// </param>
        /// <param name="port">Port to listen</param>
        public SimpleFTPServer(int port, int maxConnectionCount)
        {
            this.MaxConnectionCount = maxConnectionCount;
            this.PortNumber = port;
        }

        /// <summary>
        /// Gets the server port number
        /// </summary>
        /// <value>0..65536</value>
        public int PortNumber { get; }

        /// <summary>
        /// Gets maximal count of simultaneous connections
        /// </summary>
        /// <value>Maximal count of connections</value>
        public int MaxConnectionCount { get; }

        /// <summary>
        /// Gets the current connection count.
        /// </summary>
        /// <value>Connection count</value>
        public int CurrentConnectionCount => this.currentConnectionCount;

        /// <summary>
        /// Launch the main server loop
        /// </summary>
        public void Start()
        {
            var mainLoop = new Thread(async () =>
            {
                var listener = new TcpListener(IPAddress.Any, this.PortNumber);
                listener.Start();

                Console.WriteLine("Server started on port {0}", this.PortNumber);

                while (!this.cancellationToken.IsCancellationRequested)
                {
                    var newClient = await listener.AcceptTcpClientAsync();

                    if (this.cancellationToken.IsCancellationRequested)
                    {
                        listener.Stop();
                        newClient.Close();
                        this.currentConnectionCount = 0;
                        return;
                    }

                    IPAddress clientIP;
                    try
                    {
                        clientIP = ((IPEndPoint)newClient.Client.RemoteEndPoint).Address;
                    }
                    catch (ObjectDisposedException)
                    {
                        Console.WriteLine("New client has closed socket, skipping");
                        continue;
                    }

                    Console.WriteLine("{0} connected", clientIP);

                    if (this.CurrentConnectionCount >= this.MaxConnectionCount)
                    {
                        newClient.Close();
                        Console.WriteLine("{0} disconnected: Limit reached", clientIP);

                        continue;
                    }

                    ++this.currentConnectionCount;

                    ThreadPool.QueueUserWorkItem(
                        this.ServeConnectedClient,
                        newClient);
                }

                listener.Stop();
            });

            mainLoop.Start();
        }

        public void Shutdown()
        {
            this.cancellationToken.Cancel();
        }

        public void Dispose()
        {
            this.Shutdown();
        }

        /// <summary>
        /// Serves the connected client
        /// </summary>
        /// <param name="clientObject">TCP Client (of type TcpClient)</param>
        private async void ServeConnectedClient(object clientObject)
        {
            var client = (TcpClient)clientObject;

            IPAddress clientIP;
            StreamReader inputStream;
            StreamWriter outputStream;
            try
            {
                clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                inputStream = new StreamReader(client.GetStream());
                outputStream = new StreamWriter(client.GetStream());
            }
            catch (ObjectDisposedException)
            {
                this.HandleDisconnectedClient();
                --this.currentConnectionCount;
                return;
            }

            var commandType = new char[1];
            try
            {
                await inputStream.ReadAsync(commandType, 0, 1);
            }
            catch (ObjectDisposedException)
            {
                this.HandleDisconnectedClient();
                --this.currentConnectionCount;
                return;
            }

            if (commandType[0] == '1' || commandType[0] == '2')
            {
                string path;
                try
                {
                    path = await inputStream.ReadLineAsync();
                }
                catch (ObjectDisposedException)
                {
                    this.HandleDisconnectedClient();
                    --this.currentConnectionCount;
                    return;
                }

                if (commandType[0] == '1')
                {
                    Console.WriteLine(
                        "Client {0} requested directory content",
                        clientIP);
                    this.WriteDirectoryContent(path, outputStream);
                }
                else
                {
                    Console.WriteLine("Client {0} requested file", clientIP);
                    this.WriteFileBytes(path, outputStream);
                }
            }

            client.Close();
            --this.currentConnectionCount;

            Console.WriteLine("{0} disconnected", clientIP);
        }

        /// <summary>
        /// Sends the content of the directory to the client
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="output">Stream to client</param>
        private void WriteDirectoryContent(string path, StreamWriter output)
        {
            DirectoryInfo directory;
            try
            {
                directory = new DirectoryInfo(path);
            }
            catch (SecurityException)
            {
                this.HandleIncorrectPath(output, "Access forbidden.");
                return;
            }
            catch (ArgumentException)
            {
                this.HandleIncorrectPath(output, "Path contains prohibited characters.");
                return;
            }
            catch (PathTooLongException)
            {
                this.HandleIncorrectPath(output, "Path is too long.");
                return;
            }

            if (!directory.Exists)
            {
                this.HandleIncorrectPath(output, "Directory does not exist.");
                return;
            }

            FileSystemInfo[] directoryContent;
            try
            {
                directoryContent = directory.GetFileSystemInfos();
            }
            catch (UnauthorizedAccessException)
            {
                this.HandleIncorrectPath(output, "Access forbidden.");
                return;
            }

            try
            {
                output.Write("{0}", directoryContent.Length);
            }
            catch (ObjectDisposedException)
            {
                this.HandleDisconnectedClient();
                return;
            }

            foreach (var entity in directoryContent)
            {
                try
                {
                    // Replacing spaces with character, prohibited in filenames
                    if (entity is DirectoryInfo)
                    {
                        output.Write(
                            " {0} {1}",
                            ((DirectoryInfo)entity).Name.Replace(' ', '/'),
                            true.ToString());
                    }
                    else if (entity is FileInfo)
                    {
                        output.Write(
                            " {0} {1}",
                            ((FileInfo)entity).Name.Replace(' ', '/'),
                            false.ToString());
                    }
                } 
                catch (ObjectDisposedException)
                {
                    this.HandleDisconnectedClient();
                    return;
                }
            }

            try
            {
                output.WriteLine();
                output.Flush();
            }
            catch (ObjectDisposedException)
            {
                this.HandleDisconnectedClient();
            }
        }

        private void WriteFileBytes(string path, StreamWriter output)
        {
            const int bufferSize = 1024 * 1024;

            FileInfo file;
            try
            {
                file = new FileInfo(path);
            }
            catch (SecurityException)
            {
                this.HandleIncorrectPath(output, "Access forbidden.");
                return;
            }
            catch (UnauthorizedAccessException)
            {
                this.HandleIncorrectPath(output, "Access to file is denied.");
                return;
            }
            catch (ArgumentException)
            {
                this.HandleIncorrectPath(output, "Path contains prohibited characters.");
                return;
            }
            catch (PathTooLongException)
            {
                this.HandleIncorrectPath(output, "Path to file is too long.");
                return;
            }
            catch (NotSupportedException)
            {
                this.HandleIncorrectPath(output, "File name contains colon.");
                return;
            }

            if (!file.Exists)
            {
                this.HandleIncorrectPath(output, "File does not exist.");
                return;
            }

            FileStream fileStream;
            try
            {
                fileStream = file.OpenRead();
            }
            catch (IOException)
            {
                this.HandleIncorrectPath(output, "File is already open.");
                return;
            }
            catch (UnauthorizedAccessException)
            {
                this.HandleIncorrectPath(output, "It is a directory.");
                return;
            }

            try
            {
                output.Write("{0} ", file.Length);
                output.Flush();
            }
            catch (ObjectDisposedException)
            {
                this.HandleDisconnectedClient();
                return;
            }

            try
            {
                fileStream.CopyTo(output.BaseStream, bufferSize);
            }
            catch (ObjectDisposedException)
            {
                this.HandleDisconnectedClient();
                return;
            }
            catch (IOException)
            {
                this.HandleDisconnectedClient();
                return;
            }

            try
            {
                output.Flush();
            }
            catch (ObjectDisposedException)
            {
                this.HandleDisconnectedClient();
                return;
            }
        }

        /// <summary>
        /// Handles the situation when the client disconnected
        /// </summary>
        private void HandleDisconnectedClient()
        {
            Console.WriteLine("Client has closed socket, skipping");
        }

        /// <summary>
        /// Handles the situation when client provided invalid path
        /// </summary>
        /// <param name="output">Stream to client</param>
        /// <param name="message">Message to log</param>
        private void HandleIncorrectPath(
            StreamWriter output,
            string message = "")
        {
            Console.WriteLine("Client sent invalid path. " + message);

            try
            {
                output.WriteLine("-1");
                output.Flush();
            }
            catch (ObjectDisposedException)
            {
                this.HandleDisconnectedClient();
            }
        }
    }
}