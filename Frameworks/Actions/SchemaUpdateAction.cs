using NLog;

namespace FinalstreamCommons.Frameworks.Actions
{
    /// <summary>
    /// スキーマのアップグレードを行うアクションを表します。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SchemaUpgradeAction<T> : GeneralAction<T> where T : IAppClient
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// アップグレード後のスキーマバージョンを取得します。
        /// </summary>
        /// <returns></returns>
        protected abstract int GetUpgradeSchemaVersion();

        /// <summary>
        /// アップグレードが必要かどうか判断します。
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsNeedUpgrade(T client)
        {
            return client.AppConfig.SchemaVersion < GetUpgradeSchemaVersion();
        }

        /// <summary>
        /// アップグレードを行います。
        /// </summary>
        /// <param name="client"></param>
        protected abstract bool InvokeUpgrade(T client);

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="client"></param>
        public override void InvokeCore(T client)
        {
            if (!IsNeedUpgrade(client)) return; // アップグレードの必要がない。
            var result = InvokeUpgrade(client);

            if (!result)
            {
                _log.Error("Fail Database Schema Upgrade.");
                return;
            }

            // スキーマバージョン更新。
            var prevSchemaVersion = client.AppConfig.SchemaVersion;
            client.AppConfig.UpdateSchemaVersion(GetUpgradeSchemaVersion());   

            _log.Info("Database Schema Upgraded. Version:{0} to {1}", prevSchemaVersion, client.AppConfig.SchemaVersion);
        }
    }
}
