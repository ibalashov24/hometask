using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleFTP;
using SimpleFTPClientGUI.FileExplorer;
using SimpleFTPClientGUI.DownloadStatus;

namespace SimpleFTPClientGUI
{
    /// <summary>
    /// Application MVVM Model
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Root folder designation on server
        /// </summary>
        public const string DefaultFolderOnServer = "~";

        /// <summary>
        /// Default LevelUp folder name
        /// </summary>
        public const string LevelUpFolderName = "...";

        /// <summary>
        /// Client backend
        /// </summary>
        private SimpleFTPClient client;

        /// <summary>
        /// Stack containig path details (empty => current folder is empty)
        /// </summary>
        private Stack<string> Path = new Stack<string>();

        /// <summary>
        /// Gets current path is one string
        /// </summary>
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

        /// <summary>
        /// True if Model is connected to the server
        /// </summary>
        public bool IsConnected => this.client != null;

        /// <summary>
        /// Initializes new instance of Model
        /// </summary>
        public Model()
        {
        }

        /// <summary>
        /// Goes to the level down to subfolder
        /// </summary>
        /// <param name="subfolderName">Subfolder to go to</param>
        public void GoToSubfolder(string subfolderName)
        {
            if (subfolderName == LevelUpFolderName)
            {
                this.GoLevelUp();
                return;
            }

            this.Path.Push(subfolderName);
        }

        /// <summary>
        /// Goes to the level up to parent folder
        /// </summary>
        /// <returns>New current folder</returns>
        public string GoLevelUp()
        {
            if (this.Path.Count == 0)
            {
                return DefaultFolderOnServer;
            }

            return this.Path.Pop();
        }

        /// <summary>
        /// Reconects to the server with given credentials
        /// </summary>
        /// <param name="hostname">Server hostname</param>
        /// <param name="port">Server port</param>
        public void ReconnectToServer(string hostname, int port)
        {
            this.client = new SimpleFTP.SimpleFTPClient(hostname, port);
        }

        /// <summary>
        /// Downloads requested files from the server and shows progress
        /// </summary>
        /// <param name="files">Files to download</param>
        /// <param name="folderToSave">Folder to save files to</param>
        /// <param name="showStatusGUI">
        /// If true then progress will be shown in separate window
        /// </param>
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

            foreach(var file in statusWindow.Items)
            {
                var task = new Task(() =>
                {
                    statusWindow.Dispatcher.Invoke((Action)(() => file.SetItemStatus(
                        ItemStatus.InProgress)));

                    string filePath = string.Empty;
                    if (this.CurrentDirectory != DefaultFolderOnServer)
                    {
                        filePath = this.CurrentDirectory + '\\';
                    }
                        
                    var isReceived = this.client.ReceiveFile(
                        filePath + file.ItemName,
                        folderToSave + '\\' + file.ItemName);
                    
                    statusWindow.Dispatcher.Invoke((Action)(() => file.SetItemStatus(
                        isReceived ? ItemStatus.Downloaded : ItemStatus.Failed)));
                });
                task.Start();
            }
        }

        /// <summary>
        /// Retrieves item list in current directory
        /// </summary>
        /// <returns>Item list</returns>
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
