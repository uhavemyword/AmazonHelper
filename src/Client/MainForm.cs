using Abot.Core;
using Abot.Crawler;
using Abot.Poco;
using Abot.Util;
using Common;
using Control;
using Library;
using Library.CrawlerCustomization;
using Library.Model;
using log4net;
using log4net.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Client
{
    public partial class MainForm : MyForm
    {
        private static ILog _logger = LogManager.GetLogger("AmazonHelper");
        private PerformanceMonitor _monitor = new PerformanceMonitor();
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private CancellationTokenSource _crawlerCancellationTS = new CancellationTokenSource();
        private Task _crawlerTask;
        private IThreadManager _threadManager;
        private object _captchaLock = new object();
        private int _scrollToRowOffset = 5;
        private AmazonHelper _amazonHelper = new AmazonHelper();

        public MainForm()
        {
            InitializeComponent();
            this.radWaitingBarElement1.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            this.MessageLabel.Text = string.Empty;
            this.UsageLabel.Text = string.Empty;
            this._monitor.OverallUsageChanged += Monitor_OverallUsageChanged;
            NotifyUIOnStatusChange(false);
            StartGettingImageMenuItem.Enabled = false;
            HandleImage();
            HandlePrice();
        }

        private void RadMainForm_Shown(object sender, EventArgs e)
        {
            var log = LogManager.GetRepository().GetAppenders().OfType<MyLoggingAppender>().FirstOrDefault();
            if (log != null)
            {
                log.Logged += Log_Logged;
            }

            InitGrid();
            this._monitor.Monitor();
        }

        private void Monitor_OverallUsageChanged(int cpuPercentage, decimal memoryUsage)
        {
            InvokeIfRequired(() =>
            {
                this.UsageLabel.Text = string.Format("CPU {0}% Memory {1:N}MB Rows {2}", cpuPercentage.ToString().PadLeft(2, ' '), memoryUsage / 1024, this.radGridView1.Rows.Count);
            });
        }

        private void Log_Logged(LoggingEvent loggingEvent)
        {
            if (loggingEvent.MessageObject != null && !string.IsNullOrEmpty(loggingEvent.MessageObject.ToString()))
            {
                InvokeIfRequired(() =>
                {
                    //MessageLabel.Text = string.Join(string.Empty, loggingEvent.RenderedMessage.Take(100));
                    MessageLabel.Text = string.Format("{0} [{1}] {2} {3} - {4}", loggingEvent.TimeStamp, loggingEvent.ThreadName, loggingEvent.LoggerName, loggingEvent.Level, loggingEvent.MessageObject);
                    //Application.DoEvents();
                });
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Start()
        {
            _logger.Info("Starting...");
            var url = this.UriTextBox.Text.Trim();

            if (_threadManager == null)
            {
                _threadManager = new TaskThreadManager(10);
                var crawler = CreateCrawler(_threadManager);
                _crawlerCancellationTS = new CancellationTokenSource();
                _products.Clear();
                _crawlerTask = new Task(() =>
                {
                    CrawlResult result = crawler.Crawl(new Uri(url), _crawlerCancellationTS);
                    OnCrawlerCompleted();
                }, _crawlerCancellationTS.Token);
                _crawlerTask.Start();
            }
            else
            {
                _threadManager.Resume();
            }

            NotifyUIOnStatusChange(true);
        }

        private void OnCrawlerCompleted()
        {
            InvokeIfRequired(() =>
            {
                NotifyUIOnStatusChange(false);
                this.radGridView1.MasterTemplate.BestFitColumns();
                for (int i = 0; i < this.radGridView1.Columns.Count; i++)
                {
                    this.radGridView1.Columns[i].Width += 10;
                }
            });
            _threadManager = null;
        }

        private void Pause()
        {
            _threadManager.Pause();
            NotifyUIOnStatusChange(false);
        }

        private void Stop()
        {
            _logger.Info("Stopping");
            _crawlerCancellationTS.Cancel();
        }

        private void NotifyUIOnStatusChange(bool started)
        {
            this.StartButton.Enabled = !started;
            this.StopButton.Enabled = started;
            this.PauseButton.Enabled = started;
            this.radGridView1.ReadOnly = started;
            if (started)
            {
                this.radWaitingBarElement1.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                this.radWaitingBarElement1.StartWaiting();
            }
            else
            {
                this.radWaitingBarElement1.StopWaiting();
                this.radWaitingBarElement1.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            }
        }

        #region Crawler

        private WebCrawler CreateCrawler(IThreadManager threadManager)
        {
            CrawlConfiguration crawlConfig = AbotConfigurationSectionHandler.LoadFromXml().Convert();
            crawlConfig.MaxConcurrentThreads = 10;//this overrides the config value
            crawlConfig.MaxCrawlDepth = 3;

            //Will use the manually created crawlConfig object created above
            PoliteWebCrawler crawler = new PoliteWebCrawler(crawlConfig, new AmazonPageDecisionMaker(_amazonHelper), threadManager, null, new AmazonPageRequester(crawlConfig), new AmazonHyperLinkParser(_amazonHelper), null, null, null);

            crawler.PageCrawlStarting += crawler_ProcessPageCrawlStarting;
            crawler.PageCrawlCompleted += crawler_ProcessPageCrawlCompleted;
            crawler.PageCrawlDisallowed += crawler_PageCrawlDisallowed;
            crawler.PageLinksCrawlDisallowed += crawler_PageLinksCrawlDisallowed;
            return crawler;
        }

        private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            _logger.InfoFormat("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
        }

        private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            if (_amazonHelper.IsCaptchaPage(crawledPage))
            {
                lock (_captchaLock)
                {
                    InvokeIfRequired(() =>
                    {
                        var form = new BrowserForm();
                        form.Browser.Navigate(crawledPage.Uri);
                        form.ShowDialog();
                    });
                }
            }

            var products = _amazonHelper.GetProductsFromDetailPage(crawledPage);
            foreach (var p in products)
            {
                if (!_products.Any(x => x.external_product_id == p.external_product_id))
                {
                    InvokeIfRequired(() =>
                    {
                        _products.Add(p);
                        Application.DoEvents();
                    });
                }
            }

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                _logger.InfoFormat("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
            else
                _logger.InfoFormat("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                _logger.InfoFormat("Page had no content {0}", crawledPage.Uri.AbsoluteUri);
        }

        private void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            _logger.InfoFormat("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
        }

        private void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            _logger.InfoFormat("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
        }

        #endregion Crawler

        private void BeginInvokeIfRequired(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
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
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        #region GridView

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

        private void AddProductToGrid()
        {
        }

        #endregion GridView

        private void HandleImage()
        {
            var task = new MyTaskPlayer(SynchronizationContext.Current, StartGettingImageMenuItem, null, StopGettingImageMenuItem);
            task.Init(() =>
            {
                foreach (var row in this.radGridView1.MasterView.Rows.ToList())
                {
                    InvokeIfRequired(() =>
                    {
                        this.radGridView1.ClearSelection();
                        this.radGridView1.TableElement.ScrollToRow(row.Index > _scrollToRowOffset ? row.Index - _scrollToRowOffset : 0);
                        row.IsSelected = true;
                    });
                    var product = row.DataBoundItem as Product;
                    try
                    {
                        task.MonitorSignal();
                        if (row.Cells["image"].Value == null)
                        {
                            byte[] image = _amazonHelper.GetThumbImage(product, 150);

                            InvokeIfRequired(() =>
                            {
                                row.Cells["image"].Value = image;
                                row.Height = 80;
                            });
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.Info("Operation canceled while downloading images.");
                        return;
                    }
                    catch (Exception ex)
                    {
                        _logger.ErrorFormat("Failed to get image for product {0}, error: {1}", product.external_product_id, ex.Message);
                    }
                }
            });
        }

        private void HandlePrice()
        {
            var task = new MyTaskPlayer(SynchronizationContext.Current, StartGettingPriceMenuItem, null, StopGettingPriceMenuItem);
            task.Init(() =>
            {
                foreach (var row in this.radGridView1.MasterView.Rows.ToList())
                {
                    InvokeIfRequired(() =>
                    {
                        this.radGridView1.ClearSelection();
                        this.radGridView1.TableElement.ScrollToRow(row.Index > _scrollToRowOffset ? row.Index - _scrollToRowOffset : 0);
                        row.IsSelected = true;
                    });
                    var product = row.DataBoundItem as Product;
                    try
                    {
                        task.MonitorSignal();
                        if (row.Cells["real_price"].Value == null)
                        {
                            var price = _amazonHelper.GetLowestOfferPrice(product);
                            InvokeIfRequired(() =>
                            {
                                row.Cells["real_price"].Value = price;
                            });
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.Info("Operation canceled while getting prices.");
                        return;
                    }
                    catch (Exception ex)
                    {
                        _logger.ErrorFormat("Failed to get price for product {0}, error: {1}", product.external_product_id, ex.Message);
                    }
                }
            });
        }

        private void ShowBrowserButton_Click(object sender, EventArgs e)
        {
            var form = new BrowserForm();
            form.Show();
        }

        private void ShowTestFormButton_Click(object sender, EventArgs e)
        {
            var form = new TestForm();
            form.ShowDialog();
        }
    }
}