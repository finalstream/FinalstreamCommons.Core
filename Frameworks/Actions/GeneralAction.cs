using System;
using NLog;

namespace FinalstreamCommons.Frameworks.Actions
{
    /// <summary>
    /// 標準のアクションを表します。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GeneralAction<T> : IGeneralAction<T>
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// アクション実行後に行うアクションを取得または設定します。
        /// </summary>
        public Action AfterAction { get; set; }

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="client"></param>
        public abstract void InvokeCore(T client);

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="client"></param>
        public void Invoke(T client)
        {
            InvokeCore(client);
            if (AfterAction != null) AfterAction.Invoke();
            _log.Debug("Executed Action. ActionType:{0}", this.GetType());
        }
    }
}
