using Common;
using Control;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Client
{
    public partial class TestForm : MyForm
    {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private PerformanceMonitor _monitor;

        public TestForm()
        {
            InitializeComponent();
            this._monitor = new PerformanceMonitor();
            this._monitor.OverallUsageChanged += Monitor_OverallUsageChanged;
        }

        private void TestForm_Shown(object sender, EventArgs e)
        {
            InitGrid();
            this._monitor.Monitor(100);
        }

        private void Monitor_OverallUsageChanged(int cpuPercentage, decimal memoryUsage)
        {
            BeginInvokeIfRequired(() =>
            {
                this.Text = string.Format("CPU {0}% Memory {1:N}MB Rows {2}", cpuPercentage.ToString().PadLeft(2, ' '), memoryUsage / 1024, this.radGridView1.Rows.Count);
            });
        }

        private void InitGrid()
        {
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AutoGenerateColumns = false;
            this.radGridView1.MasterTemplate.EnableFiltering = true;
            this.radGridView1.MasterTemplate.EnableSorting = true;
            this.radGridView1.MasterTemplate.EnableGrouping = true;
            this.radGridView1.DataSource = null;
            this.radGridView1.MasterTemplate.Columns.Clear();

            var imageColumn = new GridViewImageColumn("image", "image");
            imageColumn.HeaderText = "Image";
            imageColumn.ImageLayout = ImageLayout.Zoom;
            imageColumn.Width = 150;
            this.radGridView1.MasterTemplate.Columns.Add(imageColumn);

            var asinColumn = new GridViewTextBoxColumn("external_product_id", "external_product_id");
            asinColumn.HeaderText = "ASIN";

            this.radGridView1.MasterTemplate.Columns.Add(asinColumn);
            this.radGridView1.MasterTemplate.Columns.Add("item_name", "Name", "item_name");
            this.radGridView1.MasterTemplate.Columns.Add("price", "Price", "price");

            var priceColumn = new GridViewDecimalColumn("real_price", "real_price");
            priceColumn.HeaderText = "real_price";
            this.radGridView1.MasterTemplate.Columns.Add(priceColumn);

            this.radGridView1.MasterTemplate.Columns.Add("Dimensions", "Dimensions", "Dimensions");
            this.radGridView1.MasterTemplate.Columns.Add("Size", "Size", "Size");
            this.radGridView1.MasterTemplate.Columns.Add("brand_name", "Brand", "brand_name");
            this.radGridView1.MasterTemplate.Columns.Add("product_type_name", "Type", "product_type_name");
            this.radGridView1.MasterTemplate.Columns.Add("department_name", "Department", "department_name");

            var urlColumn = new GridViewHyperlinkColumn("Url", "Url");
            urlColumn.HeaderText = "Url";
            this.radGridView1.MasterTemplate.Columns.Add(urlColumn);

            for (int i = 1; i < radGridView1.MasterTemplate.Columns.Count; i++)
            {
                this.radGridView1.MasterTemplate.Columns[i].Width = 120;
            }
            this.radGridView1.DataSource = _products;
        }

        private void BeginInvokeIfRequired(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(action));
            }
            else
            {
                action();
            }
        }

        private void InvokeIfRequired(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(action));
            }
            else
            {
                action();
            }
        }

        #region Get Price

        private CancellationTokenSource _getPriceTokenSource;

        private void GetPriceButton_Click_1(object sender, EventArgs e)
        {
            if (GetPriceButton.Text == "Get Prices")
            {
                _getPriceTokenSource = new CancellationTokenSource();
                _getPriceTokenSource.Token.Register(GetPriceStopped);
                for (int i = 0; i < 10; i++)
                {
                    Task.Factory.StartNew(() =>
                    {
                        GetPrice(_getPriceTokenSource.Token);
                        GetPriceStopped();
                    }, _getPriceTokenSource.Token);
                }

                GetPriceStarted();
            }
            else
            {
                _getPriceTokenSource.Cancel();
                GetPriceStopped();
            }
        }

        private void GetPrice(CancellationToken token)
        {
            var count = int.Parse(this.radTextBox1.Text);
            var products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    var p = new Product();
                    p.brand_name = Path.GetRandomFileName();
                    p.department_name = Path.GetRandomFileName();
                    p.external_product_id = Path.GetRandomFileName();
                    p.price = Path.GetRandomFileName();
                    p.Url = Path.GetRandomFileName();
                    p.item_name = Path.GetRandomFileName();
                    products.Add(p);
                    InvokeIfRequired(() =>
                    {
                        _products.Add(p);
                        Application.DoEvents();
                    });
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }

        private void GetPriceStarted()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.GetPriceButton.Text = "Stop";
            }));
        }

        private void GetPriceStopped()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.GetPriceButton.Text = "Get Prices";
            }));
        }

        #endregion Get Price

        private object _syncObj = new object();

        private void UpdatePriceButton_Click(object sender, EventArgs e)
        {
            this.radGridView1.TableElement.RowScroller.ItemHeight = 50;
            Task.Factory.StartNew(() =>
            {
                lock (_syncObj)
                {
                    foreach (var row in this.radGridView1.Rows)
                    {
                        InvokeIfRequired(() =>
                        {
                            this.radGridView1.ClearSelection();
                            this.radGridView1.TableElement.ScrollToRow(row.Index > 5 ? row.Index - 5 : row.Index);
                            row.IsSelected = true;
                            row.Cells[2].Value = row.Cells[1].Value;
                        });
                        System.Threading.Thread.Sleep(100);
                    }
                }
            });
        }
    }
}