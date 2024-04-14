using System.Reactive.Linq;

//using DynamicData;
//using DynamicData.Binding;

namespace Samples.Models;

public class GroupedDataItem : DataItem
{
    private System.Reactive.Disposables.CompositeDisposable disposables = new();
    public System.Collections.Generic.List<GroupedDataItem>? Data { get; set; }
    public List<GroupedDataItem> ObservableData { get; set; } = new List<GroupedDataItem>();
    //public IObservableCollection<GroupedDataItem> ObservableData { get; set; } = new ObservableCollectionExtended<GroupedDataItem>();

    /*public GroupedDataItem(IGroup<DataItem, string, int> group, IObservable<SortExpressionComparer<GroupedDataItem>> sortExpressionObservable)
    {
        IDisposable disposable = group.Cache.Connect()
            .Transform(x => new GroupedDataItem(x))
            .Sort(sortExpressionObservable)
            .Do(_=>Debug.WriteLine("groupDataItem has updated sort"))
            .Bind(ObservableData)
            .Subscribe();
        disposables.Add(disposable);

        //Key = group.Key.ToString();
        Key = group.Cache.Items.First().Key + " - " + group.Cache.Items.Last().Key;
        KeyNumber = group.Key;
        DisplayName = group.Cache.Items.First().Key + " - " + group.Cache.Items.Last().Key;
        Description = group.Cache.Items.First().Key + " - " + group.Cache.Items.Last().Key;
    }*/

    public GroupedDataItem(IGrouping<int, DataItem> grouping)
    {
        Data = grouping.Select(x => new GroupedDataItem(x)).ToList();
        Key = grouping.Key.ToString();
    }

    public GroupedDataItem(DataItem dataItem)
    {
        DisplayName = dataItem.DisplayName;
        Type = dataItem.Type;
        Key = dataItem.Key;
        KeyNumber = dataItem.KeyNumber;
        Description = dataItem.Description;
    }
}
