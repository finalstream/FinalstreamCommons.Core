using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Utils
{
    public static class StringUtils
    {

        /// <summary>
        /// 数値であるかどうか
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(string s)
        {
            double dNullable;
            return double.TryParse(
                s,
                System.Globalization.NumberStyles.Any,
                null,
                out dNullable
            );
        }

        /// <summary>
        /// 後ろから検索して見つかったら１度だけ置換します。
        /// </summary>
        /// <returns></returns>
        public static string ReplaceLastOnce(string s, string find, string replace)
        {
            var index = s.LastIndexOf(find);
            if (index == -1) return s;
            return s.Remove(index, find.Length).Insert(index, replace);
        }

    }
}
