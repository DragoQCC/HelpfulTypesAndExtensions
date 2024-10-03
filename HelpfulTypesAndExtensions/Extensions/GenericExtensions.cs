using System.Runtime.CompilerServices;

namespace HelpfulTypesAndExtensions;

public static class GenericExtensions
{
    /// <summary>
    /// Returns true if the reference type is null <br/>
    /// Includes Classes, Records, Interfaces, and Delegates
    /// </summary>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsNull<T>(this T? item) where T : class? => item is null;
    
    /// <summary>
    /// Returns true if the value type is null
    /// </summary>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsNull<T>(this T? item) where T : struct => item is null;
    
    /// <summary>
    /// Returns true if the reference type is not null <br/>
    /// Includes Classes, Records, Interfaces, and Delegates
    /// </summary>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsNotNull<T>(this T? item) where T : class? => item is not null;

    /// <summary>
    /// Returns true if the value type is not null
    /// </summary>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsNotNull<T>(this T? item) where T : struct => item is not null;
    
    /// <summary>
    /// Returns true if the item is the default value for the type
    /// </summary>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsDefault<T>(this T? item) => EqualityComparer<T>.Default.Equals(item!, default!);
    
    /// <summary>
    /// Returns true if the item is not the default value for the type
    /// </summary>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsNotDefault<T>(this T? item) => !EqualityComparer<T>.Default.Equals(item!, default!);


    /// <summary>
    /// Executes an function on an item if it is not null, otherwise does nothing, returns the result
    /// </summary>
    /// <param name="item"></param>
    /// <param name="action"></param>
    /// <param name="fallback"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public static TResult IfNotNullDo<T,TResult>(this T? item, Func<T?,TResult> action, TResult fallback = default!) where TResult : notnull
        => item is not null ? TryCatch.Try(action,item,onError: null) : fallback;
    

    /// <summary>
    /// Executes an function on an item if it is not null, otherwise does nothing, returns the original item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    public static T? IfNotNullDo<T>(this T? item, Action<T> action) => item is not null ? TryCatch.Try(action, item).ContinueWith(item) : item; 
    
    /// <summary>
    /// Executes an action to an item if it is not null, otherwise does nothing, does not return
    /// </summary>
    /// <param name="item"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Empty IfNotNullDo<T>(this T? item, Action action)
        => item is not null ? TryCatch.Try(action).AsEmpty() : Empty.Default;
    
    
    /// <summary>
    /// Executes an action if the item is null, otherwise does nothing, does not return
    /// </summary>
    /// <param name="item"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Empty IfNullDo<T>(this T? item, Action action)
        => item is null ? TryCatch.Try(action).AsEmpty() : Empty.Default;
    
    /// <summary>
    /// Executes an action if the item is null, otherwise does nothing, does not return
    /// </summary>
    /// <param name="item"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Empty IfNullDo<T>(this T? item, Action<T?> action)
        => item is null ? TryCatch.Try(action,item).AsEmpty() : Empty.Default;
    
    /// <summary>
    /// Executes an action if the item is null, otherwise does nothing, returns the item for chaining
    /// </summary>
    /// <param name="item"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T IfNullDo<T>(this T? item, Func<T> action)
        => item is null ? TryCatch.Try(action) : item;
    
    
    /// <summary>
    /// Executes the provided method, depending on if the supplied predicate is true or false
    /// </summary>
    /// <param name="item"></param>
    /// <param name="condition"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static TResult? DoIf<T,TResult>(this T? item, Predicate<T?> condition, Func<T?, TResult> action) where T : class? where TResult : notnull
        => condition(item) ? TryCatch.Try(action,item, onError: null) : default;
    
    
    /// <summary>
    /// Passes the item to the condition and returns the result
    /// </summary>
    /// <param name="item"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool CheckIf<T>(this T? item, Predicate<T?> condition) => condition(item);


    /// <summary>
    /// Executes the provided predicate, returning true or false
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static bool CheckIf(BooleanExpression condition) => condition.Evaluate();
    
    
    /// <summary>
    /// if the condition is true, the item is passed to the action and executed
    /// otherwise returns default
    /// </summary>
    /// <param name="checkResult"></param>
    /// <param name="item"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static TResult? ThenDo<T,TResult>(this bool checkResult, T? item, Func<T?, TResult> action) where T : class? where TResult : notnull
        => checkResult ? TryCatch.Try(action,item,onError: null) : default;
    
