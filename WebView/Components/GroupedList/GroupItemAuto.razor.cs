//using DynamicData;
//using DynamicData.Binding;
//using DynamicData.Cache;
//using DynamicData.Aggregation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebView.Lists;

public partial class GroupItemAuto<TItem, TKey> : BaseComponent, IAsyncDisposable
{
    private List<IGroupedListItem3<TItem>>? listReference;

    private const double COMPACT_ROW_HEIGHT = 32;
    private const double ROW_HEIGHT = 42;

    [CascadingParameter]
    public SelectionZone<TItem>? SelectionZone { get; set; }

    [Parameter]
    public bool Compact { get; set; }

    /// <summary>
    /// GetKey must get a key that can be transformed into a unique string because the key will be written as HTML. You can leave this null if your ItemsSource implements IList as the index will be used as a key.
    /// </summary>
    [Parameter]
    public Func<TItem, TKey>? GetKey { get; set; }

    [Parameter]
    public bool IsVirtualizing { get; set; } = true;

    [Parameter]
    public Func<TItem, MouseEventArgs, Task>? ItemClicked { get; set; }

    [Parameter]
    public ICollection<IGroupedListItem3<TItem>>? ItemsSource { get; set; }

    [Parameter]
    public RenderFragment<IndexedItem<IGroupedListItem3<TItem>>>? ItemTemplate { get; set; }

    [Parameter]
    public EventCallback<bool> OnGroupExpandedChanged { get; set; }

    [Parameter]
    public Action<IndexedItem<IGroupedListItem3<TItem>>>? OnHeaderClick { get; set; }

    [Parameter]
    public Action<IndexedItem<IGroupedListItem3<TItem>>>? OnHeaderToggle { get; set; }

    [Parameter]
    public Func<bool> OnShouldVirtualize { get; set; } = () => true;

    [Parameter]
    public EventCallback<Viewport> OnViewportChanged { get; set; }

    [Parameter]
    public Selection<TItem>? Selection { get; set; }

    [Parameter]
    public SelectionMode SelectionMode { get; set; } = SelectionMode.Single;

    [Parameter]
    public int StartIndex { get; set; }

#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
    Dictionary<HeaderItem3<TItem, TKey>, IDisposable> headerSubscriptions = new();
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.

    protected override Task OnInitializedAsync()
    {

        return base.OnInitializedAsync();
    }

    public void ToggleSelectAll()
    {
    }

    public bool ShouldAllBeSelected()
    {
        return false;
    }

    private static void OnHeaderClicked(IndexedItem<IGroupedListItem3<TItem>> indexedItem)
    {
    }

    private static void OnHeaderToggled(IndexedItem<IGroupedListItem3<TItem>> indexedItem)
    {
    }

    public void ForceUpdate()
    {
        StateHasChanged();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (GetKey == null)
            throw new Exception("Must have GetKey.");

        await base.OnParametersSetAsync();

    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        //Debug.WriteLine($"There are {groupedUIListItems.Count} items to render");
        return base.OnAfterRenderAsync(firstRender);
    }

    public override ValueTask DisposeAsync()
    {
        try
        {
#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
            foreach (KeyValuePair<HeaderItem3<TItem, TKey>, IDisposable> header in headerSubscriptions)
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
            {
                header.Value.Dispose();
            }
        }
        catch (TaskCanceledException)
        {
        }
        return ValueTask.CompletedTask;
    }
}
