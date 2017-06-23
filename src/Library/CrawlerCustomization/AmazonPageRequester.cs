using Abot.Core;
using Abot.Poco;
using System;
using System.Net;

namespace Library.CrawlerCustomization
{
    [Serializable]
    public class AmazonPageRequester : PageRequester
    {
        public AmazonPageRequester(CrawlConfiguration config) : base(config)
        {
        }

        public AmazonPageRequester(CrawlConfiguration config, IWebContentExtractor contentExtractor) : base(config, contentExtractor)
        {
        }

        protected override HttpWebRequest BuildRequestObject(Uri uri)
        {
            var request = base.BuildRequestObject(uri);
            request.UserAgent = Common.WebHelper.GetRandomUserAgent();
            request.CookieContainer = Common.WebHelper.GetExtCookieContainer(uri);
            return request;
        }
    }
}