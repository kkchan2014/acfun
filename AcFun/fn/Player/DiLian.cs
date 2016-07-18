using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcFun.fn.Player
{
    public class DiLian : BasePlayer
    {
        public DiLian(VideoInfo info, VideoRate rate)
        {
            Type = "DiLian";
            Url = Utils.Base64ToString(rate.playUrl);
        }
    }
}