using WebView;

namespace Samples.Models;

public class DataItemGrouped
{
    public string? Key { get; set; }
    public int KeyNumber { get; set; }
    public string? DisplayName { get; set; }
    public string? LongName { get; set; }
    public string? Description { get; set; }
    public static string ImgUrl => "redArrow.jpg";

    public SelectableOptionMenuItemType Type { get; set; }

    public static Random random = new();

    public DataItemGrouped()
    {
    }

    public DataItemGrouped(int num)
    {
        Key = num.ToString();
        KeyNumber = num;
        int start = (int)Math.Round(random.NextDouble() * 40);
        DisplayName = LoremUtils.Lorem(start, 5); // = num.ToString();
        Description = LoremUtils.Lorem(start, 5 + (int)Math.Round(random.NextDouble() * 50));
    }

    public DataItemGrouped(string text)
    {
        DisplayName = text;
        Key = text;
    }

    public DataItemGrouped(string text, SelectableOptionMenuItemType selectableOptionMenuItemType)
    {
        DisplayName = text;
        Type = selectableOptionMenuItemType;
    }
}
