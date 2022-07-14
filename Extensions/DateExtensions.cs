using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoterX.Utilities.Extensions
{
    public static class DateExtensions
    {
        public static TimeSpan ToTimeSpan(this string time)
        {
            DateTime dateTime = DateTime.Parse(time);
            return dateTime.TimeOfDay;
        }

        //public static TimeSpan Parse(this TimeSpan t, string time)
        //{
        //    return time.ToTimeSpan();
        //}
    }
}
