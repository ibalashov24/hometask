using System;
using System.Windows;

namespace SimpleFTPClientGUI
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes new instance of MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles situation when user requesed folder opening in FileExplorer
        /// </summary>
        /// <param name="sender">FileExplorer</param>
        /// <param name="e">Information about selection</param>
        private void UserRequestedFolderOpening(object sender, EventArgs e)
        {
            var viewModel = this.DataContext as ViewModel;
            if (viewModel.OpenSelectedFolder.CanExecute(null))
            {
                viewModel.OpenSelectedFolder.Execute(null);
            }
        }
    }
}
