using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Created by Chenwei on 2016/4/18.
/// </summary>
namespace AcFun.fn
{
    public class XAcFun
    {
        /// <summary>
        /// 固定AcFun请求Cookie
        /// </summary>
        private const string Cookie = "Hm_lvt_bc75b9260fe72ee13356c664daa5568c=1460948004,1460950178,1461049664,1461118827; sensorsdata2015jssdkcross=%7B%22distinct_id%22%3A%2215427496df9498-0c298f9d9456a2-13646e4a-1fa400-15427496dfa454%22%7D; analytics=GA1.2.89320270.1460948035; tma=123279387.69618643.1460948190880.1460948190880.1461007124199.2; tmd=32.123279387.69618643.1460948190880.; bfd_g=a7fcd4ae5266aa7700001c2800007010570caf80; JSESSIONID=efe5c12f65fc405892ceacd2aa51ab37; auth_key=4169836; auth_key_ac_sha1=376494769; auth_key_ac_sha1_=2vHWMNMh8tI5G1FqJMJSM2m1C6Q=; ac_username=mr%E9%85%B1%E6%B2%B9; ac_userimg=http%3A%2F%2Fcdn.aixifan.com%2Fdotnet%2F20120923%2Fstyle%2Fimage%2Favatar.jpg; _sid_=efe5c12f65fc405892ceacd2aa51ab37; viewBeta=1; Hm_lpvt_bc75b9260fe72ee13356c664daa5568c=1461118836; userGroupLevel=0; checkMobile=1; checkEmail=0; online_status=0; _gat_UA-68793632-3=1";

        public SearchResult Search(string keyword, int pageIndex)
        {
            List<string[]> parms = new List<string[]>();
            parms.AddRange(new string[][] {
                new string[] { "cd", "1" },
                new string[] { "type", "2" },
                new string[] { "q", System.Web.HttpUtility.UrlEncode(keyword) },
                new string[] { "sortType", "-1" },
                new string[] { "field", "title" },
                new string[] { "sortField", "score" },
                new string[] { "pageNo", pageIndex.ToString() },
                new string[] { "pageSize", "20" },
                new string[] { "aiCount", "3" },
                new string[] { "spCount", "3" },
                new string[] { "isWeb", "1" },
                new string[] { "sys_name", "pc" }
            });

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie",Cookie);
            headers.Add("Referer", "http://www.acfun.tv/search/");

            var json = Http.GetString(String.Format(
                "http://search.acfun.tv/search?{0}",
                String.Join("&", parms.Select(a => String.Join("=", a)).ToArray())), headers);

            //服务器返回的是直接javascript赋值语句，去除此赋值语句，获取纯JSON
            json = json.Replace("system.tv=", String.Empty);

            return Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(SearchResult)) as SearchResult;
        }

        public Video GetVideo(Detail detail)
        {
            Video video = new Video() { Detail = detail, Parts = new List<VideoPart>() };

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", Cookie);
            headers.Add("Referer", "http://www.acfun.tv/search/");

            var html = Http.GetString(String.Format("http://www.acfun.tv/v/{0}", detail.contentId), headers);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var apv = doc.GetElementbyId("area-part-view");

            if (apv != null)
            {
                var nodeList = apv.ChildNodes[0].SelectNodes("a");

                if (nodeList != null)
                {
                    VideoPart part = null;
                    foreach (var node in nodeList)
                    {
                        part = new VideoPart();
                        part.did = node.GetAttributeValue("data-did", String.Empty);
                        part.from = node.GetAttributeValue("data-from", String.Empty);
                        part.href = node.GetAttributeValue("href", String.Empty);
                        part.Name = node.InnerText.Trim();
                        part.scode = node.GetAttributeValue("data-scode", String.Empty);
                        part.sid = node.GetAttributeValue("data-sid", String.Empty);
                        part.title = node.GetAttributeValue("title", String.Empty);
                        part.vid = node.GetAttributeValue("data-vid", String.Empty);
                        video.Parts.Add(part);
                    }
                }
            }

            return video;
        }

        public VideoInfo GetVideoInfo(VideoPart part)
        {
            return GetVideoInfo(part.vid);
        }

        private VideoInfo GetVideoInfo(string vid)
        {
            List<string[]> parms = new List<string[]>();
            parms.AddRange(new string[][] {
                new string[] { "id", vid },
                new string[] { "acran", Math.Round(DateTime.Now.Ticks / (float)(DateTime.Now.Ticks * 150), 8).ToString() }
            });

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", Cookie);
            headers.Add("Referer", "http://www.acfun.tv/");
            headers.Add("DNT", "1");

            var json = Http.GetString(String.Format(
                "http://www.acfun.tv/video/getVideo.aspx?{0}",
                String.Join("&", parms.Select(a => String.Join("=", a)).ToArray())), headers);

            return Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(VideoInfo)) as VideoInfo;
        }
    }
}