using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;

namespace SimpleFTPClientGUI.DownloadStatus
{
    /// <summary>
    /// Логика взаимодействия для DownloadStatusWindow.xaml
    /// </summary>
    public partial class DownloadStatusWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DownloadStatusWindow(IEnumerable<string> itemNames)
        {
            InitializeComponent();

            this.DataContext = this;

            foreach (var item in itemNames)
            {
                this.ItemList.Items.Add(new ItemStatusInfo(item));
            }

            this.RefreshWindow();
        }

        public List<ItemStatusInfo> Items
        {
            get => new List<ItemStatusInfo>(this.ItemList.Items.Cast<ItemStatusInfo>());
        }

        public void RefreshWindow()
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
