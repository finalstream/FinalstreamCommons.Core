using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Utils
{
    /// <summary>
    /// 日付ユーティリティを表します。
    /// </summary>
    public static class DateUtils
    {

        /// <summary>
        /// シーズン文字列を取得します。
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string GetSeasonString(DateTime datetime)
        {
            var month = datetime.Month;

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    return "Winter";
                case 4:
                case 5:
                case 6:
                    return "Spring";
                case 7:
                case 8:
                case 9:
                    return "Summer";
                case 10:
                case 11:
                case 12:
                    return "Autumn";
            }
            return "Unknown";
        }

    }
}
