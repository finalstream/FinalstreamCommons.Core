using System.Diagnostics;
using System.Reflection;

namespace FinalstreamCommons.Utils
{
    /// <summary>
    ///     アプリケーションのユーティリティを表します。
    /// </summary>
    public static class ApplicationUtils
    {
        /// <summary>
        ///     アセンブリがデバッグビルドかどうかを取得します。
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static bool IsAssemblyDebugBuild(Assembly assembly)
        {
            foreach (var attribute in assembly.GetCustomAttributes(false))
            {
                var debuggableAttribute = attribute as DebuggableAttribute;
                if (debuggableAttribute != null)
                {
                    return debuggableAttribute.IsJITTrackingEnabled;
                }
            }
            return false;
        }
    }
}