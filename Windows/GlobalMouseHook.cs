using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FinalstreamCommons.Windows
{
    /// <summary>マウスが入力されたときに実行されるメソッドを表すイベントハンドラ。</summary>
    public delegate void GlobalMouseHookedEventHandler(object sender, GlobalMouseHookedEventArgs e);

    /// <summary>マウス操作の種類を表す。</summary>
    public enum MouseMessage
    {
        /// <summary>マウスカーソルが移動した。</summary>
        Move = 0x200,

        /// <summary>左ボタンが押された。</summary>
        LDown = 0x201,

        /// <summary>左ボタンが解放された。</summary>
        LUp = 0x202,

        /// <summary>右ボタンが押された。</summary>
        RDown = 0x204,

        /// <summary>左ボタンが解放された。</summary>
        RUp = 0x205,

        /// <summary>中ボタンが押された。</summary>
        MDown = 0x207,

        /// <summary>中ボタンが解放された。</summary>
        MUp = 0x208,

        /// <summary>ホイールが回転した。</summary>
        Wheel = 0x20A,

        /// <summary>Xボタンが押された。</summary>
        XDown = 0x20B,

        /// <summary>Xボタンが解放された。</summary>
        XUp = 0x20C
    }

    /// <summary>マウスの状態を表す。</summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct MouseState
    {
        /// <summary>スクリーン座標によるマウスカーソルの現在位置。</summary>
        [FieldOffset(0)]
        public Point Point;

        /// <summary>messageがMouseMessage.Wheelの時にその詳細データを持つ。</summary>
        [FieldOffset(8)]
        public WheelData WheelData;

        /// <summary>messageがMouseMessage.XDown/MouseMessage.XUpの時にその詳細データを持つ。</summary>
        [FieldOffset(8)]
        public XButtonData XButtonData;

        /// <summary>マウスのイベントインジェクト。</summary>
        [FieldOffset(12)]
        public MouseStateFlag Flag;

        /// <summary>メッセージが送られたときの時間</summary>
        [FieldOffset(16)]
        public int Time;

        /// <summary>メッセージに関連づけられた拡張情報</summary>
        [FieldOffset(20)]
        public IntPtr ExtraInfo;
    }

    /// <summary>マウスホイールの状態の詳細を表す。</summary>
    public struct WheelData
    {
        /// <summary>ホイールの回転一刻みを表す。</summary>
        public const int OneWheel = 120;

        /// <summary>ビットデータ。</summary>
        public int State;

        /// <summary>ホイールの回転量を表す。クリックされたときは-1。</summary>
        public int WheelDelta
        {
            get
            {
                var delta = State >> 16;
                return (delta < 0) ? -delta : delta;
            }
        }

        /// <summary>ホイールが一刻み分動かされたかどうかを表す。</summary>
        public bool IsOneWheel
        {
            get { return (State >> 16) == OneWheel; }
        }

        /// <summary>ホイールの回転方向を表す。</summary>
        public WheelDirection Direction
        {
            get
            {
                var delta = State >> 16;
                if (delta == 0) return WheelDirection.None;
                return (delta < 0) ? WheelDirection.Backward : WheelDirection.Forward;
            }
        }
    }

    /// <summary>ホイールの回転方向を表す。</summary>
    public enum WheelDirection
    {
        /// <summary>回転していない。</summary>
        None = 0,

        /// <summary>ユーザから離れる方向へ回転した。</summary>
        Forward = 1,

        /// <summary>ユーザに近づく方向へ回転した。</summary>
        Backward = -1
    }

    /// <summary>Xボタンの状態の詳細を表す。</summary>
    public struct XButtonData
    {
        /// <summary>ビットデータ。</summary>
        public int State;

        /// <summary>操作されたボタンを示す。</summary>
        public int ControlledButton
        {
            get { return State >> 16; }
        }

        /// <summary>Xボタン1が押されたかどうかを示す。</summary>
        public bool IsXButton1
        {
            get { return (State >> 16) == 1; }
        }

        /// <summary>Xボタン2が押されたかどうかを示す。</summary>
        public bool IsXButton2
        {
            get { return (State >> 16) == 2; }
        }
    }

    /// <summary>マウスの状態を補足する。</summary>
    internal struct MouseStateFlag
    {
        /// <summary>ビットデータ。</summary>
        public int Flag;

        /// <summary>イベントがインジェクトされたかどうかを表す。</summary>
        public bool IsInjected
        {
            get { return (Flag & 1) != 0; }
            set { Flag = value ? (Flag | 1) : (Flag & ~1); }
        }
    }

    /// <summary>
    ///     マウスのグローバルフックを行うクラス
    /// </summary>
    /// <remarks>http://hongliang.seesaa.net/article/7651626.html</remarks>
    public class GlobalMouseHook : Component
    {
        public enum DesktopWindow
        {
            ProgMan,
            SHELLDLL_DefViewParent,
            SHELLDLL_DefView,
            SysListView32
        }

        private const int MouseLowLevelHook = 14;
        private static readonly object EventMouseHooked = new object();
        private readonly MouseButtons[] _captureMouseButtons = Enumerable.Empty<MouseButtons>().ToArray();
        private MouseButtons _button = MouseButtons.None;
        private IntPtr _hook;
        private GCHandle _hookDelegate;
        private Point _position;
        private int _time;

        /// <summary>
        ///     インスタンスを作成する。
        /// </summary>
        /// <exception cref="Win32Exception">フックに失敗しました。原因の詳細はエラーコードを参照してください。</exception>
        public GlobalMouseHook()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                throw new PlatformNotSupportedException("Windows 98/Meではサポートされていません。");
            MouseHookDelegate handler = CallNextHook;
            _hookDelegate = GCHandle.Alloc(handler);

            var module = Marshal.GetHINSTANCE(Assembly.GetEntryAssembly().GetModules()[0]);
            _hook = SetWindowsHookEx(MouseLowLevelHook, handler, module, 0);
            if (_hook == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public GlobalMouseHook(MouseButtons[] captureMouseButtons) : this()
        {
            _captureMouseButtons = captureMouseButtons;
        }

        /// <summary>
        ///     マウスが入力されたときに実行するデリゲートを指定してインスタンスを作成する。
        /// </summary>
        /// <param name="handler">マウスが入力されたときに実行するメソッドを表すデリゲート。</param>
        public GlobalMouseHook(GlobalMouseHookedEventHandler handler) : this()
        {
            MouseHooked += handler;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int hookType, MouseHookDelegate hookDelegate, IntPtr hInstance,
            uint threadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int CallNextHookEx(IntPtr hook, int code, MouseMessage message, ref MouseState state);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(int x, int y);

        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        static extern IntPtr GetParent(IntPtr hwnd);
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp);
        [DllImport("user32.dll")]
        static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);
        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int pID);
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int fdwAccess, bool fInherit, int IDProcess);
        [DllImport("kernel32.dll")]
        static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll")]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            int dwSize, int flAllocationType, int flProtect);
        [DllImport("kernel32.dll")]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
            int dwSize, int dwFreeType);
        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            ref LVHITTESTINFO lpBuffer, int nSize, ref int lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            ref LVHITTESTINFO lpBuffer, int nSize, ref int lpNumberOfBytesRead);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowText", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct LVHITTESTINFO
        {
            public POINT pt;
            public uint flags;
            public int iItem;
            public int iSubItem;
        }

        const int PROCESS_VM_OPERATION = 8;
        const int PROCESS_VM_READ = 16;
        const int PROCESS_VM_WRITE = 32;
        const int MEM_COMMIT = 4096;
        const int MEM_RELEASE = 32768;
        const int PAGE_READWRITE = 4;
        const int LVM_FIRST = 0x1000;
        const int LVM_SUBITEMHITTEST = (LVM_FIRST + 57);


        /// <summary>マウスが入力されたときに発生する。</summary>
        public event GlobalMouseHookedEventHandler MouseHooked
        {
            add { Events.AddHandler(EventMouseHooked, value); }
            remove { Events.RemoveHandler(EventMouseHooked, value); }
        }

        /// <summary>
        ///     MouseHookedイベントを発生させる。
        /// </summary>
        /// <param name="e">イベントのデータ。</param>
        protected virtual void OnMouseHooked(GlobalMouseHookedEventArgs e)
        {
            var handler = Events[EventMouseHooked] as GlobalMouseHookedEventHandler;
            if (handler != null)
                handler(this, e);
        }

        private int CallNextHook(int code, MouseMessage message, ref MouseState state)
        {
            if (code >= 0)
            {
                var button = MouseButtons.None;
                switch (message)
                {
                    case MouseMessage.LDown:
                        //case MouseMessage.LUp:
                        button = MouseButtons.Left;
                        break;
                    case MouseMessage.RDown:
                        //case MouseMessage.RUp:
                        button = MouseButtons.Right;
                        break;
                    case MouseMessage.MDown:
                        //case MouseMessage.MUp:
                        button = MouseButtons.Middle;
                        break;
                    case MouseMessage.XDown:
                        //case MouseMessage.XUp:
                        if (state.XButtonData.IsXButton1) button = MouseButtons.XButton1;
                        if (state.XButtonData.IsXButton2) button = MouseButtons.XButton2;
                        break;
                    default:
                        button = MouseButtons.None;
                        break;
                }

                if ((_captureMouseButtons.Length == 0 || _captureMouseButtons.Contains(_button)) &&
                    button != MouseButtons.None)
                {
                    var e = new GlobalMouseHookedEventArgs(
                        message,
                        button,
                        IsDoubleClick(button, state),
                        IsDesktop(state),
                        ref state);
                    OnMouseHooked(e);
                    if (e.Cancel)
                        return -1;
                }
            }
            return CallNextHookEx(_hook, code, message, ref state);
        }


        /// <summary>
        ///     デスクトップ判定
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private bool IsDesktop(MouseState state)
        {
            var point = state.Point;
            var hwnd = WindowFromPoint(point.X, point.Y);

            var sb = new StringBuilder(65535);
            GetWindowText(GetForegroundWindow(), sb, 65535);

            return hwnd == GetDesktopWindow()
                && IsHitTestNoIcon(hwnd, point.X, point.Y)
                && sb.ToString().Trim() == ""; // ホントにデスクトップかどうか（WindowFromPointは間違えたハンドルを返す場合があるので。semi transparent windowだと間違える？デスクトップのウインドウ名は空であるのでそれで判定）
        }

        /// <summary>
        /// 指定の座標にアイコンがないか
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>アイコンがない場合はtrue</returns>
        private bool IsHitTestNoIcon(IntPtr hwnd, int x, int y)
        {
            int pID = 0;
            IntPtr hProcess = IntPtr.Zero;
            IntPtr p = IntPtr.Zero;
            try
            {
                POINT pt = new POINT();
                pt.x = x;
                pt.y = y;
                ScreenToClient(hwnd, ref pt);
                GetWindowThreadProcessId(hwnd, ref pID);
                hProcess = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false, pID);
                if (hProcess != IntPtr.Zero)
                {
                    LVHITTESTINFO lhi = new LVHITTESTINFO();
                    int dw = 0;
                    lhi.pt = pt;
                    p = VirtualAllocEx(hProcess, IntPtr.Zero,
                        Marshal.SizeOf(lhi), MEM_COMMIT, PAGE_READWRITE);
                    WriteProcessMemory(hProcess, p, ref lhi, Marshal.SizeOf(lhi), ref dw);
                    SendMessage(hwnd, LVM_SUBITEMHITTEST, IntPtr.Zero, p);
                    ReadProcessMemory(hProcess, p, ref lhi, Marshal.SizeOf(lhi), ref dw);
                    return lhi.iItem < 0;
                }
            }
            finally
            {
                if (p != IntPtr.Zero)
                    VirtualFreeEx(hProcess, p, 0, MEM_RELEASE);
                if (hProcess != IntPtr.Zero)
                    CloseHandle(hProcess);
            }
            return false;
        }


        public static IntPtr GetDesktopWindow(DesktopWindow desktopWindow = DesktopWindow.SysListView32)
        {
            var _ProgMan = GetShellWindow();
            var _SHELLDLL_DefViewParent = _ProgMan;
            var _SHELLDLL_DefView = FindWindowEx(_ProgMan, IntPtr.Zero, "SHELLDLL_DefView", null);
            var _SysListView32 = FindWindowEx(_SHELLDLL_DefView, IntPtr.Zero, "SysListView32", "FolderView");

            if (_SHELLDLL_DefView == IntPtr.Zero)
            {
                EnumWindows((hwnd, lParam) =>
                {
                    if (GetClassName(hwnd) == "WorkerW")
                    {
                        var child = FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null);
                        if (child != IntPtr.Zero)
                        {
                            _SHELLDLL_DefViewParent = hwnd;
                            _SHELLDLL_DefView = child;
                            _SysListView32 = FindWindowEx(child, IntPtr.Zero, "SysListView32", "FolderView");
                            return false;
                        }
                    }
                    return true;
                }, IntPtr.Zero);
            }

            switch (desktopWindow)
            {
                case DesktopWindow.ProgMan:
                    return _ProgMan;
                case DesktopWindow.SHELLDLL_DefViewParent:
                    return _SHELLDLL_DefViewParent;
                case DesktopWindow.SHELLDLL_DefView:
                    return _SHELLDLL_DefView;
                case DesktopWindow.SysListView32:
                    return _SysListView32;
                default:
                    return IntPtr.Zero;
            }
        }

        private static string GetClassName(IntPtr hwnd)
        {
            int nRet;
            // Pre-allocate 256 characters, since this is the maximum class name length.
            var ClassName = new StringBuilder(256);
            //Get the window class name
            nRet = GetClassName(hwnd, ClassName, ClassName.Capacity);
            if (nRet != 0)
            {
                return ClassName.ToString();
            }
            return "";
        }

        /// <summary>
        ///     ダブルクリック判定
        /// </summary>
        /// <param name="button"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        internal bool IsDoubleClick(MouseButtons button, MouseState state)
        {
            // perform check
            var isDblClk = ((button == _button) && (InDoubleClickTime(state.Time)) && (InDoubleClickBounds(state.Point)));
            // update history
            _button = button;
            _time = state.Time;
            _position = state.Point;
            // return result
            return isDblClk;
        }

        /// <summary>
        ///     クリック時間がダブルクリック範囲内か
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        protected bool InDoubleClickTime(int time)
        {
            return ((time - _time) <= SystemInformation.DoubleClickTime);
        }

        /// <summary>
        ///     マウス位置がダブルクリック範囲内か
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        protected bool InDoubleClickBounds(Point pt)
        {
            var wd = SystemInformation.DoubleClickSize.Width / 2;
            var ht = SystemInformation.DoubleClickSize.Height / 2;
            return (((pt.X >= (_position.X - wd)) && (pt.X < (_position.X + wd))) &&
                    (((_position.Y >= (pt.Y - ht)) && (_position.Y <= (pt.Y + ht)))));
        }

        /// <summary>
        ///     使用されているアンマネージリソースを解放し、オプションでマネージリソースも解放する。
        /// </summary>
        /// <param name="disposing">マネージリソースも解放する場合はtrue。</param>
        protected override void Dispose(bool disposing)
        {
            if (_hookDelegate.IsAllocated)
            {
                UnhookWindowsHookEx(_hook);
                Debug.WriteLine("UnHook Mouse.");
                _hookDelegate.Free();
                if (disposing)
                {
                    _hook = IntPtr.Zero;
                }
            }
            base.Dispose(disposing);
        }

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private delegate int MouseHookDelegate(int code, MouseMessage message, ref MouseState state);
    }
}