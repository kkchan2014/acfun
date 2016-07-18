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
    public class Video
    {
        public Detail Detail { get; set; }
        public List<VideoPart> Parts { get; set; }
    }
}