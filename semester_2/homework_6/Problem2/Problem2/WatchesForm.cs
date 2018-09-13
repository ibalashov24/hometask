using System;
using System.Windows.Forms;

namespace Problem2
{
    public partial class WatchesForm : Form
    {
        public WatchesForm()
        {
            this.InitializeComponent();
        }

        private void OnTick(object sender, EventArgs e)
        {
            this.SPBWatches.CurrentDateTime = DateTime.Now;
            this.NYWatches.CurrentDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
                DateTime.Now,
                @"Eastern Standard Time");
            this.UTCWatches.CurrentDateTime = DateTime.UtcNow;
        }
    }
}
