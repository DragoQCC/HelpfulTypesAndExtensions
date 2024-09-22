namespace HelpfulTypesAndExtensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Performs an action on each item in the source collection
    /// </summary>
    /// <param name="source"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }
    
    /// <summary>
    /// Applies an action to each item in a sequence if it is not null, otherwise does nothing, does not return
    /// </summary>
    /// <param name="items"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static void ForEachIfNotNull<T>(this IEnumerable<T?> items, Action<T> action)
    {
        foreach (var item in items)
        {
            item.IfNotNullDo(action);
        }
    }
    
    /// <summary>
    /// Returns true if the source collection is null or empty
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool None<T>(this IEnumerable<T>? source) => source?.Any() ?? false;
    
    /// <summary>
    /// Converts a list of items to a comma separated string using
    /// <code>ToString()</code> on each item
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string ToCommaSeparatedString<T>(this IEnumerable<T> source) => string.Join(", ", source.Select(x => x?.ToString()));

    /// <summary>
    /// Converts a list of items to a command separated string using the provided selector
    /// </summary>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string ToCommaSeparatedString<T>(this IEnumerable<T> source, Func<T, string> selector) => string.Join(", ", source.Select(selector));
    
    /// <summary>
    /// Executes the Dispose method on each item in the collection
    /// </summary>
    /// <param name="collection"></param>
    public static void DisposeItems(this IEnumerable<IDisposable?> collection)
    {
        foreach (IDisposable? item in collection)
        {
            if (item == null)
            {
                continue;
            }
            try
            {
                item.Dispose();
            }
            catch (Exception)
            {
            }
        }
    }
    
    /// <summary>
    /// Executes the DisposeAsync method on each item in the collection
    /// </summary>
    /// <param name="collection"></param>
    public static async Task DisposeItemsAsync(this IEnumerable<IAsyncDisposable?> collection)
    {
        foreach (IAsyncDisposable? item in collection)
        {
            if (item == null)
            {
                continue;
            }
            try
            {
                await item.DisposeAsync();
            }
            catch (Exception)
            {
            }
        }
    }
    
    
    /// <summary>
    /// Implements a TryAdd method for dictionaries that will only add the item if the key does not already exist <br/>
    /// Only needed for .NET Standard 2.0, since .NET Core and later have this method built in
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    #if NETSTANDARD
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key))
        {
            return false;
        }
        dictionary.Add(key, value);
        return true;
    }
    #endif
}