namespace FinalstreamCommons.Builders
{
    /// <summary>
    /// コマンドラインの文字列を生成します。
    /// </summary>
    public class CommandLineBuilder : Microsoft.Build.Utilities.CommandLineBuilder
    {

        /// <summary>
        /// クリアします。
        /// </summary>
        public void Clear()
        {
            CommandLine.Clear();
        }
    }
}
