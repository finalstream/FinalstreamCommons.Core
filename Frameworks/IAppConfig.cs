using System.Windows;

namespace FinalstreamCommons.Frameworks
{
    /// <summary>
    /// アプリケーション設定を表します。
    /// </summary>
    public interface IAppConfig
    {
        /// <summary>
        /// アプリバージョンを取得します。
        /// </summary>
        string AppVersion { get; }

        /// <summary>
        /// ウインドウの位置、大きさを取得します。
        /// </summary>
        Rect WindowBounds { get; }

    }
}