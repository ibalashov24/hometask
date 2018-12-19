using System;
using System.Windows.Input;
using System.Windows.Forms;

namespace SimpleFTPClientGUI
{
    public class OpenFolderCommand : ICommand
    {
        private ViewModel viewModel;

        private Model model;

        public event EventHandler CanExecuteChanged;

        public OpenFolderCommand(ViewModel viewModel, Model model)
        {
            this.viewModel = viewModel;
            this.model = model;
        }

        public bool CanExecute(object a)
        {
            return viewModel != null;
        }

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
