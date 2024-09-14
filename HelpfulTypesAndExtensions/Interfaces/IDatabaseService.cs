using System.Linq.Expressions;

namespace HelpfulTypesAndExtensions.Interfaces;

/// <summary>
/// Interface for abstracting database operations <br/>
/// Does not include any specific database implementation details <br/>
/// </summary>
public interface IDatabaseService
{
    /// <summary>
    /// Creates a table for the given type <br/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<bool> CreateTableAsync<T>() where T : new();
    
    /// <summary>
    /// Inserts an item into the database <br/>
    /// </summary>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<bool> InsertAsync<T>(T item) where T : new();
    
    Task<bool> CheckTableExistsAsync<T>() where T : new();
    
    Task<bool> InsertAsync(object item, Type itemType);
    
    Task<bool> InsertItemsAsync<T>(IEnumerable<T> items) where T : new();
    
    Task<bool> UpdateAsync<T>(T updatedItem) where T : new();
    
    Task<bool> UpdateItemsAsync<T>(List<T> updatedItems) where T : new();
    
    Task<bool> DeleteAsync<T>(T item) where T : new();
    
    Task<bool> DeleteByIdAsync<T>(string id) where T : new();
    
    Task<bool> DeleteByIdAsync(object itemToDelete);
    
    Task<T?> GetItemWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class, new();
    
    Task<T?> GetItemImplementingTypeWhereAsync<T>(Expression<Func<T,bool>> predicate) where T : class;
    
    Task<List<T>> GetItemsWhereAsync<T>(Expression<Func<T,bool>> predicate) where T : class, new();
    
    Task<List<T>> GetItemsImplementingTypeWhereAsync<T>(Expression<Func<T,bool>> predicate) where T : class;
    
    Task<T?> GetItemByIdAsync<T>(string id) where T : class, new();
    
    Task<List<T>> GetItemsOfTypeAsync<T>() where T : class, new();
    
    Task<TResult?> DoWithConnectionAsync<TConnection,TResult>(Func<TConnection, TResult> action);
}