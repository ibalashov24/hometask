using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections;

using SimpleFTP;
using SimpleFTPClientGUI.FileExplorer;
using SimpleFTPClientGUI.DownloadStatus;

using System.Threading;

namespace SimpleFTPClientGUI
{
    public class Model
    {
        public const string DefaultFolderOnServer = "~";

        public const string LevelUpFolderName = "...";

        private SimpleFTPClient client;

        private Stack<string> Path = new Stack<string>();

        public string CurrentDirectory
        {
            get
            {
                if (this.Path.Count == 0)
                {
                    return DefaultFolderOnServer;
                }

                string result = string.Empty;
                foreach (var e in this.Path)
                {
                    result = $"{e}/{result}";
                }

                return result;
            }
        }

        public bool IsConnected => this.client != null;

        public Model()
        {
        }

        public void GoToSubfolder(string subfolderName)
        {
            if (subfolderName == LevelUpFolderName)
            {
                this.GoLevelUp();
                return;
            }

            this.Path.Push(subfolderName);
        }

        public string GoLevelUp()
        {
            if (this.Path.Count == 0)
            {
                return DefaultFolderOnServer;
            }

            return this.Path.Pop();
        }

        public void ReconnectToServer(string hostname, int port)
        {
            this.client = new SimpleFTP.SimpleFTPClient(hostname, port);
        }

        public void DownloadSelectedFiles(
            IEnumerable<ItemInfo> files, 
            string folderToSave,
            bool showStatusGUI = true)
        {
            var statusWindow = new DownloadStatusWindow(
                    files.Where(a => !a.IsDirectory).Select(a => a.Name));

            if (showStatusGUI)
            {
                statusWindow.Show();
            }

            foreach (var item in statusWindow.Items)
            {
                item.SetItemStatus(ItemStatus.Downloaded);
                statusWindow.RefreshWindow();
            }

            Parallel.ForEach<ItemStatusInfo>(statusWindow.Items, (ItemStatusInfo file) =>
            {
                //Dispatcher.CurrentDispatcher.BeginInvoke(changeStatus, file, ItemStatus.InProgress);

                this.client.ReceiveFile(
                    this.CurrentDirectory + '\\' + file.ItemName, 
                    folderToSave + '\\' + file.ItemName);
                 
                //Dispatcher.CurrentDispatcher.BeginInvoke(changeStatus, file, ItemStatus.Downloaded);
            });
            
            if (showStatusGUI)
            {
                statusWindow.Close();
            } 
        }

        public List<ItemInfo> GetItemsInCurrentDirectory()
        {
            var items = this.client.ReceiveFileList(this.CurrentDirectory);

            var result = new List<ItemInfo>();
            // If not in root directory
            if (this.Path.Count != 0)
            {
                result.Add(new ItemInfo() { Name = "...", IsDirectory = true });
            }

            foreach (var item in items)
            {
                result.Add(new ItemInfo() { Name = item.Name, IsDirectory = item.IsDirectory });
            }

            return result;
        }
    }
}
