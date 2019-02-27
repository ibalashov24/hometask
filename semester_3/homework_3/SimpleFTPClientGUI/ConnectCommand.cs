using System;
using System.Windows.Input;

namespace SimpleFTPClientGUI
{
    /// <summary>
    /// Command which handles SimpleFTP server connection process
    /// </summary>
    public class ConnectCommand : ICommand
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
        /// Is execution state changed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// True if command could be executed
        /// </summary>
        /// <param name="a">Some unused object</param>
        /// <returns>True if could be executed</returns>
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
            var isPortCorrect = int.TryParse(this.viewModel.ServerPort, out int serverPort);

            if (viewModel.ServerIP != null && isPortCorrect)
            {
                this.model.ReconnectToServer(this.viewModel.ServerIP, serverPort);
                this.viewModel.UpdateControlsState("ItemList");
            }
        }

        /// <summary>
        /// Initializes new instance of ConnectCommand
        /// </summary>
        /// <param name="viewModel">App's MVVM ViewModel</param>
        /// <param name="model">App's MVVM Model</param>
        public ConnectCommand(ViewModel viewModel, Model model)
        {
            this.viewModel = viewModel;
            this.model = model;
        }
    }
}
