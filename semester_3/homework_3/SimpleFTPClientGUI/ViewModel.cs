using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;

using SimpleFTPClientGUI.FileExplorer;

namespace SimpleFTPClientGUI
{
    /// <summary>
    /// SimpleFTPClientGUI MVVM ViewModel
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// MVVM Model
        /// </summary>
        private Model model = new Model();

        /// <summary>
        /// Command which connects to the server
        /// </summary>
        private ConnectCommand connectCommand;

        /// <summary>
        /// Command which downloads files from server
        /// </summary>
        private DownloadFilesCommand downloadFilesCommand;

        /// <summary>
        /// Command which opens folder on server
        /// </summary>
        private OpenFolderCommand openFolderCommand;

        /// <summary>
        /// Item list which displays in FileExplorer
        /// </summary>
        public List<ItemInfo> ItemList
        {
            get => this.model.IsConnected ? this.model.GetItemsInCurrentDirectory() : null;
        }

        /// <summary>
        /// Items which was selected by user in FileExplorer
        /// </summary>
        public List<ItemInfo> SelectedForHandling { get; set; }

        /// <summary>
        /// Server IP
        /// </summary>
        public string ServerIP { get; set; }

        /// <summary>
        /// Server Port
        /// </summary>
        public string ServerPort { get; set; }

        /// <summary>
        /// Connects to the server
        /// </summary>
        public ICommand Connect => this.connectCommand;

        /// <summary>
        /// Downloads files from the server
        /// </summary>
        public ICommand DownloadFiles => this.downloadFilesCommand;

        /// <summary>
        /// Opens folder on the server
        /// </summary>
        public ICommand OpenSelectedFolder => this.openFolderCommand;

        /// <summary>
        /// Fires if some property was changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes new instance of ViewModel
        /// </summary>
        public ViewModel()
        {
            this.connectCommand = new ConnectCommand(this, model);
            this.downloadFilesCommand = new DownloadFilesCommand(this, model);
            this.openFolderCommand = new OpenFolderCommand(this, model);
        }

        /// <summary>
        /// Refreshes properties on window
        /// </summary>
        /// <param name="changedPropertyName">Property to refresh</param>
        public void UpdateControlsState(string changedPropertyName)
        {
            this.PropertyChanged?.Invoke(this, 
                new PropertyChangedEventArgs(null));
        }
    }
}
