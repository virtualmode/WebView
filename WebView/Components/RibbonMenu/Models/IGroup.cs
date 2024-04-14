namespace WebView.Models;

public interface IGroup
{
    ICollection<IRibbonItem> ItemsSource { get; set; }
}
