using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Models
{
    /// <summary>
    /// 表示可能なアイテムを表します。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DisplayableItem<T>
    {

        public T Value { get; private set; }

        public string DisplayValue { get; private set; }

        public DisplayableItem(T value, string displayValue = null)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public override string ToString()
        {
            return DisplayValue ?? Value.ToString();
        }
    }
}
