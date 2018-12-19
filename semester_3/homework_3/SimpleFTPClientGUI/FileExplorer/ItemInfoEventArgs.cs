using System;

namespace SimpleFTPClientGUI.FileExplorer
{
    public class ItemInfoEventArgs : EventArgs
    {
        public ItemInfoEventArgs(ItemInfo itemInfo)
        {
            this.ItemInfo = itemInfo;
        }

        public ItemInfo ItemInfo { get; }
    }
}
