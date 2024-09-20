using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HelpfulTypesAndExtensions;

public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyPropertyChanged 
    where TKey : notnull
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<KeyValuePair<TKey, TValue>>? ItemAdded;
    public event EventHandler<KeyValuePair<TKey, TValue>>? ItemUpdated;
    public event EventHandler<TKey>? ItemRemoved;

    virtual protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    virtual protected void OnItemAdded(KeyValuePair<TKey, TValue> e)
    {
        ItemAdded?.Invoke(this, e);
    }
    virtual protected void OnItemUpdated(KeyValuePair<TKey, TValue> e)
    {
        ItemUpdated?.Invoke(this, e);
    }
    virtual protected void OnItemRemoved(TKey e)
    {
        ItemRemoved?.Invoke(this, e);
    }

    public new TValue this[TKey key]
    {
        get => base.TryGetValue(key, out var value) ? value : default!;
        set
        {
            base[key] = value;
            OnItemUpdated(new KeyValuePair<TKey, TValue>(key, value));
        }
    }

    public new void Add(TKey key, TValue value)
    {
        base.Add(key, value);
        OnItemAdded(new KeyValuePair<TKey, TValue>(key,value));
    }

    #if DOTNET5_0_OR_GREATER
    public new bool TryAdd(TKey key, TValue value)
    {
        var result = base.TryAdd(key, value);
        if (result)
        {
            OnItemAdded(new KeyValuePair<TKey, TValue>(key, value));
        }
        return result;
    }
    #endif
    #if NETSTANDARD
    public bool TryAdd(TKey key, TValue value)
    {
        var result = false;
        if (!ContainsKey(key))
        {
            Add(key, value);
            result = true;
        }
        if (result)
        {
            OnItemAdded(new KeyValuePair<TKey, TValue>(key, value));
        }
        return result;
    }
    #endif

    public new bool Remove(TKey key)
    {
        var result = base.Remove(key);
        if (result)
        {
            OnItemRemoved(key);
        }
        return result;
    }
}