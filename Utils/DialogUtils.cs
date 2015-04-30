using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalstreamCommons.Utils
{
    /// <summary>
    /// ダイアログを扱うユーティリティです。
    /// </summary>
    public static class DialogUtils
    {
        /// <summary>
        /// ファイル選択ダイアログを開きます。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <returns>選択されたファイルパスを返却します。キャンセルされた場合はnullを返却します。</returns>
        public static string ShowFileDialog(
            string title = null, 
            string filter = null)
        {
            if (title == null) title = Properties.Resources.FileDialogTitle;
            if (filter == null) filter = Properties.Resources.AllFilesFilter;

            string resultFilePath = null;

            //OpenFileDialogクラスのインスタンスを作成
            var ofd = new OpenFileDialog();

            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter = filter;

            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 1;

            //タイトルを設定する
            ofd.Title = title;

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            //ofd.RestoreDirectory = true;

            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;

            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                resultFilePath = ofd.FileName;
            }

            return resultFilePath;

        }

        /// <summary>
        /// フォルダ選択ダイアログを開きます。
        /// </summary>
        /// <param name="description"></param>
        /// <param name="selectedPath"></param>
        /// <returns>選択されたフォルダパスを返却します。キャンセルされた場合はnullを返却します。</returns>
        public static string ShowFolderDialog(
            string description = null,
            string selectedPath = "")
        {
            if (description == null) description = Properties.Resources.FolderDialogDescription;

            string resultDirPath = null;

            //OpenFileDialogクラスのインスタンスを作成
            var fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = description;

            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;

            fbd.SelectedPath = selectedPath;
            //ユーザーが新しいフォルダを作成できるようにする
            fbd.ShowNewFolderButton = true;

            //ダイアログを表示する
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                resultDirPath = fbd.SelectedPath;
            }

            return resultDirPath;

        }

    }
}
