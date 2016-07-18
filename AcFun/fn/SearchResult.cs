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
    public class SearchResult
    {
        public int status { get; set; }
        public string msg { get; set; }
        public bool success { get; set; }
        public Page data { get; set; }
    }
}