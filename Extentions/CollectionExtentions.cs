using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FinalstreamCommons.Extentions
{
    /// <summary>
    /// コレクションの拡張メソッドを表します。
    /// </summary>
    public static class CollectionExtentions
    {
        /// <summary>
        ///     2つのコレクションの差分を更新します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nowCollection"></param>
        /// <param name="newCollection"></param>
        /// <param name="comparer"></param>
        public static void DiffUpdate<T>(this ICollection<T> nowCollection, ICollection<T> newCollection,
            IEqualityComparer<T> comparer = null)
        {
            var removes = nowCollection.Except(newCollection, comparer).ToArray();

            var adds = newCollection.Except(nowCollection, comparer).ToArray();

            foreach (var remove in removes)
            {
                nowCollection.Remove(remove);
            }

            foreach (var add in adds)
            {
                if (!nowCollection.Contains(add)) nowCollection.Add(add);
            }
        }

        /// <summary>
        /// コレクションの要素が１つであるかどうか。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsSingle<T>(this ICollection<T> collection)
        {
            return collection.Count == 1;
        }
    }
}