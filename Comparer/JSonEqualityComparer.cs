using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Comparer
{
    /// <summary>
    /// JSONでシリアライズした結果を比較するコンペアラーを表します。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JSonEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            return String.Equals
            (
                Newtonsoft.Json.JsonConvert.SerializeObject(x),
                Newtonsoft.Json.JsonConvert.SerializeObject(y)
            );
        }

        public int GetHashCode(T obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj).GetHashCode();
        }
    }      
}
