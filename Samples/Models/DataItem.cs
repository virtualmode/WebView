using WebView;

namespace Samples.Models;

public class DataItem
{
    public string? Key { get; set; }
    public int KeyNumber { get; set; }
    public string? DisplayName { get; set; }
    public string? LongName { get; set; }
    public string? Description { get; set; }
    public static string ImgUrl => "redArrow.jpg";
    public string? GroupName { get; set; }
    public SelectableOptionMenuItemType Type { get; set; }
    public static Random random = new();

    public DataItem()
    {
    }

    public DataItem(int num)
    {
        Key = num.ToString();
        KeyNumber = num;
        int start = (int)Math.Round(random.NextDouble() * 40);
        int limitedGrouping = (int)Math.Round(random.NextDouble() * 20);
        GroupName = LoremUtils.Lorem(limitedGrouping, 1);
        DisplayName = LoremUtils.Lorem(start, 5); // = num.ToString();
        Description = LoremUtils.Lorem(start, 5 + (int)Math.Round(random.NextDouble() * 50));
    }

    public DataItem(string text)
    {
        DisplayName = text;
        Key = text;
    }

    public DataItem(string text, SelectableOptionMenuItemType selectableOptionMenuItemType)
    {
        DisplayName = text;
        Type = selectableOptionMenuItemType;
    }
}
