using System.Runtime.InteropServices;

namespace HelpfulTypesAndExtensions;

/// <summary>
/// A type that represents an empty value <br/>
/// Can be used in place of null or void <br/>
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential, Size = 1)]
public readonly record struct Empty
{
    public static Empty Default => new();
    
    /// <summary>
    /// Returns the static instance of Empty
    /// </summary>
    /// <returns></returns>
    public static Empty Return() => Default;
    
    /// <summary>
    /// Allows for returning a different value when dealing with a Empty type
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Return<T>(T value) => value;
    
    /// <summary>
    /// Allows for returning a different value when dealing with a Empty type
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Return<T>(Func<T> value) => value();
    
    /// <summary>
    /// Modify the Method to return Empty <br/>
    /// Useful for chaining methods together when you don't need the result of the previous method <br/>
    /// </summary>
    /// <param name="itemToIgnore"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Empty Ignore<T>(T itemToIgnore) => Default; 
    
    /// <summary>
    /// Converts a void method to a Func that returns Empty <br/>
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public static Func<Empty> FromVoidMethod(Action method) => () =>
    {
        method();
        return Default;
    };
    
    /// <summary>
    /// Converts a void method to a Func that returns Empty <br/>
    /// </summary>
    /// <param name="method"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Func<T, Empty> FromVoidMethod<T>(Action<T> method) => (T item) =>
    {
        method(item);
        return Default;
    };

    /// <summary>
    /// Converts a Task to an Empty type <br/>
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public static async Task<Empty> FromTask(Func<Task> task)
    {
        await task();
        return Default;
    }
}