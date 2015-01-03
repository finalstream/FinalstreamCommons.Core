using System;
using System.Data.SQLite;

namespace FinalstreamCommons.Models
{
    public interface ISQLExecuter : IDisposable
    {
        /// <summary>
        ///     トランザクションを開始します。
        /// </summary>
        /// <returns></returns>
        SQLiteTransaction BeginTransaction();

        /// <summary>
        ///     DMLを実行します。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        int Execute(string sql, object param = null, SQLiteTransaction tran = null);

        /// <summary>
        ///     Selectを実行します。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        dynamic Query(string sql, object param = null, SQLiteTransaction tran = null);
    }
}