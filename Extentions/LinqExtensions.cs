using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalstreamCommons.Comparer;

namespace FinalstreamCommons.Extentions
{
    public static partial class LinqExtensions
    {
        public static IEnumerable<T> ExceptUsingJSonCompare<T>
            (this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.Except(second, new JSonEqualityComparer<T>());
        }
    }
}
