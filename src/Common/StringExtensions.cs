using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    public static class StringExtensions
    {
        public static string CompressWhiteSpace(this string str)
        {
            str = Regex.Replace(str, @"[\s\r\n]+", " ").Trim();
            return str;
        }

        public static string HtmlDecodeAndTrim(this string str)
        {
            str = HttpUtility.HtmlDecode(str);
            return str.CompressWhiteSpace();
        }

        public static string HtmlDecode(this string str)
        {
            str = HttpUtility.HtmlDecode(str);
            return str;
        }

        public static string UrlDecode(this string str)
        {
            str = HttpUtility.UrlDecode(str);
            return str;
        }
    }
}