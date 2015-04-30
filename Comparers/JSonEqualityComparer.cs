using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FinalstreamCommons.Comparers
{
    /// <summary>
    ///     JSONでシリアライズした結果を比較するコンペアラーを表します。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JSonEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            return String.Equals
                (
                    JsonConvert.SerializeObject(x),
                    JsonConvert.SerializeObject(y)
                );
        }

        public int GetHashCode(T obj)
        {
            return JsonConvert.SerializeObject(obj).GetHashCode();
        }
    }
}