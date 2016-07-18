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
    public class VideoInfo
    {
        public string sourceId { get; set; }
        public string contentId { get; set; }
        public int allowDanmaku { get; set; }
        public string title { get; set; }
        public int userId { get; set; }
        public int danmakuId { get; set; }
        public string sourceUrl { get; set; }
        public string sourceType { get; set; }
        public string createTime { get; set; }
        public List<VideoRate> videoList { get; set; }
        public bool success { get; set; }
        public int startTime { get; set; }
        public int id { get; set; }
        public int time { get; set; }
        public int config { get; set; }
        public string player { get; set; }
        public int status { get; set; }
    }
}