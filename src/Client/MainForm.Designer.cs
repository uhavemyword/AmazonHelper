namespace Client
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.UriTextBox = new Telerik.WinControls.UI.RadTextBox();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.radStatusStrip1 = new Control.MyStatusStrip();
            this.radWaitingBarElement1 = new Telerik.WinControls.UI.RadWaitingBarElement();
            this.MessageLabel = new Telerik.WinControls.UI.RadLabelElement();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.UsageLabel = new Telerik.WinControls.UI.RadLabelElement();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.ShowTestFormButton = new Telerik.WinControls.UI.RadButton();
            this.ShowBrowserButton = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radRibbonBar1 = new Telerik.WinControls.UI.RadRibbonBar();
            this.radRibbonBarBackstageView1 = new Telerik.WinControls.UI.RadRibbonBarBackstageView();
            this.ribbonTab1 = new Telerik.WinControls.UI.RibbonTab();
            this.radRibbonBarGroup1 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.StartButton = new Telerik.WinControls.UI.RadButtonElement();
            this.PauseButton = new Telerik.WinControls.UI.RadButtonElement();
            this.StopButton = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonBarGroup2 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radDropDownButtonElement1 = new Telerik.WinControls.UI.RadDropDownButtonElement();
            this.StartGettingImageMenuItem = new Telerik.WinControls.UI.RadMenuItem();
            this.StopGettingImageMenuItem = new Telerik.WinControls.UI.RadMenuItem();
            this.radDropDownButtonElement2 = new Telerik.WinControls.UI.RadDropDownButtonElement();
            this.StartGettingPriceMenuItem = new Telerik.WinControls.UI.RadMenuItem();
            this.StopGettingPriceMenuItem = new Telerik.WinControls.UI.RadMenuItem();
            this.radRibbonFormBehavior1 = new Telerik.WinControls.UI.RadRibbonFormBehavior();
            ((System.ComponentModel.ISupportInitialize)(this.UriTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowTestFormButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowBrowserButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBarBackstageView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // UriTextBox
            // 
            this.UriTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UriTextBox.Location = new System.Drawing.Point(57, 15);
            this.UriTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UriTextBox.Name = "UriTextBox";
            this.UriTextBox.Size = new System.Drawing.Size(1301, 23);
            this.UriTextBox.TabIndex = 0;
            this.UriTextBox.Text = "https://www.amazon.com/dp/B000CEOXTS";
            // 
            // radGridView1
            // 
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Location = new System.Drawing.Point(0, 218);
            this.radGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.EnableAlternatingRowColor = true;
            this.radGridView1.MasterTemplate.EnableGrouping = false;
            this.radGridView1.MasterTemplate.EnableSorting = false;
            this.radGridView1.MasterTemplate.MultiSelect = true;
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Size = new System.Drawing.Size(1376, 748);
            this.radGridView1.TabIndex = 2;
            this.radGridView1.Text = "radGridView1";
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radWaitingBarElement1,
            this.MessageLabel,
            this.commandBarSeparator1,
            this.UsageLabel});
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 966);
            this.radStatusStrip1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(1376, 29);
            this.radStatusStrip1.SizingGrip = false;
            this.radStatusStrip1.TabIndex = 3;
            this.radStatusStrip1.Text = "radStatusStrip1";
            // 
            // radWaitingBarElement1
            // 
            this.radWaitingBarElement1.Name = "radWaitingBarElement1";
            // 
            // 
            // 
            this.radWaitingBarElement1.SeparatorElement.Dash = false;
            this.radStatusStrip1.SetSpring(this.radWaitingBarElement1, false);
            this.radWaitingBarElement1.Text = "";
            ((Telerik.WinControls.UI.WaitingBarSeparatorElement)(this.radWaitingBarElement1.GetChildAt(0).GetChildAt(0))).Dash = false;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Name = "MessageLabel";
            this.radStatusStrip1.SetSpring(this.MessageLabel, true);
            this.MessageLabel.Text = "Message";
            this.MessageLabel.TextWrap = true;
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.radStatusStrip1.SetSpring(this.commandBarSeparator1, false);
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // UsageLabel
            // 
            this.UsageLabel.Image = global::Client.Properties.Resources.bar_chart;
            this.UsageLabel.Name = "UsageLabel";
            this.radStatusStrip1.SetSpring(this.UsageLabel, false);
            this.UsageLabel.Text = "CPU 0% Memory 0MB";
            this.UsageLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.UsageLabel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.UsageLabel.TextWrap = true;
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.ShowTestFormButton);
            this.radPanel1.Controls.Add(this.ShowBrowserButton);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Controls.Add(this.UriTextBox);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 162);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1376, 56);
            this.radPanel1.TabIndex = 5;
            // 
            // ShowTestFormButton
            // 
            this.ShowTestFormButton.Location = new System.Drawing.Point(932, 37);
            this.ShowTestFormButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShowTestFormButton.Name = "ShowTestFormButton";
            this.ShowTestFormButton.Size = new System.Drawing.Size(101, 28);
            this.ShowTestFormButton.TabIndex = 5;
            this.ShowTestFormButton.Text = "Test Form";
            this.ShowTestFormButton.Visible = false;
            this.ShowTestFormButton.Click += new System.EventHandler(this.ShowTestFormButton_Click);
            // 
            // ShowBrowserButton
            // 
            this.ShowBrowserButton.Location = new System.Drawing.Point(825, 37);
            this.ShowBrowserButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShowBrowserButton.Name = "ShowBrowserButton";
            this.ShowBrowserButton.Size = new System.Drawing.Size(101, 28);
            this.ShowBrowserButton.TabIndex = 4;
            this.ShowBrowserButton.Text = "Show Browser";
            this.ShowBrowserButton.Visible = false;
            this.ShowBrowserButton.Click += new System.EventHandler(this.ShowBrowserButton_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(16, 16);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(31, 21);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "URL";
            // 
            // radRibbonBar1
            // 
            this.radRibbonBar1.BackstageControl = this.radRibbonBarBackstageView1;
            this.radRibbonBar1.CommandTabs.AddRange(new Telerik.WinControls.RadItem[] {
            this.ribbonTab1});
            // 
            // 
            // 
            this.radRibbonBar1.ExitButton.Text = "Exit";
            this.radRibbonBar1.Location = new System.Drawing.Point(0, 0);
            this.radRibbonBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radRibbonBar1.Name = "radRibbonBar1";
            // 
            // 
            // 
            this.radRibbonBar1.OptionsButton.Text = "Options";
            this.radRibbonBar1.Size = new System.Drawing.Size(1376, 162);
            this.radRibbonBar1.StartButtonImage = global::Client.Properties.Resources.home;
            this.radRibbonBar1.TabIndex = 6;
            this.radRibbonBar1.Text = "Amazon Helper";
            // 
            // radRibbonBarBackstageView1
            // 
            this.radRibbonBarBackstageView1.EnableKeyMap = true;
            this.radRibbonBarBackstageView1.Location = new System.Drawing.Point(0, 58);
            this.radRibbonBarBackstageView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radRibbonBarBackstageView1.Name = "radRibbonBarBackstageView1";
            this.radRibbonBarBackstageView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radRibbonBarBackstageView1.SelectedItem = null;
            this.radRibbonBarBackstageView1.Size = new System.Drawing.Size(1016, 705);
            this.radRibbonBarBackstageView1.TabIndex = 7;
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.IsSelected = true;
            this.ribbonTab1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radRibbonBarGroup1,
            this.radRibbonBarGroup2});
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Text = "Home";
            // 
            // radRibbonBarGroup1
            // 
            this.radRibbonBarGroup1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.StartButton,
            this.PauseButton,
            this.StopButton});
            this.radRibbonBarGroup1.Name = "radRibbonBarGroup1";
            this.radRibbonBarGroup1.Text = "";
            // 
            // StartButton
            // 
            this.StartButton.AngleTransform = 0F;
            this.StartButton.AutoSize = true;
            this.StartButton.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
            this.StartButton.Image = global::Client.Properties.Resources.start;
            this.StartButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.StartButton.Name = "StartButton";
            this.StartButton.Opacity = 1D;
            this.StartButton.RightToLeft = false;
            this.StartButton.ScaleTransform = new System.Drawing.SizeF(1F, 1F);
            this.StartButton.Text = "Start";
            this.StartButton.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.StartButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Image = global::Client.Properties.Resources.pause;
            this.PauseButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Text = "Pause";
            this.PauseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Image = global::Client.Properties.Resources.stop;
            this.StopButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.StopButton.Name = "StopButton";
            this.StopButton.Text = "Stop";
            this.StopButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // radRibbonBarGroup2
            // 
            this.radRibbonBarGroup2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radDropDownButtonElement1,
            this.radDropDownButtonElement2});
            this.radRibbonBarGroup2.Name = "radRibbonBarGroup2";
            this.radRibbonBarGroup2.Text = "";
            // 
            // radDropDownButtonElement1
            // 
            this.radDropDownButtonElement1.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.radDropDownButtonElement1.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.radDropDownButtonElement1.ExpandArrowButton = false;
            this.radDropDownButtonElement1.Image = global::Client.Properties.Resources.picture;
            this.radDropDownButtonElement1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radDropDownButtonElement1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.StartGettingImageMenuItem,
            this.StopGettingImageMenuItem});
            this.radDropDownButtonElement1.Name = "radDropDownButtonElement1";
            this.radDropDownButtonElement1.Text = "Get Images";
            this.radDropDownButtonElement1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radDropDownButtonElement1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // StartGettingImageMenuItem
            // 
            this.StartGettingImageMenuItem.Image = global::Client.Properties.Resources.start_small;
            this.StartGettingImageMenuItem.Name = "StartGettingImageMenuItem";
            this.StartGettingImageMenuItem.Text = "Start";
            // 
            // StopGettingImageMenuItem
            // 
            this.StopGettingImageMenuItem.Image = global::Client.Properties.Resources.stop_small;
            this.StopGettingImageMenuItem.Name = "StopGettingImageMenuItem";
            this.StopGettingImageMenuItem.Text = "Stop";
            // 
            // radDropDownButtonElement2
            // 
            this.radDropDownButtonElement2.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.radDropDownButtonElement2.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.radDropDownButtonElement2.ExpandArrowButton = false;
            this.radDropDownButtonElement2.Image = global::Client.Properties.Resources.price;
            this.radDropDownButtonElement2.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radDropDownButtonElement2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.StartGettingPriceMenuItem,
            this.StopGettingPriceMenuItem});
            this.radDropDownButtonElement2.Name = "radDropDownButtonElement2";
            this.radDropDownButtonElement2.Text = "Get Prices";
            this.radDropDownButtonElement2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // StartGettingPriceMenuItem
            // 
            this.StartGettingPriceMenuItem.Image = global::Client.Properties.Resources.start_small;
            this.StartGettingPriceMenuItem.Name = "StartGettingPriceMenuItem";
            this.StartGettingPriceMenuItem.Text = "Start";
            // 
            // StopGettingPriceMenuItem
            // 
            this.StopGettingPriceMenuItem.Image = global::Client.Properties.Resources.stop_small;
            this.StopGettingPriceMenuItem.Name = "StopGettingPriceMenuItem";
            this.StopGettingPriceMenuItem.Text = "Stop";
            // 
            // radRibbonFormBehavior1
            // 
            this.radRibbonFormBehavior1.Form = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 995);
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radStatusStrip1);
            this.Controls.Add(this.radRibbonBar1);
            this.Controls.Add(this.radRibbonBarBackstageView1);
            this.FormBehavior = this.radRibbonFormBehavior1;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IconScaling = Telerik.WinControls.Enumerations.ImageScaling.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1371, 952);
            this.Name = "MainForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Amazon Helper";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.RadMainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.UriTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowTestFormButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowBrowserButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBarBackstageView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox UriTextBox;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Control.MyStatusStrip radStatusStrip1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadWaitingBarElement radWaitingBarElement1;
        private Telerik.WinControls.UI.RadLabelElement MessageLabel;
        private Telerik.WinControls.UI.RadLabelElement UsageLabel;
        private Telerik.WinControls.UI.RadButton ShowBrowserButton;
        private Telerik.WinControls.UI.RadButton ShowTestFormButton;
        private Telerik.WinControls.UI.RadRibbonBar radRibbonBar1;
        private Telerik.WinControls.UI.RadRibbonFormBehavior radRibbonFormBehavior1;
        private Telerik.WinControls.UI.RibbonTab ribbonTab1;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup1;
        private Telerik.WinControls.UI.RadButtonElement StartButton;
        private Telerik.WinControls.UI.RadButtonElement PauseButton;
        private Telerik.WinControls.UI.RadButtonElement StopButton;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.RadRibbonBarBackstageView radRibbonBarBackstageView1;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup2;
        private Telerik.WinControls.UI.RadDropDownButtonElement radDropDownButtonElement1;
        private Telerik.WinControls.UI.RadMenuItem StartGettingImageMenuItem;
        private Telerik.WinControls.UI.RadMenuItem StopGettingImageMenuItem;
        private Telerik.WinControls.UI.RadDropDownButtonElement radDropDownButtonElement2;
        private Telerik.WinControls.UI.RadMenuItem StartGettingPriceMenuItem;
        private Telerik.WinControls.UI.RadMenuItem StopGettingPriceMenuItem;
    }
}
