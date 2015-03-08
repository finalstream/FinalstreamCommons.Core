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


    }
}
