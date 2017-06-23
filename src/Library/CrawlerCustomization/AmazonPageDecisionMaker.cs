using Abot.Core;
using Abot.Poco;
using System.Net;

namespace Library.CrawlerCustomization
{
    public class AmazonPageDecisionMaker : CrawlDecisionMaker
    {
        private IAmazonHelper _helper;

        public AmazonPageDecisionMaker(IAmazonHelper helper)
        {
            _helper = helper;
        }

        public override CrawlDecision ShouldCrawlPageLinks(CrawledPage crawledPage, CrawlContext crawlContext)
        {
            var result = base.ShouldCrawlPageLinks(crawledPage, crawlContext);
            if (result.Allow)
            {
                if (!_helper.IsProductListPage(crawledPage))
                {
                    result.Allow = false;
                    result.Reason = "Page doesn't contain product list";
                }
            }
            return result;
        }

        public override CrawlDecision ShouldRecrawlPage(CrawledPage crawledPage, CrawlContext crawlContext)
        {
            var result = base.ShouldRecrawlPage(crawledPage, crawlContext);
            if (!result.Allow)
            {
                if (_helper.IsCaptchaPage(crawledPage))
                {
                    result.Allow = true;
                    result.Reason = "Page is a captcha page.";
                }
            }
            return result;
        }

        public override CrawlDecision ShouldDownloadPageContent(CrawledPage crawledPage, CrawlContext crawlContext)
        {
            var result = base.ShouldDownloadPageContent(crawledPage, crawlContext);
            if (!result.Allow)
            {
                if (crawledPage.HttpWebResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    result.Allow = true;
                    result.Reason = "Service unavailable detected, but might be a captcha page.";
                }
            }
            return result;
        }
    }
}