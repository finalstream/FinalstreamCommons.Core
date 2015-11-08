using System.IO;
using Microsoft.VisualBasic.FileIO;

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

            MoveRecycleBin(direcotry);
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

        /// <summary>
        ///     フォルダをゴミ箱に移動する
        /// </summary>
        /// <param name="dirPath"></param>
        public static void MoveRecycleBin(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                FileSystem.DeleteDirectory(
                    dirPath,
                    UIOption.OnlyErrorDialogs,
                    RecycleOption.SendToRecycleBin);
            }
        }
    }
}