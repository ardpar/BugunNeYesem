using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.Infrastructure
{
    public static class LinqExtension
    {
        public static bool CheckAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }
        public static bool CheckAny<T>(this List<T> data)
        {
            return data != null && data.Any();
        }
    }
}
