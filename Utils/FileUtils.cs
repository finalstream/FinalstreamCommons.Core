using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace FinalstreamCommons.Utils
{
    /// <summary>
    ///     ファイルを扱うユーティリティです。
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        ///     ファイルサイズをGBの文字列に変換します。
        /// </summary>
        /// <param name="fileSize">変換元のファイルサイズ。</param>
        /// <returns>変換後のファイルサイズ。</returns>
        public static string ConvertFileSizeGigaByteString(long fileSize)
        {
            return String.Format("{0:###0.#0} GB", fileSize/1073741824.0);
        }

        /// <summary>
        ///     ファイルサイズを取得します。
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static long GetFileSize(string filePath)
        {
            return new FileInfo(filePath).Length;
        }

        /// <summary>
        ///     ファイルを移動します。移動先にすでに同名のファイルが存在する場合は何もしません。
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        /// <returns>移動元と移動先が同じか、移動先にすでに同名のファイルが存在する場合はfalse。成功したらtrue。</returns>
        /// <remarks></remarks>
        public static bool Move(string srcPath, string destPath)
        {
            if (srcPath == destPath || File.Exists(destPath)) return false;

            File.Move(srcPath, destPath);
            return true;
        }

        public static string GetDriveLetter(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return "";

            return filePath.Substring(0, 1).ToUpper();
        }

        /// <summary>
        ///     ファイルをゴミ箱に移動する
        /// </summary>
        /// <param name="filePath"></param>
        public static void MoveRecycleBin(string filePath)
        {
            if (!File.Exists(filePath)) return;

            FileSystem.DeleteFile(
                filePath,
                UIOption.OnlyErrorDialogs,
                RecycleOption.SendToRecycleBin);
        }
    }
}