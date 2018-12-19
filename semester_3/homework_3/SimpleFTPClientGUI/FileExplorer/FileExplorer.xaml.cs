using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace SimpleFTPClientGUI.FileExplorer
{
    /// <summary>
    /// Логика взаимодействия для FileExplorer.xaml
    /// </summary>
    public partial class FileExplorer : UserControl, INotifyPropertyChanged
    {
        public List<ItemInfo> SelectedItems
        {
            get => (List<ItemInfo>)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        public new List<ItemInfo> Content
        {
            get => (List<ItemInfo>)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly new DependencyProperty ContentProperty = 
            DependencyProperty.Register(
            "Content",
            typeof(List<ItemInfo>),
            typeof(FileExplorer),
            new PropertyMetadata(ContentUpdated));

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
            "SelectedItems",
            typeof(List<ItemInfo>),
            typeof(FileExplorer),
            new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler ItemRequested;

        public FileExplorer()
        {
            InitializeComponent();

            this.ItemList.SelectionChanged += UserClickedOnFileExplorer;
        }

        private void AddItemToList(ItemInfo item)
        {
            this.ItemList.Items.Add(item);
        }

        private void ClearList()
        {
            this.ItemList.Items.Clear();
        }

        private static void ContentUpdated(
            DependencyObject obj, 
            DependencyPropertyChangedEventArgs e)
        {
            var explorer = (FileExplorer)obj;

            explorer.ClearList();
            foreach (var item in (List<ItemInfo>)e.NewValue)
            {
                explorer.AddItemToList(item);
            }
        }

        private void UserClickedOnFileExplorer(object sender, SelectionChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            SetValue(
                SelectedItemsProperty,
                new List<ItemInfo>() { (ItemInfo)this.ItemList.SelectedItem });
        }

        private void UserRequestedItem(object sender, MouseButtonEventArgs e)
        {
            var clickedItem = (ItemInfo)((ListViewItem)sender).Content;
            this.ItemRequested?.Invoke(this, new ItemInfoEventArgs(clickedItem));
        }
    }   
}