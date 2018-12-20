using System.Windows.Media;
using System.ComponentModel;

namespace SimpleFTPClientGUI.DownloadStatus
{
    /// <summary>
    /// Available downloading states
    /// </summary>
    public enum ItemStatus { Neutral, Downloaded, InProgress, Failed }

    /// <summary>
    /// Represents information about loaded item
    /// </summary>
    public class ItemStatusInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// Name of item
        /// </summary>
        public string ItemName { get; private set;  }

        /// <summary>
        /// Color of item which represents downloading status
        /// </summary>
        public Brush StatusColor { get; private set; }

        /// <summary>
        /// Is some of properties changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Ininitializes new instance of ItemStatusInfo
        /// </summary>
        /// <param name="itemName">New item name</param>
        /// <param name="itemStatus">New item status</param>
        public ItemStatusInfo(string itemName, ItemStatus itemStatus = ItemStatus.Neutral)
        {
            this.ItemName = itemName;
            this.SetItemStatus(itemStatus);
        }

        /// <summary>
        /// Sets new status of item
        /// </summary>
        /// <param name="newStatus"></param>
        public void SetItemStatus(ItemStatus newStatus)
        {
            switch (newStatus)
            {
                case ItemStatus.Downloaded:
                    {
                        this.StatusColor = Brushes.Green;
                        //this.ItemName += " HUIPIZDA";
                        break;
                    }
                case ItemStatus.InProgress:
                    {
                        this.StatusColor = Brushes.Yellow;
                        //this.ItemName += " LAKAKAL";
                        break;
                    }
                case ItemStatus.Failed:
                    {
                        this.StatusColor = Brushes.Red;
                        break;
                    }
                case ItemStatus.Neutral:
                    {
                        this.StatusColor = Brushes.Transparent;
                        break;
                    }
                default:
                    {
                        this.StatusColor = Brushes.Aquamarine;
                        break;
                    }
            }

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
