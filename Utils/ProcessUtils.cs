using System.Linq;
using System.Management;

namespace FinalstreamCommons.Utils
{
    public static class ProcessUtils
    {

        /// <summary>
        /// プロセスIDから実行ファイルパスを取得します。
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static string GetExecutablePath(int processId)
        {
            var wmiQueryString = "SELECT ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId;
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            {
                using (var results = searcher.Get())
                {
                    var mo = results.Cast<ManagementObject>().FirstOrDefault();
                    if (mo != null)
                    {
                        return (string)mo["ExecutablePath"];
                    }
                }
            }
            return null;
        }

    }
}