    /// <summary>
    /// Executes the action if the checkResult is true, otherwise returns Empty.Default and does nothing
    /// </summary>
    /// <param name="checkResult"></param>
    /// <param name="item"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Empty ThenDo<T>(this bool checkResult,T item, Action<T> action)
        => checkResult ? TryCatch.Try(action,item).ContinueWith(Empty.Default) : Empty.Default;
    
    
    /// <summary>
    /// Returns either the item or null depending on the checkResult, if the checkResult is true, the item is returned otherwise null
    /// </summary>
    /// <param name="checkResult"></param>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? ReturnOrNull<T>(this bool checkResult, T item)
        => checkResult ? item : default;
    
    
    /// <summary>
    /// Returns the item if it clears the condition, otherwise returns null
    /// </summary>
    /// <param name="item"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? ReturnIf<T>(this T? item, Predicate<T?> condition) where T : class?
        => condition(item) ? item : default;
   
    /// <summary>
    /// Executes the action if the checkResult is true, otherwise returns Empty.Default and does nothing
    /// </summary>
    /// <param name="checkResult"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static bool ThenDo(this bool checkResult, Action action)
        => checkResult ? TryCatch.Try(action).ContinueWith(checkResult) : checkResult;
    
    /// <summary>
    /// Executes the doAction if the checkResult is true, otherwise executes the elseAction
    /// </summary>
    /// <param name="checkResult"></param>
    /// <param name="doAction"></param>
    /// <param name="elseAction"></param>
    /// <returns></returns>
    public static Empty ThenDo(this bool checkResult, Action doAction, Action elseAction)
        => checkResult ? TryCatch.Try(doAction).ContinueWith(Empty.Default) : TryCatch.Try(elseAction).ContinueWith(Empty.Default);
    
    /// <summary>
    /// Can be used to chain together with ThenDo to create an if else statement <br/>
    /// will execute the passed in action if the checkResult is false
    /// </summary>
    /// <param name="checkResult"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static bool ElseDo(this bool checkResult, Action action)
        => checkResult ? checkResult : TryCatch.Try(action).ContinueWith(checkResult);
    
    
    
    /// <summary>
    /// Used to allow chaining of functions and still return whatever item is relevant in the call chain
    /// </summary>
    /// <param name="appliedItem">Can be anything allowing for robust method chaining</param>
    /// <param name="itemToReturn">The item that is needed to end or continue the chain correctly</param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    /// <returns></returns>
    public static TReturn ContinueWith<T,TReturn> (this T? appliedItem, TReturn itemToReturn) => itemToReturn;
    
    /// <summary>
    /// Used to allow chaining of functions and still return whatever item is relevant in the call chain
    /// </summary>
    /// <param name="appliedItem"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    /// <returns></returns>
    public static TReturn? ContinueWith<T,TReturn> (this T? appliedItem) => default;
    
    
    /// <summary>
    /// Can be used to help chain together logical operations such as item.IsNotNull().And(item).IsNotNull()
    /// </summary>
    /// <param name="checkResult"></param>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? And<T>(this bool checkResult, T item) => checkResult ? item : default;
    
    
    #if NETCORE 
    /// <summary>
    /// Throws an exception if the supplied item is null
    /// Will include the method name, file path, line number, and a custom message
    /// </summary>
    /// <param name="value"></param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <param name="message"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public static T ThrowIfNull<T>(this T? value, 
        [CallerMemberName] string memberName = "", 
        [CallerFilePath] string sourceFilePath = "", 
        [CallerLineNumber] int sourceLineNumber = 0, 
        [CallerArgumentExpression(nameof(value))] string? message = "") 
        => value ?? throw new NullReferenceException($"Object {message} is null, \n\t type is: {typeof(T)}, \n\t method: {memberName}, \n\t file: {sourceFilePath}, \n\t line: {sourceLineNumber}");
    #endif
    #if NETSTANDARD
    /// <summary>
    /// Throws an exception if the supplied item is null
    /// Will include the method name, file path, line number, and a custom message
    /// </summary>
    /// <param name="value"></param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <param name="message"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public static T ThrowIfNull<T>(this T? value, 
        [CallerMemberName] string memberName = "", 
        [CallerFilePath] string sourceFilePath = "", 
        [CallerLineNumber] int sourceLineNumber = 0, 
        string? message = "") 
        => value ?? throw new NullReferenceException($"Object {message} is null, \n\t type is: {typeof(T)}, \n\t method: {memberName}, \n\t file: {sourceFilePath}, \n\t line: {sourceLineNumber}");
    #endif
}