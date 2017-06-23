using Abot.Poco;
using Common;
using HtmlAgilityPack;
using Library.Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Library
{
    public class AmazonHelper : IAmazonHelper
    {
        public virtual bool IsCaptchaPage(CrawledPage crawledPage)
        {
            return crawledPage.Content.Text.Contains("discuss automated access");
        }

        public virtual bool IsProductDetailPage(CrawledPage crawledPage)
        {
            var htmlNode = crawledPage.HtmlDocument.DocumentNode;
            var nodes = htmlNode.SelectNodes("//div[@id='dp-container']");
            return nodes != null && nodes.Count == 1;
        }

        public virtual bool IsProductListPage(CrawledPage crawledPage)
        {
            var htmlNode = crawledPage.HtmlDocument.DocumentNode;
            var nodes = htmlNode.SelectNodes("//li[@data-asin]");
            return nodes != null && nodes.Count > 0;
        }

        public List<string> GetProductLinksFromListPage(CrawledPage crawledPage)
        {
            var links = new List<string>();
            var htmlNode = crawledPage.HtmlDocument.DocumentNode;
            var nodes = htmlNode.SelectNodes("//li[@data-asin]");
            foreach (var node in nodes)
            {
                var asin = node.Attributes["data-asin"].Value;
                links.Add(string.Format("{0}://{1}/dp/{2}", crawledPage.Uri.Scheme, crawledPage.Uri.Host, asin));
            }
            return links;
        }

        public virtual HtmlNodeCollection GetPageLinkNodesFromListPage(CrawledPage crawledPage)
        {
            var htmlNode = crawledPage.HtmlDocument.DocumentNode;
            var nodes = htmlNode.SelectNodes("//*[@id='pagn']//a");
            return nodes;
        }

        public virtual List<Product> GetProductsFromDetailPage(CrawledPage crawledPage)
        {
            var products = new List<Product>();
            var htmlNode = crawledPage.HtmlDocument.DocumentNode;
            if (IsProductDetailPage(crawledPage))
            {
                if (htmlNode.InnerHtml.Contains("isTwisterPage = 1;"))
                {
                    products.AddRange(GetTwisterProducts(crawledPage));
                }
                else
                {
                    var p = GetSingleProduct(crawledPage);
                    if (p != null)
                    {
                        products.Add(p);
                    }
                }
            }

            return products;
        }

        private Product GetSingleProduct(CrawledPage crawledPage)
        {
            var product = new Product();
            FillProductInfo(crawledPage, product);
            return product;
        }

        private List<Product> GetTwisterProducts(CrawledPage crawledPage)
        {
            var products = new List<Product>();
            string html = crawledPage.HtmlDocument.DocumentNode.InnerHtml;
            string pattern = @"'variationValues' : [\s\S]+?'asinVariationValues' : [\s\S]+?'dimensionValuesData'".Replace("'", "\"");
            Match match = Regex.Match(html, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                JObject variantionData = JObject.Parse("{" + match.Value + ":{}}");
                List<JToken> variationValues = variantionData["variationValues"].ToList(); //"variationValues" : {"size_name":["6.5 M US Big Kid","6.5Y","7 M US Big Kid"],"color_name":["Hyper Cblt/Deep Royal Blue/Vrsty Mz/B","Gym Red/White/Dark Grey"]}
                List<JToken> asinVariationValues = variantionData["asinVariationValues"].ToList(); //"asinVariationValues":{"B011EJGTCS":{"size_name":"13","ASIN":"B011EJGTCS","color_name":"1"},"B00YDN2RMO":{"size_name":"18","ASIN":"B00YDN2RMO","color_name":"0"}}

                var dimensionDict = new Dictionary<string, List<string>>();
                foreach (JProperty variationValue in variationValues)
                {
                    var name = variationValue.Name;
                    dimensionDict[name] = variationValue.Value.Select(x => (string)x).ToList();
                }

                JToken colorImages = null;
                match = Regex.Match(html, @"data\['colorImages'\] = ([\s\S]+?);".Replace("'", "\""), RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    colorImages = JObject.Parse(match.Groups[1].Value);
                }

                foreach (JProperty asinVariationValue in asinVariationValues)
                {
                    var product = new Product();
                    FillProductInfo(crawledPage, product);
                    product.external_product_id = asinVariationValue.Name;
                    product.Url = string.Format("{0}://{1}/dp/{2}?psc=1", crawledPage.Uri.Scheme, crawledPage.Uri.Host, product.external_product_id);
                    foreach (JProperty variation_value in asinVariationValue.Value)
                    {
                        if (dimensionDict.Keys.Contains(variation_value.Name))
                        {
                            var dimension = new Dimension();
                            dimension.Name = variation_value.Name;
                            dimension.Value = dimensionDict[variation_value.Name][(int)variation_value.Value];
                            if (colorImages != null)
                            {
                                foreach (JProperty colorImage in colorImages)
                                {
                                    if (colorImage.Name == dimension.Value)
                                    {
                                        product.ThumbImageUrl = colorImage.Value[0]["thumb"].ToString();
                                        break;
                                    }
                                }
                            }
                            product.Dimensions.Add(dimension);
                            var sizeMatch = Regex.Match(dimension.ToString(), @"\d+[.,]?\d*", RegexOptions.IgnoreCase);
                            if (sizeMatch.Success)
                            {
                                product.Size = float.Parse(sizeMatch.Value);
                            }
                        }
                    }

                    products.Add(product);
                }
            }

            return products;
        }

        private void FillProductInfo(CrawledPage crawledPage, Product product)
        {
            var htmlNode = crawledPage.HtmlDocument.DocumentNode;
            product.external_product_id_type = ExternalProductIdType.ASIN;
            product.external_product_id = htmlNode.SelectSingleNode("//*[@id='averageCustomerReviews']")?.Attributes["data-asin"].Value;
            product.item_name = htmlNode.SelectSingleNode("//*[@id='productTitle']")?.InnerText.HtmlDecodeAndTrim();
            product.Url = string.Format("{0}://{1}/dp/{2}?psc=1", crawledPage.Uri.Scheme, crawledPage.Uri.Host, product.external_product_id);
            product.OfferListingUrl = string.Format("{0}://{1}/gp/offer-listing/{2}/ref=olp_f_new?ie=UTF8&f_new=true", crawledPage.Uri.Scheme, crawledPage.Uri.Host, product.external_product_id);
            string brandUrl = htmlNode.SelectSingleNode("//*[@id='brand']")?.Attributes["href"].Value;
            if (brandUrl != null && brandUrl.Contains("="))
            {
                product.brand_name = brandUrl.Substring(brandUrl.LastIndexOf('=') + 1).UrlDecode();
            }
            var match = Regex.Match(htmlNode.InnerHtml, @"'productTypeName':'(.+?)'".Replace("'", "\""), RegexOptions.IgnoreCase);
            if (match.Success)
            {
                product.product_type_name = match.Groups[1].Value;
            }
            product.department_name = htmlNode.SelectSingleNode("//*[@id='wayfinding-breadcrumbs_container']")?.InnerText.HtmlDecodeAndTrim();
            product.price = htmlNode.SelectSingleNode("//*[@id='priceblock_ourprice'] | //*[@id='priceblock_saleprice']")?.InnerText.HtmlDecodeAndTrim();
        }

        public decimal? GetLowestOfferPrice(Product product)
        {
            decimal? price = null;
            if (product != null && !string.IsNullOrEmpty(product.OfferListingUrl))
            {
                var webClient = new MyWebClient();
                var html = webClient.DownloadString(product.OfferListingUrl);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var value = doc.DocumentNode.SelectSingleNode("//*[contains(@class,'olpOfferPrice')]")?.InnerText ?? string.Empty;
                var match = Regex.Match(value, @"\d+[.,]?\d*");
                if (match.Success)
                {
                    decimal d;
                    if (decimal.TryParse(match.Value.Replace(",", string.Empty), out d))
                    {
                        price = d;
                    }
                }
            }
            return price;
        }

        public byte[] GetThumbImage(Product product, int size)
        {
            byte[] image = null;
            if (product != null && !string.IsNullOrEmpty(product.ThumbImageUrl))
            {
                var webClient = new MyWebClient();
                var url = product.ThumbImageUrl.ToString(); //https://images-na.ssl-images-amazon.com/images/I/51SuX9PrSUL._US40_.jpg
                url = Regex.Replace(url, @"(\d+)(_\.)", size.ToString() + "$2");
                image = webClient.DownloadData(url);
            }
            return image;
        }
    }
}