using Abot.Core;
using Abot.Poco;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Library.CrawlerCustomization
{
    public class AmazonHyperLinkParser : HapHyperLinkParser
    {
        private IAmazonHelper _helper;

        public AmazonHyperLinkParser(IAmazonHelper helper)
        {
            _helper = helper;
        }

        protected override IEnumerable<string> GetHrefValues(CrawledPage crawledPage)
        {
            List<string> hrefValues = new List<string>();
            if (HasRobotsNoFollow(crawledPage))
                return hrefValues;

            //HtmlNodeCollection productLinkNodes = _helper.GetProductLinkNodes(crawledPage.HtmlDocument);
            //hrefValues.AddRange(GetLinks(productLinkNodes));
            var productLinks = _helper.GetProductLinksFromListPage(crawledPage);
            hrefValues.AddRange(productLinks);
            HtmlNodeCollection pageLinkNodes = _helper.GetPageLinkNodesFromListPage(crawledPage);
            hrefValues.AddRange(GetLinks(pageLinkNodes));
            return hrefValues.Distinct().ToList();
        }
    }
}