using System.Collections.Generic;
using System.Linq;
using FinalstreamCommons.Comparers;

namespace FinalstreamCommons.Extentions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> ExceptUsingJSonCompare<T>
            (this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.Except(second, new JSonEqualityComparer<T>());
        }
    }
}