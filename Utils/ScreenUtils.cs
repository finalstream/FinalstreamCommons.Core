using System.Runtime.InteropServices;
using FinalstreamCommons.Windows;

namespace FinalstreamCommons.Utils
{
    public static class ScreenUtils
    {
        /// <summary>
        ///     デバイス名からディスプレイデバイス情報を取得します。
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public static Win32Api.DISPLAY_DEVICE GetDisplayDevice(string deviceName)
        {
            var d = new Win32Api.DISPLAY_DEVICE();
            d.cb = Marshal.SizeOf(d);
            Win32Api.EnumDisplayDevices(deviceName, 0, ref d, 0);
            return d;
        }
    }
}