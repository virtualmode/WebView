using System.Collections;

namespace WebView;

// from https://github.com/mattmc3/dotmore/tree/master/dotmore/Collections/Generic
public class DictionaryEnumerator<TKey, TValue> : IDictionaryEnumerator, IDisposable where TKey : notnull
{
    readonly IEnumerator<KeyValuePair<TKey, TValue?>> _impl;
    public void Dispose() { _impl.Dispose(); }
    public DictionaryEnumerator(IDictionary<TKey, TValue?> value)
    {
        _impl = value.GetEnumerator();
    }
    public void Reset() { _impl.Reset(); }
    public bool MoveNext() { return _impl.MoveNext(); }
    public DictionaryEntry Entry
    {
        get
        {
            KeyValuePair<TKey, TValue?> pair = _impl.Current;
            return new DictionaryEntry(pair.Key, pair.Value);
        }
    }
    public object Key { get { return _impl.Current.Key; } }
    public object? Value { get { return _impl.Current.Value; } }
    public object Current { get { return Entry; } }
}
