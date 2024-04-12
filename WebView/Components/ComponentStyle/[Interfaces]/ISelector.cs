namespace WebView
{
    public interface ISelector
    {
        string? SelectorName { get; set; }
        string? GetSelectorAsString();
    }
}
