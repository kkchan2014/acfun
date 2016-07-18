using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

/// <summary>
/// Created by Chenwei on 2016/4/18.
/// </summary>
namespace AcFun
{
    public class Http
    {

        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:45.0) Gecko/20100101 Firefox/45.0";

        public static string GetString(string url, Dictionary<string, string> headers)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.ContentType = "text/html;charset=UTF-8";
            foreach (var key in headers.Keys)
            {
                switch (key)
                {
                    case "Referer":
                        req.Referer = headers[key];
                        break;
                    default:
                        req.Headers.Add(key, headers[key]);
                        break;
                }

            }
            req.UserAgent = UserAgent;

            try
            {
                using (HttpWebResponse rsp = (HttpWebResponse)req.GetResponse())
                using (Stream stream = rsp.GetResponseStream())
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (System.Net.WebException)
            {
                return String.Empty;
            }
        }

        public static byte[] GetData(string url)
        {
            WebClient wc = new WebClient();

            try
            {
                return wc.DownloadData(url);
            }
            catch (System.Net.WebException)
            {
                return null;
            }
        }

        public static Image GetBitmap(string url)
        {
            var buf = GetData(url);

            if (buf == null) return null;

            using (MemoryStream ms = new MemoryStream(buf))
            {
                return Bitmap.FromStream(ms);
            }
        }
    }
}