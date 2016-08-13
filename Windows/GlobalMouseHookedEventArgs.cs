using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FinalstreamCommons.Windows
{
    public class GlobalMouseHookedEventArgs : CancelEventArgs
    {
        private MouseState state;

        /// <summary>
        ///     新しいインスタンスを作成する。
        /// </summary>
        /// ec
        /// <param name="message">マウス操作の種類を表すMouseMessage値の一つ。</param>
        /// <param name="button"></param>
        /// <param name="isDoubleClick"></param>
        /// <param name="isDesktop"></param>
        /// <param name="state">マウスの状態を表すMouseState構造体。</param>
        internal GlobalMouseHookedEventArgs(MouseMessage message, MouseButtons button, bool isDoubleClick,
            bool isDesktop, ref MouseState state)
        {
            Message = message;
            this.state = state;
            MouseButton = button;
            IsDoubleClick = isDoubleClick;
            IsDesktop = isDesktop;
        }

        /// <summary>マウス操作の種類を表すMouseMessage値。</summary>
        public MouseMessage Message { get; }

        /// <summary>スクリーン座標における現在のマウスカーソルの位置。</summary>
        public Point Point
        {
            get { return state.Point; }
        }

        /// <summary>ホイールの情報を表すWheelData構造体。</summary>
        public WheelData WheelData
        {
            get { return state.WheelData; }
        }

        /// <summary>XButtonの情報を表すXButtonData構造体。</summary>
        public XButtonData XButtonData
        {
            get { return state.XButtonData; }
        }

        /// <summary>
        ///     ダブルクリックかどうか
        /// </summary>
        public bool IsDoubleClick { get; private set; }

        public MouseButtons MouseButton { get; private set; }
        public bool IsDesktop { get; private set; }
    }
}