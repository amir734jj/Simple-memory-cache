using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMemoryCache.Extensions
{
    public static class LinqExtensions
    {
        public static List<T> Copy<T>(this IEnumerable<T> enumerable) => new List<T>(enumerable.ToList());
    }
}