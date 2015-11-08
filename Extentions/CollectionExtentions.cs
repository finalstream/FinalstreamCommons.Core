using System.Collections.Generic;
using System.Linq;

namespace FinalstreamCommons.Extentions
{
    /// <summary>
    ///     コレクションの拡張メソッドを表します。
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
        ///     2つのコレクションの差分を更新します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nowList"></param>
        /// <param name="newCollection"></param>
        /// <param name="comparer"></param>
        public static void DiffUpdate<T>(this IList<T> nowList, ICollection<T> newCollection,
            IEqualityComparer<T> comparer = null)
        {
            // 削除されているものを削る
            var removes = nowList.Except(newCollection, comparer).ToArray();
            foreach (var remove in removes)
            {
                nowList.Remove(remove);
            }

            var i = 0;
            foreach (var item in newCollection)
            {
                if (nowList.Contains(item, comparer))
                {
                    // 存在する場合はなにもしない
                }
                else
                {
                    // 存在しない場合は追加
                    nowList.Insert(i, item);
                }
                i++;
            }
        }

        /// <summary>
        ///     コレクションの要素が１つであるかどうか。
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