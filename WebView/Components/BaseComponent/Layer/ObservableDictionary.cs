using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WebView;

// from https://richardwilburn.wordpress.com/2009/07/13/observable-dictionary/
public class ObservableDictionary<TKey, TValue> :
    IDictionary<TKey, TValue?>,
    INotifyCollectionChanged,
    INotifyPropertyChanged where TKey : notnull
{
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    readonly IDictionary<TKey, TValue?> _dictionary = new Dictionary<TKey, TValue?>();

    public IEnumerator<KeyValuePair<TKey, TValue?>> GetEnumerator() => _dictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(KeyValuePair<TKey, TValue?> item)
    {
        _dictionary.Add(item);

        if (CollectionChanged != null)
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));

        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Keys"));
            PropertyChanged(this, new PropertyChangedEventArgs("Values"));
        }
    }

    public void Clear()
    {
        int keysCount = _dictionary.Keys.Count;
        _dictionary.Clear();

        if (keysCount == 0) return; //dont trigger changed event if there was no change.

        if (CollectionChanged != null)
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Keys"));
            PropertyChanged(this, new PropertyChangedEventArgs("Values"));
        }
    }

    public bool Contains(KeyValuePair<TKey, TValue?> item) => _dictionary.Contains(item);

    public void CopyTo(KeyValuePair<TKey, TValue?>[] array, int arrayIndex) => _dictionary.CopyTo(array, arrayIndex);

    public bool Remove(KeyValuePair<TKey, TValue?> item)
    {
        bool remove = _dictionary.Remove(item);

        if (!remove) return false; //don’t trigger change events if there was no change.

        if (CollectionChanged != null)
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));

        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Keys"));
            PropertyChanged(this, new PropertyChangedEventArgs("Values"));
        }

        return true;
    }

    public int Count => _dictionary.Count;

    public bool IsReadOnly => _dictionary.IsReadOnly;

    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

    public void Add(TKey key, TValue? value)
    {
        _dictionary.Add(key, value);

        if (CollectionChanged != null)
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));

        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Keys"));
            PropertyChanged(this, new PropertyChangedEventArgs("Values"));
        }
    }

    public bool Remove(TKey key)
    {
        bool remove = _dictionary.Remove(key);

        if (!remove) return false;

        if (CollectionChanged != null)
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));

        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Keys"));
            PropertyChanged(this, new PropertyChangedEventArgs("Values"));
        }

        return true;
    }

    public bool TryGetValue(TKey key, out TValue? value) => _dictionary.TryGetValue(key, out value);

    public TValue? this[TKey key]
    {
        get { return _dictionary[key]; }
        set
        {
            if (key != null )
            {
                bool changed = _dictionary[key]?.Equals(value) ?? false;

                if (!changed) return; // If there are no changes then we don’t need to update the value or trigger changed events.

                _dictionary[key] = value;

                if (CollectionChanged != null)
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Keys"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Values"));
                }
            }
        }
    }

    public ICollection<TKey> Keys => _dictionary.Keys;

    public ICollection<TValue?> Values => _dictionary.Values;
}
