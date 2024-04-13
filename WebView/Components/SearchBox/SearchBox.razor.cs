using Microsoft.AspNetCore.Components;

namespace WebView;

public partial class SearchBox<T> : BaseComponent
{
    private string? _filter;
    private int _filterChanged;
    private readonly List<object> _suggestions = new();
    private TextField? _textFieldRef;

    private ICollection<IRule> DropdownLocalRules { get; set; } = new List<IRule>();
    protected bool IsOpen { get; set; }

#pragma warning disable BL0007 // Component parameters should be auto properties.
    [Parameter] public int Delay { get; set; }

    [Parameter] public string? Filter
    {
        get
        {
            return _filter;
        }
        set
        {
            if(_filter != value)
            {
                _filter = value;
                _filterChanged += 2;
                SearchNewEntries();
                if (!IsMultiSelect)
                {
                    SelectedItemChanged.InvokeAsync();
                }
            }
        }
    }

    [Parameter] public double InputWidth { get; set; } = 200;
    [Parameter] public string IconName { get; set; } = "search";
    [Parameter] public string? IconSrc { get; set; }
    [Parameter] public bool IsDropDownOpen { get; set; }
    [Parameter] public bool IsLoading { get; set; }

    [Parameter] public T? SelectedItem { get; set; }
    [Parameter] public EventCallback<T> SelectedItemChanged { get; set; }
    [Parameter] public ICollection<T>? SelectedItems { get; set; }
    [Parameter] public EventCallback<ICollection<T>> SelectedItemsChanged { get; set; }
    [Parameter] public string Placeholder { get; set; } = "Enter here";
    [Parameter] public bool IsMultiSelect { get; set; }
    [Parameter] public Func<string?, IEnumerable<T>?>? ProvideSuggestions { get; set; }
    [Parameter] public Func<object, string>? ProvideString { get; set; }
    [Parameter] public int DropdownWidth { get; set; } = 0;

    [Parameter] public EventCallback<bool> ContextMenuShownChanged { get; set; }
    [Parameter] public RenderFragment<T>? SearchItemTemplate { get; set; }
    [Parameter] public RenderFragment<T>? SelectedItemTemplate { get; set; }
#pragma warning restore BL0007 // Component parameters should be auto properties.

    void SearchNewEntries()
    {
        _suggestions.Clear();
        IEnumerable<T>? suggestionsInt = ProvideSuggestions!(_filter);
        if (suggestionsInt != null)
        {
            foreach (T suggestionInt in suggestionsInt)
            {
                 _suggestions.Add(suggestionInt!);
            }
        }
        IsOpen = true;
    }

    protected  override async void OnAfterRender(bool firstRender)
    {
        if (_filterChanged > 0 && _textFieldRef != null)
        {
            await _textFieldRef.Focus();
            _filterChanged--;
        }
        base.OnAfterRender(firstRender);
    }

    protected void DismissHandler()
    {
        IsOpen = false;
    }

    void ClickedSelectHandler(SearchItem<T> searchItem)
    {
        if (IsMultiSelect)
        {
            Filter = "";
            if(SelectedItems == null)
            {
                SelectedItems = new List<T>();
            }
            if (searchItem.Content != null && !SelectedItems.Contains(searchItem.Content))
            {
                SelectedItems.Add(searchItem.Content);
            }
            SelectedItemsChanged.InvokeAsync(SelectedItems);
        }
        else
        {
            if (searchItem.Content is string stringContent)
            {
                Filter = stringContent;
            }
            else
            {
                Filter = ProvideString!(searchItem.Content!);
            }
            SelectedItemChanged.InvokeAsync(searchItem.Content);
        }
        IsOpen = false;
    }

    void ClickedDeletedHandler(SelectedItem<T> selectedItem)
    {
        SelectedItems?.Remove(selectedItem.Content!);
        SelectedItemsChanged.InvokeAsync(SelectedItems);
    }
}
