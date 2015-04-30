using System;
using System.Data.SQLite;

namespace FinalstreamCommons.Database
{
    /// <summary>
    /// データベースへアクセスするためのアクセサを表します。
    /// </summary>
    public abstract class DatabaseAccessor : IDatabaseAccessor
    {
        /// <summary>
        /// データベース名を取得します。
        /// </summary>
        public string DatabaseName { get; protected set; }

        /// <summary>
        /// SQLエグゼキュータを取得または設定します。
        /// </summary>
        protected SQLExecuter SqlExecuter { get; set; }

        /// <summary>
        /// トランザクションを開始します。
        /// </summary>
        /// <returns></returns>
        public SQLiteTransaction BeginTransaction()
        {
            return SqlExecuter.BeginTransaction();
        }

        #region Dispose

        // Flag: Has Dispose already been called?
        private bool _disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                if (SqlExecuter != null) SqlExecuter.Dispose();
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }

        #endregion
    }
}
