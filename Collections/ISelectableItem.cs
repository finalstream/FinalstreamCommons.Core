using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Collections
{
    /// <summary>
    /// 選択可能なアイテムを表します。
    /// </summary>
    public interface ISelectableItem
    {
        bool IsSelected { get; set; }
    }
}
