using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Windows.Media;

namespace SimpleFTPClientGUI.DownloadStatus
{
    public enum ItemStatus { Neutral, Downloaded, InProgress, Failed }

    public class ItemStatusInfo
    {
        public string ItemName { get; private set;  }

        public Brush StatusColor { get; private set; }

        public ItemStatusInfo(string itemName, ItemStatus itemStatus = ItemStatus.Failed)
        {
            this.ItemName = itemName;
            this.SetItemStatus(itemStatus);
        }

        public void SetItemStatus(ItemStatus newStatus)
        {
            switch (newStatus)
            {
                case ItemStatus.Downloaded:
                    {
                        //this.StatusColor = Brushes.Green;
                        this.ItemName += " HUIPIZDA";
                        break;
                    }
                case ItemStatus.InProgress:
                    {
                        this.StatusColor = Brushes.Yellow;
                        this.ItemName += " LAKAKAL";
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
        }
    }
}
