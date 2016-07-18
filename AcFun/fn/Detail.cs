using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Created by Chenwei on 2016/4/18.
/// *适配AcFun Json Result 结构
/// </summary>
namespace AcFun.fn
{
    public class Detail
    {
        public string contentId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<string> tags { get; set; }
        public int channelId { get; set; }
        public int parentChannelId { get; set; }
        public int views { get; set; }
        public int stows { get; set; }
        public int comments { get; set; }
        public int userId { get; set; }
        public string avatar { get; set; }
        public string titleImg { get; set; }
        public string username { get; set; }
        public long releaseDate { get; set; }
        public bool recommend { get; set; }
        public int status { get; set; }
        public string url { get; set; }
        public List<int> channelIds { get; set; }
        public string sourceType { get; set; }
        public int time { get; set; }
        public int display { get; set; }
        public int contentSize { get; set; }
        public int tudouDomain { get; set; }
    }
}