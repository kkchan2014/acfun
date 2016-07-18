using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcFun.fn.Player
{
    public class BasePlayer
    {
        public string Url { get; set; }
        public List<string[]> Parms { get; set; }
        public string Type { get; set; }
        public VideoInfo VideoInfo { get; private set; }

        public static BasePlayer Parse(VideoInfo info, VideoRate rate)
        {
            BasePlayer player = null;

            switch (info.player)
            {
                case "youku": player = new Youku(info, rate); break;
                case "letv": player = new Letv(info, rate); break;
                default: player = new DiLian(info, rate); break;
            }
            player.VideoInfo = info;

            return player;
        }
    }
}