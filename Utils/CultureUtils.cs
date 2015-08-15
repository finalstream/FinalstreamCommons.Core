using System.Globalization;
using System.Linq;

namespace FinalstreamCommons.Utils
{
    /// <summary>
    ///     カルチャを扱うユーティリティを表します。
    /// </summary>
    public static class CultureUtils
    {
        /// <summary>
        ///     カルチャ名からカルチャ情報を生成します。
        /// </summary>
        /// <param name="cultureNames"></param>
        /// <returns></returns>
        public static CultureInfo[] CreateCultureInfos(string[] cultureNames)
        {
            return cultureNames.Select(x => new CultureInfo(x)).ToArray();
        }

        /// <summary>
        ///     カルチャに一致するカルチャを取得します。
        /// </summary>
        /// <param name="cultureName"></param>
        /// <param name="cultureInfos"></param>
        /// <returns></returns>
        public static CultureInfo GetCulture(string cultureName, CultureInfo[] cultureInfos)
        {
            return cultureInfos.Where(x => x.Name == cultureName).Concat(cultureInfos).First();
        }
    }
}