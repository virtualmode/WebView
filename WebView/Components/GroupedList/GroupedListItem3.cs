using System.Reactive.Linq;
using System.Reactive.Subjects;

//using DynamicData;
//using DynamicData.Aggregation;
//using DynamicData.Binding;

namespace WebView.Lists;

public class HeaderItem3<TItem, TKey> : IGroupedListItem3<TItem> where TKey : notnull
{
    public bool IsOpen
    {
        get => isOpenSubject.Value;
        set
        {
            if (isOpenSubject.Value != value)
                isOpenSubject.OnNext(value);
        }
    }

    private BehaviorSubject<bool> isOpenSubject;
    public IObservable<bool> IsOpenObservable => isOpenSubject.AsObservable();

    public bool IsVisible => true;

    public int Count
    {
        get => countSubject.Value;
        set
        {
            countSubject.OnNext(value);
        }

    }
    private BehaviorSubject<int> countSubject;
    public IObservable<int> CountChanged => countSubject.AsObservable();

    public int Depth { get; private set; }

    public string? Name { get; private set; }

    public ICollection<IGroupedListItem3<TItem>>? Items { get; private set; }

    //private IGroup<TItem, TKey, object>? _group;

    public int GroupIndex { get; set; }

    public TItem? Item { get; private set; }

    /// <summary>
    /// This constructor is used for the plain GroupedList where filtering and sorting of the list items must be done before adding to the list.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="depth"></param>
    /// <param name="index"></param>
    /// <param name="subGroupSelector"></param>
    /// <param name="groupTitleSelector"></param>
    public HeaderItem3(TItem item, int depth, int index, Func<TItem, IEnumerable<TItem>> subGroupSelector, Func<TItem, string> groupTitleSelector)
    {
        Item = item;
        Depth = depth;
        GroupIndex = index;
        Items = new System.Collections.Generic.List<IGroupedListItem3<TItem>>();

        Name = groupTitleSelector(item);

        isOpenSubject = new BehaviorSubject<bool>(true);
        countSubject = new BehaviorSubject<int>(0);

        IEnumerable<TItem>? subItems = subGroupSelector(Item);
        int cummulativeCount = GroupIndex;
        foreach (TItem? subItem in subItems)
        {
            // Check if is plain or header.
            if (subGroupSelector(subItem) == null || !subGroupSelector(subItem).Any())
            {
                Items.Add(new PlainItem3<TItem, TKey>(subItem, depth + 1));
                cummulativeCount++;
            }
            else
            {
                Items.Add(new HeaderItem3<TItem, TKey>(subItem, depth + 1, cummulativeCount, subGroupSelector, groupTitleSelector));
                int subItemCount = GroupedList<TItem, TKey>.GetPlainItemsCount(subItem, subGroupSelector);
                cummulativeCount += subItemCount;
            }
        }
        countSubject.OnNext(cummulativeCount);
    }

    /// <summary>
    /// This constructor is used for the GroupedListAuto where filtering and sorting is done internally.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="groupBy"></param>
    /// <param name="depth"></param>
    /// <param name="groupsChangeSet"></param>
    /// <param name="parent"></param>
    /// <param name="sortComparer"></param>
    /// <param name="stateChangeCallback"></param>
    /// <param name="reindexTrigger"></param>
    //public HeaderItem3(IGroup<TItem, TKey, object>? group, IEnumerable<Func<TItem, object>>? groupBy, int depth, IConnectableObservable<ISortedChangeSet<IGroup<TItem, TKey, object>, object>> groupsChangeSet, HeaderItem3<TItem, TKey>? parent, IObservable<IComparer<IGroupedListItem3<TItem>>> sortComparer, Func<Task> stateChangeCallback, Subject<Unit> reindexTrigger)
    //{
    //    _group = group;
    //    Depth = depth;
    //    isOpenSubject = new BehaviorSubject<bool>(true);
    //    countSubject = new BehaviorSubject<int>(0);

    //    Name = group?.Key.ToString();

    //    IKeyValueCollection<IGroup<TItem, TKey, object>, object>? sortedItems = null;

