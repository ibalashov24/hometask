using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

namespace SimpleFTPClientGUI
{
    public class ConnectCommand : ICommand
    {
        private ViewModel viewModel;

        private Model model;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object a)    
        {
            return viewModel != null;
        }

        public void Execute(object a)
        {
            var isPortCorrect = int.TryParse(this.viewModel.ServerPort, out int serverPort);

            if (viewModel.ServerIP != null && isPortCorrect)
            {
                this.model.ReconnectToServer(this.viewModel.ServerIP, serverPort);
                this.viewModel.UpdateControlsState("ItemList");
            }
        }

        public ConnectCommand(ViewModel viewModel, Model model)
        {
            this.viewModel = viewModel;
            this.model = model;
        }
    }
}
