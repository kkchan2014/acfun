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
    public class PageInfo
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public int totalPage
        {
            get
            {
                return (int)Math.Ceiling((totalCount / (float)pageSize));
            }
        }
        public List<Detail> sp { get; set; }
        public List<Detail> list { get; set; }
    }
}