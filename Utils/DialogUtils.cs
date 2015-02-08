﻿using System;
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
        /// ファイルダイアログを開きます。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <returns>選択されたファイルパスを返却します。キャンセルされた場合はnullを返却します。</returns>
        public static string ShowFileDialog(
            string title = "開くファイルを選択してください", 
            string filter = "すべてのファイル(*.*)|*.*")
        {
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

    }
}
