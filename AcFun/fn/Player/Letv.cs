using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcFun.fn.Player
{
    public class Letv : BasePlayer
    {
        public Letv(VideoInfo info, VideoRate rate)
        {
            Type = "Letv";
            Url = "http://yuntv.letv.com/bcloud.swf";
            Parms = new List<string[]>();
            Parms.AddRange(new string[][] {
                new string[] { "uu", "2d8c027396" },
                new string[] { "auto_play", "1" },
                new string[] { "skinnable", "0" },
                new string[] { "pu", "8e7e683c11" },
                new string[] { "vu", rate.playUrl }
            });
        }
    }
}