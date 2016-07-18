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
    public class VideoPart
    {
        public string vid { get; set; }
        public string scode { get; set; }
        public string from { get; set; }
        public string did { get; set; }
        public string sid { get; set; }
        public string href { get; set; }
        public string title { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}