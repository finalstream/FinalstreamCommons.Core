namespace FinalstreamCommons.Collections
{
    /// <summary>
    ///     選択可能なアイテムを表します。
    /// </summary>
    public interface ISelectableItem
    {
        bool IsSelected { get; set; }
    }
}