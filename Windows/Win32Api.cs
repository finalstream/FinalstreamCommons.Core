using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FinalstreamCommons.Windows
{
    public static class Win32Api
    {

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        extern static int GetWindowText(IntPtr hWnd, StringBuilder lpStr, int nMaxCount);


        public static string GetWindowCaption(IntPtr hwnd, string className, string regexPattern)
        {
            StringBuilder sb = new StringBuilder(100);
            IntPtr dialogHandle;
            IntPtr windowHandle;
            dialogHandle = FindWindowEx(hwnd, IntPtr.Zero, null, null);
            while (dialogHandle != IntPtr.Zero)
            {
                windowHandle = FindWindowEx(dialogHandle, IntPtr.Zero, null, null);
                while (windowHandle != IntPtr.Zero)
                {
                    GetWindowText(windowHandle, sb, sb.Capacity);  // タイトルバー文字列を取得
                    if (new System.Text.RegularExpressions.Regex(regexPattern).IsMatch(sb.ToString()))
                    {
                        return sb.ToString();
                    }
                    windowHandle = FindWindowEx(dialogHandle, windowHandle, null, null);
                }
                dialogHandle = FindWindowEx(hwnd, dialogHandle, null, null);
            }
            /*
            if (hWndChild != IntPtr.Zero)
            {
                GetWindowText(hwnd, sb, sb.Capacity);  // タイトルバー文字列を取得
            }*/

            return "";
        }

    }
}
