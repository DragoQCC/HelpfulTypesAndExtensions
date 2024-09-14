using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace HelpfulTypesAndExtensions;

public class ObservableHashSet<T> : ISet<T>, IReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged, INotifyPropertyChanging
{
    private HashSet<T> _set;
    
    public ObservableHashSet() : this(EqualityComparer<T>.Default)
    {}

    public ObservableHashSet(IEqualityComparer<T> comparer)
    {
        _set = new HashSet<T>(comparer);
    }

    public ObservableHashSet(IEnumerable<T> collection) : this(collection, EqualityComparer<T>.Default)
    { }
    
    public ObservableHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
    {
        _set = new HashSet<T>(collection, comparer);
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    void ICollection<T>.Add(T item) => Add(item);
    
    public virtual void Clear()
    {
        if (_set.Count == 0)
        {
            return;
        }
        OnCountPropertyChanging();
        var removed = this.ToList();
        _set.Clear();
        OnCollectionChanged(ObservableHashSetSingletons.NoItems, removed);
        OnCountPropertyChanged();
    }

    public virtual bool Contains(T item)  => _set.Contains(item);
    
    public virtual void CopyTo(T[] array, int arrayIndex)  => _set.CopyTo(array, arrayIndex);


    public virtual bool Remove(T item)
    {
        if (!_set.Contains(item))
        {
            return false;
        }
        OnCountPropertyChanging();
        _set.Remove(item);
        OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
        OnCountPropertyChanged();
        return true;
    }
    
    public virtual int Count  => _set.Count;
    public virtual bool IsReadOnly => ((ICollection<T>)_set).IsReadOnly;
    
    public virtual HashSet<T>.Enumerator GetEnumerator() => _set.GetEnumerator();
    IEnumerator<T> IEnumerable<T>.GetEnumerator()  => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    public virtual bool Add(T item)
    {
        if (_set.Contains(item))
        {
            return false;
        }
        OnCountPropertyChanging();
        _set.Add(item);
        OnCollectionChanged(NotifyCollectionChangedAction.Add, item);
        OnCountPropertyChanged();
        return true;
    }
    
    public virtual void UnionWith(IEnumerable<T> other)
    {
        var copy = new HashSet<T>(_set, _set.Comparer);
        copy.UnionWith(other);
        if (copy.Count == _set.Count)
        {
            return;
        }
        var added = copy.Where(i => !_set.Contains(i)).ToList();

        OnCountPropertyChanging();
        _set = copy;
        OnCollectionChanged(added, ObservableHashSetSingletons.NoItems);
        OnCountPropertyChanged();
    }
    
    public virtual void IntersectWith(IEnumerable<T> other)
    {
        var copy = new HashSet<T>(_set, _set.Comparer);

        copy.IntersectWith(other);

        if (copy.Count == _set.Count)
        {
            return;
        }

        var removed = _set.Where(i => !copy.Contains(i)).ToList();

        OnCountPropertyChanging();
        _set = copy;
        OnCollectionChanged(ObservableHashSetSingletons.NoItems, removed);
        OnCountPropertyChanged();
    }
    
    public virtual void ExceptWith(IEnumerable<T> other)
    {
        var copy = new HashSet<T>(_set, _set.Comparer);

        copy.ExceptWith(other);
        if (copy.Count == _set.Count)
        {
            return;
        }

        var removed = _set.Where(i => !copy.Contains(i)).ToList();
        OnCountPropertyChanging();
        _set = copy;
        OnCollectionChanged(ObservableHashSetSingletons.NoItems, removed);
        OnCountPropertyChanged();
    }
    
    public virtual void SymmetricExceptWith(IEnumerable<T> other)
    {
        var copy = new HashSet<T>(_set, _set.Comparer);

        copy.SymmetricExceptWith(other);

        var removed = _set.Where(i => !copy.Contains(i)).ToList();
        var added = copy.Where(i => !_set.Contains(i)).ToList();

        if (removed.Count == 0
            && added.Count == 0)
        {
            return;
        }

        OnCountPropertyChanging();
        _set = copy;
        OnCollectionChanged(added, removed);
        OnCountPropertyChanged();
    }
    
    public virtual bool IsSubsetOf(IEnumerable<T> other) => _set.IsSubsetOf(other);

    public virtual bool IsProperSubsetOf(IEnumerable<T> other) => _set.IsProperSubsetOf(other);

    public virtual bool IsSupersetOf(IEnumerable<T> other) => _set.IsSupersetOf(other);

    public virtual bool IsProperSupersetOf(IEnumerable<T> other) => _set.IsProperSupersetOf(other);

    public virtual bool Overlaps(IEnumerable<T> other) => _set.Overlaps(other);
    
    public virtual bool SetEquals(IEnumerable<T> other) => _set.SetEquals(other);

    public virtual void CopyTo(T[] array) => _set.CopyTo(array);
    
    public virtual void CopyTo(T[] array, int arrayIndex, int count) => _set.CopyTo(array, arrayIndex, count);
   
    public virtual int RemoveWhere(Predicate<T> match)
    {
        var copy = new HashSet<T>(_set, _set.Comparer);
        var removedCount = copy.RemoveWhere(match);
        if (removedCount == 0)
        {
            return 0;
        }
        var removed = _set.Where(i => !copy.Contains(i)).ToList();
        OnCountPropertyChanging();
        _set = copy;
        OnCollectionChanged(ObservableHashSetSingletons.NoItems, removed);
        OnCountPropertyChanged();
        return removedCount;
    }

   
    public virtual IEqualityComparer<T> Comparer => _set.Comparer;

   
    public virtual void TrimExcess() => _set.TrimExcess();

   
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

   
    protected virtual void OnPropertyChanging(PropertyChangingEventArgs e) => PropertyChanging?.Invoke(this, e);

    private void OnCountPropertyChanged() => OnPropertyChanged(ObservableHashSetSingletons.CountPropertyChanged);

    private void OnCountPropertyChanging() => OnPropertyChanging(ObservableHashSetSingletons.CountPropertyChanging);

    private void OnCollectionChanged(NotifyCollectionChangedAction action, object? item) => OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item));

    private void OnCollectionChanged(IList newItems, IList oldItems) => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItems, oldItems));
    
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(this, e);
}

internal static class ObservableHashSetSingletons
{
    public static readonly PropertyChangedEventArgs CountPropertyChanged = new("Count");
    public static readonly PropertyChangingEventArgs CountPropertyChanging = new("Count");
    public static readonly object[] NoItems = [];
}