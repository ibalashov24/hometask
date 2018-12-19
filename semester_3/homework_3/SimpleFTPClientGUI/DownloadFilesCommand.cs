using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using System.Windows.Forms;

using SimpleFTPClientGUI.FileExplorer;

namespace SimpleFTPClientGUI
{
    public class DownloadFilesCommand : ICommand
    {
        private ViewModel viewModel;

        private Model model;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object a)
        {
            return this.viewModel != null;
        }

        public void Execute(object a)
        {
            IList<ItemInfo> filesToDownload;
            if ((string)a == "Selected")
            {
                filesToDownload = this.viewModel.SelectedForHandling;
            }
            else
            {
                filesToDownload = this.viewModel.ItemList;
            }

            if (this.model.IsConnected && filesToDownload != null)
            {
                var savePath = this.GetDestinationPath();
                this.model.DownloadSelectedFiles(filesToDownload, savePath);
            }
        }

        private string GetDestinationPath()
        {
            var folderChooser = new FolderBrowserDialog();
            folderChooser.ShowDialog();

            return folderChooser.SelectedPath;
        }

        public DownloadFilesCommand(ViewModel viewModel, Model model)
        {
            this.viewModel = viewModel;
            this.model = model;
        }
    }
}
