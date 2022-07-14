using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VoterX.Utilities.Extensions
{
    public static class RegexExtensions
    {
        // If you want to implement both "*" and "?"
        public static String WildCardToRegular(String value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }

        // If you want to implement "*" only
        //public static String WildCardToRegular(String value)
        //{
        //    return "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
        //}
    }
}
