using System;
using System.Windows.Input;
using System.Windows.Forms;

namespace SimpleFTPClientGUI
{
    /// <summary>
    /// Command which opens given folder on the server
    /// </summary>
    public class OpenFolderCommand : ICommand
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
        /// Can command availability changed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Initializes new instance of open folder command
        /// </summary>
        /// <param name="viewModel">App's MVVM ViewModel</param>
        /// <param name="model">App's MVVM Model</param>
        public OpenFolderCommand(ViewModel viewModel, Model model)
        {
            this.viewModel = viewModel;
            this.model = model;
        }

        /// <summary>
        /// Can command be executed
        /// </summary>
        /// <param name="a">Some unused object</param>
        /// <returns>True if can be executed</returns>
        public bool CanExecute(object a)
        {
            return viewModel != null;
        }

        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="a">Some unused object</param>
        public void Execute(object a)
        {
            if (this.viewModel.SelectedForHandling != null 
                && this.viewModel.SelectedForHandling[0].IsDirectory)
            {
                var requestedSubfolder = this.viewModel.SelectedForHandling[0].Name;
                this.model.GoToSubfolder(requestedSubfolder);

                this.viewModel.UpdateControlsState("Content");
            }
        }
    }
}
