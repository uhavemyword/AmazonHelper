namespace Client
{
    partial class TestForm
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.GetPriceButton = new Telerik.WinControls.UI.RadButton();
            this.radTextBox1 = new Telerik.WinControls.UI.RadTextBox();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.UpdatePriceButton = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GetPriceButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdatePriceButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.UpdatePriceButton);
            this.radPanel1.Controls.Add(this.GetPriceButton);
            this.radPanel1.Controls.Add(this.radTextBox1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1071, 65);
            this.radPanel1.TabIndex = 0;
            this.radPanel1.Text = "radPanel1";
            // 
            // GetPriceButton
            // 
            this.GetPriceButton.Location = new System.Drawing.Point(562, 27);
            this.GetPriceButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GetPriceButton.Name = "GetPriceButton";
            this.GetPriceButton.Size = new System.Drawing.Size(101, 28);
            this.GetPriceButton.TabIndex = 4;
            this.GetPriceButton.Text = "Get Prices";
            this.GetPriceButton.Click += new System.EventHandler(this.GetPriceButton_Click_1);
            // 
            // radTextBox1
            // 
            this.radTextBox1.Location = new System.Drawing.Point(83, 27);
            this.radTextBox1.Name = "radTextBox1";
            this.radTextBox1.Size = new System.Drawing.Size(341, 23);
            this.radTextBox1.TabIndex = 0;
            // 
            // radGridView1
            // 
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Location = new System.Drawing.Point(0, 65);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.MultiSelect = true;
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Size = new System.Drawing.Size(1071, 641);
            this.radGridView1.TabIndex = 1;
            this.radGridView1.Text = "radGridView1";
            // 
            // UpdatePriceButton
            // 
            this.UpdatePriceButton.Location = new System.Drawing.Point(735, 27);
            this.UpdatePriceButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UpdatePriceButton.Name = "UpdatePriceButton";
            this.UpdatePriceButton.Size = new System.Drawing.Size(101, 28);
            this.UpdatePriceButton.TabIndex = 5;
            this.UpdatePriceButton.Text = "Update Prices";
            this.UpdatePriceButton.Click += new System.EventHandler(this.UpdatePriceButton_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 706);
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.radPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TestForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "TestForm";
            this.Shown += new System.EventHandler(this.TestForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GetPriceButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdatePriceButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadTextBox radTextBox1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.RadButton GetPriceButton;
        private Telerik.WinControls.UI.RadButton UpdatePriceButton;
    }
}
