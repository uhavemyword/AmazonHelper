using Abot.Poco;
using HtmlAgilityPack;
using Library.Model;
using System.Collections.Generic;

namespace Library
{
    public interface IAmazonHelper
    {
        List<string> GetProductLinksFromListPage(CrawledPage crawledPage);

        HtmlNodeCollection GetPageLinkNodesFromListPage(CrawledPage crawledPage);

        List<Product> GetProductsFromDetailPage(CrawledPage crawledPage);

        bool IsCaptchaPage(CrawledPage crawledPage);

        bool IsProductDetailPage(CrawledPage crawledPage);

        bool IsProductListPage(CrawledPage crawledPage);

        decimal? GetLowestOfferPrice(Product product);

        byte[] GetThumbImage(Product product, int size);
    }
}