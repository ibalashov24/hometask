using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SimpleFTPClientGUI.FileExplorer;

namespace SimpleFTPClientGUI
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

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
