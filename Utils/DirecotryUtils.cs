﻿using System.IO;

namespace FinalstreamCommons.Utils
{
    /// <summary>
    ///     ディレクトリを扱うユーティリティです。
    /// </summary>
    public static class DirecotryUtils
    {
        /// <summary>
        ///     ディレクトリが空の場合、削除します。
        /// </summary>
        /// <param name="direcotry"></param>
        /// <returns></returns>
        public static void DeleteEmptyDirecotry(string direcotry)
        {
            if (!IsEmptyDirectory(direcotry)) return;

            Directory.Delete(direcotry);
        }

        /// <summary>
        ///     ディレクトリが空であるかどうか。
        /// </summary>
        /// <param name="direcotry"></param>
        /// <returns></returns>
        public static bool IsEmptyDirectory(string direcotry)
        {
            if (!Directory.Exists(direcotry)) return false; // ディレクトリが存在しなければ空でないとする

            try
            {
                var files = Directory.GetFileSystemEntries(direcotry);
                return files.Length == 0;
            }
            catch
            {
                // アクセス権がないなどの場合は空でないとする
                return false;
            }
        }
    }
}