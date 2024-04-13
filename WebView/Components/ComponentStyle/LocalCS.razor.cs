using Microsoft.AspNetCore.Components;

namespace WebView;

public partial class LocalCS : ComponentBase, ILocalCSSheet, IDisposable
{
    private static long _idGenerator = 0;

    private string? css;
    private ICollection<IRule>? rules;

    protected long ScopeId { get; set; }

    [Inject]
    public IComponentStyle? ComponentStyle { get; set; }

#pragma warning disable BL0007 // Component parameters should be auto properties.
    [Parameter]
    public ICollection<IRule> Rules
    {
        get => rules!;
        set
        {
            if (value == rules)
            {
                return;
            }
            rules = value;
            RulesChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<ICollection<IRule>> RulesChanged { get; set; }
#pragma warning restore BL0007 // Component parameters should be auto properties.

    protected override async Task OnInitializedAsync()
    {
        ScopeId = Interlocked.Add(ref _idGenerator, 1);
        ComponentStyle!.LocalCSSheets.Add(this);
        SetSelectorNames();
        await base.OnInitializedAsync();
    }

    protected override void OnParametersSet()
    {
        css = string.Join(string.Empty, Rules.Select(x => ComponentStyle!.PrintRule(x)));
        base.OnParametersSet();
    }

    private void SetSelectorNames()
    {
        foreach (IRule? rule in rules!)
        {
            Rule? innerRule = rule as Rule;
            if (innerRule!.Selector?.GetType() == typeof(IdSelector) || innerRule.Selector?.GetType() == typeof(MediaSelector))
                continue;
            if (string.IsNullOrWhiteSpace(innerRule.Selector?.SelectorName))
            {
                innerRule.Selector!.SelectorName = $"css-{ScopeId}-{rules.ToList().IndexOf(innerRule)}";
            }
            else
            {
                innerRule.Selector.SelectorName = $"{innerRule.Selector.SelectorName}-{ScopeId}-{rules.ToList().IndexOf(innerRule)}";
            }
        }
        RulesChanged.InvokeAsync(rules);
    }

    public void Dispose()
    {
        ComponentStyle!.LocalCSSheets.Remove(this);
        GC.SuppressFinalize(this);
    }
}
