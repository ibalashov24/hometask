using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using System.ComponentModel;

using SimpleFTPClientGUI.FileExplorer;

namespace SimpleFTPClientGUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Model model = new Model();

        private ConnectCommand connectCommand;

        private DownloadFilesCommand downloadFilesCommand;

        private OpenFolderCommand openFolderCommand;

        public List<ItemInfo> ItemList
        {
            get => this.model.IsConnected ? this.model.GetItemsInCurrentDirectory() : null;
        }

        public List<ItemInfo> SelectedForHandling { get; set; }

        public string ServerIP { get; set; }

        public string ServerPort { get; set; }

        public ICommand Connect => this.connectCommand;

        public ICommand DownloadFiles => this.downloadFilesCommand;

        public ICommand OpenSelectedFolder => this.openFolderCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            this.connectCommand = new ConnectCommand(this, model);
            this.downloadFilesCommand = new DownloadFilesCommand(this, model);
            this.openFolderCommand = new OpenFolderCommand(this, model);
        }

        public void UpdateControlsState(string changedPropertyName)
        {
            this.PropertyChanged?.Invoke(this, 
                new PropertyChangedEventArgs(null));
        }
    }
}
