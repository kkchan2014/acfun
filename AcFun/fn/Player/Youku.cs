using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcFun.fn.Player
{
    public class Youku : BasePlayer
    {
        public Youku(VideoInfo info, VideoRate rate)
        {
            Type = "Youku";
            Url = String.Format("http://player.youku.com/acfun.php/sid/{0}/client_id/908a519d032263f8/autoPlay/true/v.swf", rate.playUrl);
        }
    }
}