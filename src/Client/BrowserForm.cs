using System;
using System.Windows.Forms;

namespace Client
{
    public partial class BrowserForm : Form
    {
        public BrowserForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browser.Navigate(this.textBox1.Text);
        }
    }
}