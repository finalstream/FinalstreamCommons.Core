using System;
using System.IO;

namespace FinalstreamCommons.Database
{

    /// <summary>
    /// SQLエグゼキュータを生成するファクトリを表します。
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLExecuterFactory
    {

        /// <summary>
        /// SQLエグゼキュータを生成します。
        /// </summary>
        /// <param name="databaseFilePath">データベースファイルパス</param>
        /// <param name="blankDatabaseFilePath">ブランクのデータベースファイルパス</param>
        /// <param name="sqliteFunctions"></param>
        /// <returns></returns>
        public static SQLExecuter Create(string databaseFilePath,
            string blankDatabaseFilePath,
            Type[] sqliteFunctions)
        {
            if (!File.Exists(databaseFilePath)) CreateBlankDatabase(blankDatabaseFilePath, databaseFilePath);

            return new SQLExecuter(databaseFilePath, sqliteFunctions);
        }

        private static void CreateBlankDatabase(string blankDatabaseFilePath, string databaseFilePath)
        {
            File.Copy(blankDatabaseFilePath, databaseFilePath);
        }
    }
}
