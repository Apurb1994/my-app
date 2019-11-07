//using ISG_VIG_Brands.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Web;

//namespace ISG_VIG_Brands.Library
//{
//    public static class Converter
//    {
//        public static IApplicationLogger logger = ApplicationLogger.Instance;
//        private const string QUOTE = "\"";
//        private const string ESCAPED_QUOTE = "\"\"";
//        private static readonly char[] CHARACTERS_THAT_MUST_BE_QUOTED = { ',', '"', '\n' };

//        public static T ConvertValue<T>(this string value)
//        {
//            T retValue = default(T);
//            try
//            {
//                if (typeof(T).IsEnum)
//                {
//                    retValue = (T)Enum.Parse(typeof(T), value);
//                }
//                else if (typeof(T).Equals(typeof(DateTime)))
//                {
//                    object o = value.ConvertToDate();
//                    retValue = (T)o;
//                }
//                else
//                {
//                    if (typeof(T) == typeof(double))
//                    {
//                        try
//                        {
//                            decimal d = (decimal)Convert.ChangeType(value, typeof(decimal));

//                            retValue = (T)Convert.ChangeType((object)d, typeof(T));
//                        }
//                        catch
//                        {
//                            retValue = (T)Convert.ChangeType(value, typeof(T));
//                        }
//                    }
//                    else
//                    {
//                        retValue = (T)Convert.ChangeType(value, typeof(T));
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.Error("Exception in ConvertValue ", ex);
//            }
//            return retValue;
//        }

//        public static DateTime ConvertToDate(this string value)
//        {
//            DateTime t = DateTime.MinValue;
//            bool AmPmFound = false;

//            try
//            {
//                string value1 = string.Empty;
//                string value2 = string.Empty;
//                value1 = value.ToString();

//                Match m = Regex.Match(value1, @"(\d{4}\-\d{1,2}\-\d{1,2})");
//                if (m.Success)
//                {
//                    value2 = m.Groups[1].ToString();
//                    m = Regex.Match(value1, @"(\d{1,2}\:\d{1,2}\:\d{1,2})");
//                    if (m.Success)
//                    {
//                        value2 += " " + m.Groups[1].ToString();

//                        m = Regex.Match(value1, @"( [AP]M)");
//                        if ((AmPmFound = m.Success))
//                        {
//                            value2 += m.Groups[1].ToString();
//                        }
//                    }
//                    else
//                    {
//                        value2 += " 00:00:00 AM";
//                        AmPmFound = true;
//                    }
//                }

//                try
//                {
//                    t = DateTime.ParseExact(value2, "yyyy-MM-dd HH:mm:ss" + (AmPmFound ? " tt" : string.Empty), null);
//                }
//                catch
//                {
//                    t = Convert.ToDateTime(value2);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.Error("Exception in ConvertToDate function", ex);
//            }

//            return t;
//        }

//        public static string GetValidDate(this string date, string format)
//        {
//            string retValue = string.Empty;

//            try
//            {
//                DateTime dateTime = DateTime.ParseExact(date, format/*"yyyy-MM-ddTHH:mm:ss.fffZ"*/,
//                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
//                retValue = dateTime.ToString(format);
//            }
//            catch (Exception ex)
//            {
//                logger.Error("Exception in GetValidDate function", ex);
//            }

//            return retValue;
//        }

//        public static string Escape(this string s)
//        {
//            //if (s.Contains(QUOTE))
//            //    s = s.Replace(QUOTE, ESCAPED_QUOTE);

//            //if (s.IndexOfAny(CHARACTERS_THAT_MUST_BE_QUOTED) > -1)
//            //    s = QUOTE + s + QUOTE;

//            return string.Format("{0}{1}{0}", QUOTE, s);
//        }

//        public static string Unescape(this string s)
//        {
//            if (s.StartsWith(QUOTE) && s.EndsWith(QUOTE))
//            {
//                s = s.Substring(1, s.Length - 2);

//                if (s.Contains(ESCAPED_QUOTE))
//                    s = s.Replace(ESCAPED_QUOTE, QUOTE);
//            }

//            return s;
//        }

//        public static List<string> GetQuotedValues(this List<string> ct)
//        {
//            if (ct == null) { return new List<string>(); }
//            for (int i = 0; i < ct.Count; i++)
//            {
//                ct[i] = ct[i].GetValidString().Trim().Escape();
//            }

//            return ct;
//        }

//        public static List<string> GetUnescapedValues(this List<string> ct)
//        {
//            if (ct == null) { return new List<string>(); }
//            for (int i = 0; i < ct.Count; i++)
//            {
//                ct[i] = ct[i].GetValidString().Unescape().Trim();
//            }

//            return ct;
//        }
//    }

//}