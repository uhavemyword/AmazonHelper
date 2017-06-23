using System;
using System.Net;
using System.Text;

namespace Common
{
    public class MyWebClient : WebClient
    {
        private CookieContainer _cookieContainer = new CookieContainer();

        public MyWebClient()
        {
            base.Encoding = Encoding.UTF8;
            base.Headers[HttpRequestHeader.Accept] = "*/*";
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            HttpWebRequest webRequest = request as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.UserAgent = WebHelper.GetRandomUserAgent();
                webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                _cookieContainer.Add(WebHelper.GetExtCookieContainer(address).GetCookies(address));
                webRequest.CookieContainer = WebHelper.GetExtCookieContainer(address);
            }
            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            HttpWebResponse webResponse = response as HttpWebResponse;
            if (webResponse != null && !string.IsNullOrEmpty(webResponse.Headers[HttpResponseHeader.SetCookie]))
            {
                _cookieContainer.SetCookies(webResponse.ResponseUri, webResponse.Headers[HttpResponseHeader.SetCookie]);
            }

            return response;
        }
    }
}