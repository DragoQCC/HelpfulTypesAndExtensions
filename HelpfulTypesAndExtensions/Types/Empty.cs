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
    
    public static Empty Ignore<T>(T itemToIgnore) => Default; 
}