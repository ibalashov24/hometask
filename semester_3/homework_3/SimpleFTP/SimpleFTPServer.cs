namespace SimpleFTP
{
	using System;
	using System.IO;
	using System.Net;
	using System.Net.Sockets;
	using System.Threading;
	using System.Threading.Tasks;

	public class SimpleFTPServer
	{
		public int PortNumber { get; }

		public int MaxConnectionCount { get; }

		public int CurrentConnectionCount => this.currentConnectionCount;

		private int currentConnectionCount = 0;

		private CancellationTokenSource cancellation =
			new CancellationTokenSource();

		/// <summary>
        /// Initializes a new instance of the 
		/// <see cref="T:SimpleFTP.SimpleFTPServer"/> class.
        /// </summary>
        /// <param name="maxConnectionCount">Maximal parallel 
		/// connection count</param>
		public SimpleFTPServer(int port, int maxConnectionCount)
		{
			this.MaxConnectionCount = maxConnectionCount;
			this.PortNumber = port;
		}

        public void Start()
		{
			this.HandleNewConnections();
		}
        
		private void HandleNewConnections()
		{
			var cancellationToken = this.cancellation.Token;

			var listener = new TcpListener(IPAddress.Any, this.PortNumber);
			listener.Start();

			Console.WriteLine("Server started on port {0}", this.PortNumber);

			while (!cancellationToken.IsCancellationRequested)
			{
				var newClient = listener.AcceptTcpClient();
				var clientIP = 
					((IPEndPoint)newClient.Client.RemoteEndPoint).Address;
				
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

			Console.WriteLine("Goodbye!");
		}

		private async void ServeConnectedClient(object clientObject)
		{
			var client = (TcpClient)clientObject;
			var clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
			var inputStream = new StreamReader(client.GetStream());
			var outputStream = new StreamWriter(client.GetStream());
   
			var commandType = new char[1];
			await inputStream.ReadAsync(commandType, 0, 1);

			if (commandType[0] == '1' || commandType[0] == '2')
			{
				var path = await inputStream.ReadLineAsync();

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

		private void WriteDirectoryContent(string path, StreamWriter output)
		{
			var directory = new DirectoryInfo(path);
			if (!directory.Exists)
			{
				output.WriteLine(-1);
				output.Flush();
				return;
			}

			var directoryContent = directory.GetFileSystemInfos();
			output.Write("{0}", directoryContent.Length);
            
			foreach (var entity in directoryContent)
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

            output.WriteLine();
            output.Flush();
		}

		private void WriteFileBytes(string path, StreamWriter output)
		{
			const int bufferSize = 1024 * 1024;

			var file = new FileInfo(path);
			if (!file.Exists)
			{
				output.WriteLine(-1);
                output.Flush();
                return;
			}

            var fileStream = file.OpenRead();

			output.Write("{0} ", file.Length);
			output.Flush();

			fileStream.CopyTo(output.BaseStream, bufferSize);

			output.WriteLine();
			output.Flush();
		}
	}
}