using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Forms;

using SimpleFTPClientGUI.FileExplorer;

namespace SimpleFTPClientGUI
{
    /// <summary>
    /// Command which handles file downloading process
    /// </summary>
    public class DownloadFilesCommand : ICommand
    {
        /// <summary>
        /// MVVM ViewModel
        /// </summary>
        private ViewModel viewModel;

        /// <summary>
        /// MVVM Model
        /// </summary>
        private Model model;

        /// <summary>
        /// Can command be executed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// True if command can be executed
        /// </summary>
        /// <param name="a">Some unused object</param>
        /// <returns>True if can be executed</returns>
        public bool CanExecute(object a)
        {
            return this.viewModel != null;
        }

        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="a">Downloading mode ("All" or "Selected")</param>
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

        /// <summary>
        /// Retrieves downloading destination path
        /// </summary>
        /// <returns>Destination path in file system</returns>
        private string GetDestinationPath()
        {
            var folderChooser = new FolderBrowserDialog();
            folderChooser.ShowDialog();

            return folderChooser.SelectedPath;
        }

        /// <summary>
        /// Initializes new instance of DownloadFileCommand
        /// </summary>
        /// <param name="viewModel">App's MVVM ViewModel</param>
        /// <param name="model">App's MVVM Model</param>
        public DownloadFilesCommand(ViewModel viewModel, Model model)
        {
            this.viewModel = viewModel;
            this.model = model;
        }
    }
}
