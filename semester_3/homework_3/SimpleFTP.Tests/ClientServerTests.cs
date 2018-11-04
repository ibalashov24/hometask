using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace SimpleFTP.Tests
{
    [TestFixture]
    public class ServerLowLevelTests
    {
        private const int port = 12345;

        private const string hostname = "localhost";

        private const int maxConnections = 20;

        private SimpleFTP.SimpleFTPServer server;


        [OneTimeSetUp]
        public void ServerInitialization()
        {
            this.server = new SimpleFTPServer(port, maxConnections);
            server.Start();
        }


        [Test]
        public void ServerShouldCatchConnectionCorrectly()
        {
            Console.WriteLine("Smoke test");

            var client = new TcpClient(hostname, port);

            Assert.IsTrue(client.Connected);

            this.ForceDisconnectTCPClient(client);         
        }

        [Test]
        public void ServerShouldNotAcceptInvalidCommand()
        {
            Console.WriteLine("Invalid command test");

            var client = new TcpClient(hostname, port);
            var reader = new StreamReader(client.GetStream());

            this.SendInvalidCommand(client);
            var data = reader.ReadToEnd();

            if (data != string.Empty)
            {
                this.ForceDisconnectTCPClient(client);
                Assert.Fail();
            }
            else
            {
                client.Close();
            }
        }

        [Test]
        public void ServerShouldNotCrashIfClientDisconnectedWhileHandling()
        {
            Console.WriteLine("Break connection test");

            var client = new TcpClient(hostname, port);

            var writer = new StreamWriter(client.GetStream());
            var reader = new StreamReader(client.GetStream());

            // Sends invalid command
            writer.WriteLine("2 |||||somestrangepath");

            client.Close();
        }

        [Test]
        public void SimpleFTPClientShouldConnectToServerSuccessfully()
        {
            var client = new SimpleFTPClient(hostname, port);

            // It should crash if client is not able to connect
            client.ReceiveFileList("strange_test_path");
        }

        [Test]
        public void SimpleFTPClientShouldReceiveCorrectFileList()
        {
            var path = Path.Combine(Path.GetTempPath(), ".SimpleFTPTEST_TEST_");

            var actualFileList = this.CreateTestDirectory(path);

            var client = new SimpleFTPClient(hostname, port);
            var files = client.ReceiveFileList(path);

            Comparison<FileMetaInfo> comparison = 
                (item1, item2) =>
                {
                    if (item1.IsDirectory == item2.IsDirectory)
                    {
                        return String.Compare(
                                item1.Name, item2.Name, StringComparison.Ordinal);
                    }
                    else
                    {
                        return (item1.IsDirectory ? -1 : 1);
                    }
                };

            Assert.That(files, Is.EquivalentTo(actualFileList).Using(comparison));
        }

        [Test]
        public void SimpleFTPClientShouldReceiveCorrectFile()
        {
            var testFilePath = Path.GetTempFileName();
            var fileSavePath = Path.GetTempFileName();

            var client = new SimpleFTPClient(hostname, port);

            Assert.IsTrue(client.ReceiveFile(testFilePath, fileSavePath));

            var testFileContent = 
                new StreamReader(File.OpenRead(testFilePath)).ReadToEnd();
            var receivedFileContent = 
                new StreamReader(File.OpenRead(fileSavePath)).ReadToEnd();

            Assert.AreEqual(testFileContent, receivedFileContent);
        }

        private string CreateTestFile()
        {
            var fileContent = "TESTTESTTESTTEST";
            var filePath = Path.GetTempFileName();

            var writer = new StreamWriter(File.OpenWrite(filePath));
            writer.WriteLine(fileContent);
            writer.Flush();

            writer.Close();

            return filePath;
        }

        private List<FileMetaInfo> CreateTestDirectory(string dirName)
        {
            var result = new List<FileMetaInfo>();
            result.Add(new FileMetaInfo("subDirectory", true));
            result.Add(new FileMetaInfo("hugeFile.tst", false));
            for (int i = 1; i <= 4; ++i)
            {
                result.Add(new FileMetaInfo($"smallFile{i}.tst", false));
            }

            Directory.CreateDirectory(dirName);
            File.WriteAllText(
                Path.Combine(Path.GetFullPath(dirName), "hugeFile.tst"),
                        "TESTTESTTESTTESTTESTTESTTESTTESTTESTTEST");
            for (int i = 1; i <= 4; ++i)
            {
                File.WriteAllText(
                        Path.Combine(Path.GetFullPath(dirName), $"smallFile{i}.tst"),
                        "TEST");
            }
            Directory.CreateDirectory(Path.Combine(dirName, "subDirectory"));

            return result;
        }

        private void ForceDisconnectTCPClient(TcpClient clientToDisconnect)
        {
            this.SendInvalidCommand(clientToDisconnect);
            clientToDisconnect.Close();
        }

        private void SendInvalidCommand(TcpClient clientToHandle)
        {
            var writer = new StreamWriter(clientToHandle.GetStream());
            writer.WriteLine("32423");
            writer.Flush();
        }
    }
}
