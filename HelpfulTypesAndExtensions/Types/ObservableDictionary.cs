using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HelpfulTypesAndExtensions;

public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyPropertyChanged, INotifyPropertyChanging, INotifyCollectionChanged 
    where TKey : notnull
{
    /// <inheritdoc />
    public event PropertyChangingEventHandler? PropertyChanging;
    /// <inheritdoc />
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;
    /// <summary>
    /// Occurs when an item is added to the dictionary
    /// </summary>
    public event EventHandler<KeyValuePair<TKey, TValue>>? ItemAdded;
    /// <summary>
    /// Occurs when an item is updated in the dictionary
    /// </summary>
    public event EventHandler<KeyValuePair<TKey, TValue>>? ItemUpdated;
    /// <summary>
    /// Occurs when an item is removed from the dictionary
    /// </summary>
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

    /// <inheritdoc cref="Dictionary{TKey,TValue}.Add" />
    public new void Add(TKey key, TValue value)
    {
        base.Add(key, value);
        OnItemAdded(new KeyValuePair<TKey, TValue>(key,value));
    }

    
    /// <inheritdoc cref="Dictionary{TKey,TValue}.TryAdd" />
    /// <br/> Fires the ItemAdded event if the item was added successfully
    #if NET
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
    
    
    /// <summary>
    /// An Implementation of TryAdd for .NET Standard <br/>
    /// Returns true if the key/value pair was added successfully, false if the key already exists or the value already exists for the key <br/>
    /// Fires the ItemAdded event if the item was added successfully
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    #if NETSTANDARD
    public bool TryAdd(TKey key, TValue value)
    {
        var result = false;
        if (!ContainsKey(key))
        {
            if (!ContainsValue(value))
            {
                Add(key, value);
                result = true;
            }
        }
        if (result)
        {
            OnItemAdded(new KeyValuePair<TKey, TValue>(key, value));
        }
        return result;
    }
    #endif

    /// <inheritdoc cref="Dictionary{TKey,TValue}.Remove(TKey)"/>
    /// <br/> Fires the ItemRemoved event if the item was removed successfully
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