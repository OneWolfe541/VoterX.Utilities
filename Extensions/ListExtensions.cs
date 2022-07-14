using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoterX.Utilities.Extensions
{
    public static class ListExtensions
    {
        // https://visualstudiomagazine.com/articles/2019/07/01/updating-linq.aspx
        public static IEnumerable<T> SetValue<T>(this IEnumerable<T> items, Action<T>
         updateMethod)
        {
            foreach (T item in items)
            {
                updateMethod(item);
            }
            return items;
        }
    }
}
