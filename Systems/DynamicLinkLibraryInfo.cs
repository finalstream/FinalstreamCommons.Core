using System.Diagnostics;

namespace FinalstreamCommons.Systems
{
    /// <summary>
    ///     DLL情報を表します。
    /// </summary>
    public class DynamicLinkLibraryInfo
    {
        private readonly FileVersionInfo _version;

        /// <summary>
        ///     新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="libraryName"></param>
        /// <param name="categroy"></param>
        /// <param name="license"></param>
        /// <param name="copyright"></param>
        /// <param name="url"></param>
        public DynamicLinkLibraryInfo(string filePath, string libraryName, string categroy, string license,
            string copyright, string url)
        {
            FilePath = filePath;
            Categroy = categroy;
            LibraryName = libraryName;
            License = license;
            Copyright = copyright;
            Url = url;
            _version = FileVersionInfo.GetVersionInfo(FilePath);
        }

        /// <summary>
        ///     ファイルパスを取得します。
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        ///     カテゴリを取得します。
        /// </summary>
        public string Categroy { get; private set; }

        /// <summary>
        ///     ライブラリ名を取得します。
        /// </summary>
        public string LibraryName { get; private set; }

        /// <summary>
        ///     バージョン情報を取得します。
        /// </summary>
        public string Version
        {
            get { return _version.FileVersion.Replace(", ", "."); }
        }

        /// <summary>
        ///     ライセンスを取得します。
        /// </summary>
        public string License { get; private set; }

        /// <summary>
        ///     著作権を取得します。
        /// </summary>
        public string Copyright { get; private set; }

        /// <summary>
        ///     URLを取得します。
        /// </summary>
        public string Url { get; private set; }

        public string BBCodeUrl
        {
            get { return string.Format("[url={0}]{0}[/url]", Url); }
        }
    }
}