using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Extentions
{
    public static class CollectionExtentions
    {
        /// <summary>
        /// 2つのコレクションの差分を更新します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nowCollection"></param>
        /// <param name="newCollection"></param>
        public static void DiffUpdate<T>(this ICollection<T> nowCollection, ICollection<T> newCollection)
        {
            if (nowCollection.Count == 0) nowCollection.Clear();    // クリアしないとなぜか重複するので。

            var removes = nowCollection.Except(newCollection);

            var adds = newCollection.Except(nowCollection);

            foreach (var remove in removes)
            {
                nowCollection.Remove(remove);
            }

            foreach (var add in adds)
            {
                if (!nowCollection.Contains(add))nowCollection.Add(add);
            }
        }
    }
}
