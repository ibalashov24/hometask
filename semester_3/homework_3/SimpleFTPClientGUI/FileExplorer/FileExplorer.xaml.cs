using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;

namespace SimpleFTPClientGUI.FileExplorer
{
    /// <summary>
    /// Логика взаимодействия для FileExplorer.xaml
    /// </summary>
    public partial class FileExplorer : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Selected items from the list
        /// </summary>
        public List<ItemInfo> SelectedItems
        {
            get => (List<ItemInfo>)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        /// <summary>
        /// Item list content
        /// </summary>
        public new List<ItemInfo> Content
        {
            get => (List<ItemInfo>)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        /// <summary>
        /// Item list content
        /// </summary>
        public static readonly new DependencyProperty ContentProperty = 
            DependencyProperty.Register(
            "Content",
            typeof(List<ItemInfo>),
            typeof(FileExplorer),
            new PropertyMetadata(ContentUpdated));

        /// <summary>
        /// Selected items in the list
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
            "SelectedItems",
            typeof(List<ItemInfo>),
            typeof(FileExplorer),
            new PropertyMetadata(null));

        /// <summary>
        /// Are some properties changed?
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// User requested opening of selected item
        /// </summary>
        public event EventHandler ItemRequested;

        /// <summary>
        /// Initializes new instance of FileExplorer
        /// </summary>
        public FileExplorer()
        {
            InitializeComponent();

            this.ItemList.SelectionChanged += UserClickedOnFileExplorer;
        }

        /// <summary>
        /// Adds new item to item list
        /// </summary>
        /// <param name="item">New item</param>
        private void AddItemToList(ItemInfo item)
        {
            this.ItemList.Items.Add(item);
        }

        /// <summary>
        /// Clears item list
        /// </summary>
        private void ClearList()
        {
            this.ItemList.Items.Clear();
        }

        /// <summary>
        /// Handles situation when content in the item list was updated using xaml property
        /// </summary>
        /// <param name="obj">Property</param>
        /// <param name="e">Information about change</param>
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

        /// <summary>
        /// Handles situation when user selected item in the list
        /// </summary>
        /// <param name="sender">Selected item</param>
        /// <param name="e">Information about selection</param>
        private void UserClickedOnFileExplorer(object sender, SelectionChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            SetValue(
                SelectedItemsProperty,
                new List<ItemInfo>() { (ItemInfo)this.ItemList.SelectedItem });
        }

        /// <summary>
        /// Handles situation when user requested opening of selected item
        /// </summary>
        private void UserRequestedItem(object sender, MouseButtonEventArgs e)
        {
            var clickedItem = (ItemInfo)((ListViewItem)sender).Content;
            this.ItemRequested?.Invoke(this, new ItemInfoEventArgs(clickedItem));
        }
    }   
}