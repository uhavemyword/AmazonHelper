using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Common
{
    public static class WebHelper
    {
        #region UserAgent

        private static List<string> _userAgent;
        private static Random _random = new Random();

        public static string GetRandomUserAgent()
        {
            if (_userAgent == null)
            {
                ReadUserAgentsFile();
            }
            if (_userAgent == null || _userAgent.Count == 0)
            {
                return null;
            }

            int index = _random.Next(0, _userAgent.Count - 1);
            return _userAgent[index];
        }

        private static void ReadUserAgentsFile()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "configs", "UserAgents.txt");
            if (File.Exists(filePath))
            {
                _userAgent = File.ReadAllLines(filePath).ToList();
            }
        }

        #endregion UserAgent

        #region GetUriCookieContainer

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InternetGetCookieEx(string url, string cookieName, StringBuilder cookieData, ref int size, Int32 dwFlags, IntPtr lpReserved);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InternetSetCookie(string url, string cookieName, string cookieData);

        private const Int32 InternetCookieHttponly = 0x2000;

        /// <summary>
        /// Gets the URI cookie container.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static CookieContainer GetExtCookieContainer(Uri uri)
        {
            CookieContainer cookies = new CookieContainer();
            // Determine the size of the cookie
            int datasize = 8192 * 16;
            StringBuilder cookieData = new StringBuilder(datasize);
            if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
            {
                if (datasize < 0)
                    return cookies;
                // Allocate stringbuilder large enough to hold the cookie
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
                    return cookies;
            }
            if (cookieData.Length > 0)
            {
                cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));
            }
            return cookies;
        }

        public static void SetExtCookie(Uri uri, CookieCollection cookies)
        {
            for (int i = 0; i < cookies.Count; i++)
            {
                Cookie c = cookies[i];
                InternetSetCookie(uri.ToString(), c.Name, c.Value);
            }
        }

        #endregion GetUriCookieContainer
    }
}