    //    groupsChangeSet.CombineLatest(CountChanged, (groups, count) => (groups, count)).Subscribe(x =>
    //    {
    //        sortedItems = x.groups.SortedItems;
    //        int groupIndex = x.groups.SortedItems.TakeWhile(x => x.Key != _group?.Key).Aggregate(0, (v, x) =>
    //        {
    //            v += x.Value.Cache.Count;
    //            return v;
    //        });
    //        if (parent != null)
    //        {
    //            groupIndex += parent.GroupIndex;
    //        }
    //        if (groupIndex != GroupIndex)
    //        {
    //            GroupIndex = groupIndex;
    //            reindexTrigger.OnNext(Unit.Default);
    //        }
    //    });

    //    reindexTrigger.Subscribe(_ =>
    //    {
    //        if (sortedItems != null)
    //        {
    //            int groupIndex = sortedItems.TakeWhile(x => x.Key != _group?.Key).Aggregate(0, (v, x) =>
    //            {
    //                v += x.Value.Cache.Count;
    //                return v;
    //            });
    //            if (parent != null)
    //            {
    //                groupIndex += parent.GroupIndex;
    //            }
    //            if (groupIndex != GroupIndex)
    //            {
    //                GroupIndex = groupIndex;
    //                reindexTrigger.OnNext(Unit.Default);
    //            }
    //        }
    //    });

    //    if (groupBy != null && groupBy.Any())
    //    {
    //        Func<TItem, object>? firstGroup = groupBy.First();
    //        IEnumerable<Func<TItem, object>>? rest = groupBy.Skip(1);


    //        IConnectableObservable<ISortedChangeSet<IGroup<TItem, TKey, object>, object>>? published = _group?.Cache.Connect()
    //            .Group(firstGroup)
    //            .Sort(SortExpressionComparer<IGroup<TItem, TKey, object>>.Ascending(x => (x.Key as IComparable)!))
    //            .Replay();

    //        if (published != null)
    //        {

    //            published.ToCollection().Subscribe(collection =>
    //            {
    //                int count = collection.Aggregate(0, (v, x) =>
    //                {
    //                    v += x.Cache.Count;
    //                    return v;
    //                });
    //                if (count != countSubject.Value)
    //                {
    //                    countSubject.OnNext(count);
    //                    stateChangeCallback();
    //                }
    //            });

    //            published
    //                .Transform(group => new HeaderItem3<TItem, TKey>(group, rest, depth + 1, published, this, sortComparer, stateChangeCallback, reindexTrigger) as IGroupedListItem3<TItem>)
    //                .Bind(out System.Collections.ObjectModel.ReadOnlyObservableCollection<IGroupedListItem3<TItem>>? items)
    //                .Subscribe();

    //            published.Connect();

    //            Items = items;
    //        }
    //    }
    //    else
    //    {
    //        sortComparer.Subscribe(x => Debug.WriteLine("sort changed"));
    //        _group!.Cache.Connect()
    //            .Transform(x => new PlainItem3<TItem, TKey>(x, depth + 1) as IGroupedListItem3<TItem>)
    //            .Sort(sortComparer)
    //            .Bind(out System.Collections.ObjectModel.ReadOnlyObservableCollection<IGroupedListItem3<TItem>>? items)
    //            .Do(x =>
    //            {
    //                stateChangeCallback.Invoke();
    //            })
    //            .Subscribe();

    //        if (items != null)
    //            countSubject = new BehaviorSubject<int>(items.Count);

    //        _group?.Cache.Connect().QueryWhenChanged(x => x.Count).Subscribe(x => countSubject.OnNext(x));

    //        Items = items;
    //    }
    //}
}

public class PlainItem3<TItem, TKey> : IGroupedListItem3<TItem>
{
    public PlainItem3(TItem item, int depth)
    {
        Item = item;
        Depth = depth;

        if (Item == null)
            throw new Exception("Item is null!");
    }
    public int Depth { get; private set; }
    public bool IsVisible { get; set; }

    public int Count => 1;

    public string? Name { get; private set; }

    public TItem? Item { get; private set; }

    public ICollection<IGroupedListItem3<TItem>>? Items => null;
}
