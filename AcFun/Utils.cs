using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Created by Chenwei on 2016/4/18.
/// </summary>
namespace AcFun
{
    public static class Utils
    {
        public static string Base64ToString(string str)
        {
            return System.Text.ASCIIEncoding.Default.GetString(Convert.FromBase64String(str));
        }
    }
}