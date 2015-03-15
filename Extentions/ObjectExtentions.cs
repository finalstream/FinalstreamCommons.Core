using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using NLog;

namespace FinalstreamCommons.Extentions
{
    public static class ObjectExtensions
    {
        // 概要:
        //     Specifies formatting options for the Newtonsoft.Json.JsonTextWriter.
        public enum LogFormat
        {
            // 概要:
            //     No special formatting is applied. This is the default.
            None = 0,
            //
            // 概要:
            //     Causes child objects to be indented according to the Newtonsoft.Json.JsonTextWriter.Indentation
            //     and Newtonsoft.Json.JsonTextWriter.IndentChar settings.
            Indented = 1,
        }

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// JSON形式に変換します。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="formatting"></param>
        /// <returns></returns>
        public static string ToJson(this object value, LogFormat formatting = LogFormat.None)
        {
            if (value == null) return "null";

            try
            {
                return JsonConvert.SerializeObject(value, ConvertLogFormatToFormatting(formatting));
            }
            catch
            {
                return value.ToString();
            }
        }

        private static Formatting ConvertLogFormatToFormatting(LogFormat logFormat)
        {
            return logFormat == LogFormat.None ? Formatting.None : Formatting.Indented;
        }


        /// <summary>
        /// ログにJson形式で出力します。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="value"></param>
        /// <param name="detail"></param>
        /// <param name="formatting"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static void DebugWriteJson(this object value, string detail = null, LogFormat formatting = LogFormat.None, [CallerMemberName] string caller = null)
        {
            if (!Log.IsDebugEnabled) return; 
            if (value == null) return;
            if (caller == null) caller = "Unknown";

            var format =  string.Format("{0}{1}:",caller, string.IsNullOrEmpty(detail)? "":"(" + detail + ")") + "{0}";

            Log.Debug(() => string.Format(format, value.ToJson(formatting)));
        }

        /// <summary>
        /// ログにJson形式で出力します。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="value"></param>
        /// <param name="detail"></param>
        /// <param name="formatting"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static void TraceWriteJson(this object value, string detail = null, LogFormat formatting = LogFormat.None, [CallerMemberName] string caller = null)
        {
            if (!Log.IsTraceEnabled) return; 
            if (value == null) return;
            if (caller == null) caller = "Unknown";

            var format = string.Format("{0}{1}:", caller, string.IsNullOrEmpty(detail) ? "" : "(" + detail + ")") + "{0}";

            Log.Trace(() => string.Format(format, value.ToJson(formatting)));
        }
    }
}
