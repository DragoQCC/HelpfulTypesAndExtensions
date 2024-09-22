namespace HelpfulTypesAndExtensions;

public static class TGuidExtensions
{
    /// <summary>
    /// Converts a collection of Guids to a collection of TGuids
    /// </summary>
    /// <param name="guids"></param>
    /// <returns></returns>
    public static IEnumerable<TGuid> ToTGuids(this IEnumerable<Guid> guids) => guids.Select(x => new TGuid(x));
    
    /// <summary>
    /// Organizes a collection of TGuids by their creation time
    /// </summary>
    /// <param name="guids">The Collection of TGuids to sort</param>
    /// <returns>a collection of the underlying Guids ordered by creation time</returns>
    public static IEnumerable<TGuid> SortByCreationTime(this IEnumerable<TGuid> guids) => guids.OrderBy(x => x.CreationTime);
    
}