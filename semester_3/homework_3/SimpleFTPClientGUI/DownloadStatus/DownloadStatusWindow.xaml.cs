using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SimpleFTPClientGUI.DownloadStatus
{
    /// <summary>
    /// Логика взаимодействия для DownloadStatusWindow.xaml
    /// </summary>
    public partial class DownloadStatusWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Is WPF property changed (INotifyPropertyChanged stuff)
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes new instance of DownloadStatusWindow
        /// </summary>
        /// <param name="itemNames">Items to show on window</param>
        public DownloadStatusWindow(IEnumerable<string> itemNames)
        {
            InitializeComponent();

            this.DataContext = this;

            foreach (var item in itemNames)
            {
                var newItem = new ItemStatusInfo(item);
                this.Items.Add(newItem);
            }
        }

        /// <summary>
        /// Items which are downloaded
        /// </summary>
        public ObservableCollection<ItemStatusInfo> Items { get; private set; } =
            new ObservableCollection<ItemStatusInfo>();
    }
}
