using System;
using System.Data.SQLite;

namespace FinalstreamCommons.Database
{
    /// <summary>
    /// データベースアクセサを表します。
    /// </summary>
    public interface IDatabaseAccessor : IDisposable
    {
        /// <summary>
        /// データベース名を取得します。
        /// </summary>
        string DatabaseName { get; }

        /// <summary>
        /// トランザクションを開始します。
        /// </summary>
        /// <returns></returns>
        SQLiteTransaction BeginTransaction();
    }
}