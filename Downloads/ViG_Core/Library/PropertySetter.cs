using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ISG_VIG_Brands.Library
{
    public static class PropertySetter
    {
        //public static readonly string ColumnSeperator = ConfigurationManager.AppSettings["Seperator"].ToString();
        public static readonly string ColumnSeperator ="";

        public static string GetValidString(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            try
            {
                string re = @"[^\x09\x0A\x0D\x20-\uD7FF\uE000-\uFFFD\u10000-u10FFFF]";
                value = Regex.Replace(value, @"\""", string.Empty);
                value = Regex.Replace(value, re, string.Empty);
                value = Regex.Replace(value, @"[\&]", string.Empty);
            }
            catch { }
            return value;
        }
    }
}
