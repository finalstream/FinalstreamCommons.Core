using System;

namespace FinalstreamCommons.Frameworks
{
    /// <summary>
    /// アプリケーションのクライアントを表します。
    /// </summary>
    public interface IAppClient : IDisposable
    {
        /// <summary>
        /// アプリケーション設定を取得します。
        /// </summary>
        IAppConfig AppConfig { get; }

        /// <summary>
        /// アプリケーションが初期化済みかどうか。
        /// </summary>
        bool IsInitialized { get; }

        
    }
}