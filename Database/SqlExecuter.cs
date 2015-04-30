using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using FinalstreamCommons.Extentions;
using Newtonsoft.Json;
using NLog;

namespace FinalstreamCommons.Database
{
    /// <summary>
    ///     SQLを実行するものを表します。
    /// </summary>
    /// <remarks>コネクションを変更するときはインスタン明日</remarks>
    public class SQLExecuter : ISQLExecuter
    {
        /// <summary>
        ///     コネクション。
        /// </summary>
        private readonly SQLiteConnection _connection;

        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="databaseFilePath"></param>
        public SQLExecuter(string databaseFilePath, Type[] functionTypes = null)
        {
            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = databaseFilePath,
                //LegacyFormat = false,
                //Version = 3,
                SyncMode = SynchronizationModes.Normal,
                JournalMode = SQLiteJournalModeEnum.Wal
            };

            _connection = new SQLiteConnection(builder.ToString());
            _connection.Open();

            if (functionTypes != null)
            {
                foreach (Type functionType in functionTypes)
                {
                    SQLiteFunction.RegisterFunction(functionType);
                }
            }
        }

        /// <summary>
        ///     トランザクションを開始します。
        /// </summary>
        /// <returns></returns>
        public SQLiteTransaction BeginTransaction()
        {
            return _connection.BeginTransaction();
        }

        /// <summary>
        ///     DMLを実行します。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public int Execute(string sql, object param = null, SQLiteTransaction tran = null)
        {
            _log.Trace("[SQL] {0}", sql);
            if (param != null) param.TraceWriteJson("SQLPARAM");
            return _connection.Execute(sql, param, tran);
        }

        /// <summary>
        ///     Selectを実行します。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, SQLiteTransaction tran = null)
        {
            if (this.disposed) return Enumerable.Empty<T>();
            _log.Trace("[SQL] {0}", sql);
            if (param != null) _log.Trace("[SQLPARAM] {0}", JsonConvert.SerializeObject(param));
            return _connection.Query<T>(sql, param, tran);
        }

        /// <summary>
        ///     Selectを実行します。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public dynamic Query(string sql, object param = null, SQLiteTransaction tran = null)
        {
            _log.Trace("[SQL] {0}", sql);
            if (param != null) _log.Trace("[SQLPARAM] {0}", JsonConvert.SerializeObject(param));
            return _connection.Query(sql, param, tran);
        }

        #region Dispose

        // Flag: Has Dispose already been called?
        private bool disposed;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                _connection.Close();
                _connection.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        #endregion
    }
}