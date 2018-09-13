namespace Problem2
{
    partial class WatchesForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода. (I'll think about it)
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SecondTimer = new System.Windows.Forms.Timer(this.components);
            this.SPBWatchesLabel = new System.Windows.Forms.Label();
            this.UTCWatchesLabel = new System.Windows.Forms.Label();
            this.NYWatchesLabel = new System.Windows.Forms.Label();
            this.NYWatches = new WatchesControl.AnalogWatches();
            this.UTCWatches = new WatchesControl.AnalogWatches();
            this.SPBWatches = new WatchesControl.AnalogWatches();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SecondTimer
            // 
            this.SecondTimer.Enabled = true;
            this.SecondTimer.Interval = 1000;
            this.SecondTimer.Tick += new System.EventHandler(this.OnTick);
            // 
            // SPBWatchesLabel
            // 
            this.SPBWatchesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SPBWatchesLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SPBWatchesLabel.ForeColor = System.Drawing.Color.Black;
            this.SPBWatchesLabel.Location = new System.Drawing.Point(3, 371);
            this.SPBWatchesLabel.Name = "SPBWatchesLabel";
            this.SPBWatchesLabel.Size = new System.Drawing.Size(277, 43);
            this.SPBWatchesLabel.TabIndex = 2;
            this.SPBWatchesLabel.Text = "Saint Petersburg";
            this.SPBWatchesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UTCWatchesLabel
            // 
            this.UTCWatchesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UTCWatchesLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UTCWatchesLabel.Location = new System.Drawing.Point(286, 165);
            this.UTCWatchesLabel.Name = "UTCWatchesLabel";
            this.UTCWatchesLabel.Size = new System.Drawing.Size(277, 41);
            this.UTCWatchesLabel.TabIndex = 3;
            this.UTCWatchesLabel.Text = "UTC";
            this.UTCWatchesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NYWatchesLabel
            // 
            this.NYWatchesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NYWatchesLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NYWatchesLabel.Location = new System.Drawing.Point(286, 371);
            this.NYWatchesLabel.Name = "NYWatchesLabel";
            this.NYWatchesLabel.Size = new System.Drawing.Size(277, 43);
            this.NYWatchesLabel.TabIndex = 5;
            this.NYWatchesLabel.Text = "New York";
            this.NYWatchesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NYWatches
            // 
            this.NYWatches.CurrentDateTime = new System.DateTime(2018, 4, 4, 16, 58, 10, 902);
            this.NYWatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NYWatches.Location = new System.Drawing.Point(286, 209);
            this.NYWatches.Name = "NYWatches";
            this.NYWatches.Size = new System.Drawing.Size(277, 159);
            this.NYWatches.TabIndex = 4;
            this.NYWatches.Text = "analogWatches1";
            // 
            // UTCWatches
            // 
            this.UTCWatches.CurrentDateTime = new System.DateTime(2018, 4, 4, 13, 55, 42, 146);
            this.UTCWatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UTCWatches.Location = new System.Drawing.Point(286, 3);
            this.UTCWatches.Name = "UTCWatches";
            this.UTCWatches.Size = new System.Drawing.Size(277, 159);
            this.UTCWatches.TabIndex = 1;
            // 
            // SPBWatches
            // 
            this.SPBWatches.CurrentDateTime = new System.DateTime(2018, 4, 4, 16, 51, 4, 888);
            this.SPBWatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SPBWatches.Location = new System.Drawing.Point(3, 3);
            this.SPBWatches.Name = "SPBWatches";
            this.tableLayoutPanel1.SetRowSpan(this.SPBWatches, 3);
            this.SPBWatches.Size = new System.Drawing.Size(277, 365);
            this.SPBWatches.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.SPBWatches, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.NYWatchesLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.UTCWatches, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.NYWatches, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.UTCWatchesLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.SPBWatchesLabel, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-3, -1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(566, 414);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // WatchesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 412);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(578, 451);
            this.Name = "WatchesForm";
            this.Text = "Watches";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private WatchesControl.AnalogWatches SPBWatches;
        private System.Windows.Forms.Timer SecondTimer;
        private WatchesControl.AnalogWatches UTCWatches;
        private System.Windows.Forms.Label SPBWatchesLabel;
        private System.Windows.Forms.Label UTCWatchesLabel;
        private WatchesControl.AnalogWatches NYWatches;
        private System.Windows.Forms.Label NYWatchesLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

