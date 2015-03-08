using Newtonsoft.Json;

namespace FinalstreamCommons.Extentions
{
    public static class ObjectExtensions
    {

        /// <summary>
        /// JSON形式に変換します。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJson(this object value)
        {
            if (value == null) return "null";

            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch
            {
                return value.ToString();
            }
        }
    }
}
