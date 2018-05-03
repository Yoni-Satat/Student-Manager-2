using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManager2.Extentions
{
    public static class Extentions
    {
        public static int FindIndex<T>(this IEnumerable<T> items, Predicate<T> predicate)
        {
            int index = 0;
            foreach (var item in items)
            {
                if (predicate(item)) break;
                index++;
            }
            return index;
        }
    }
}