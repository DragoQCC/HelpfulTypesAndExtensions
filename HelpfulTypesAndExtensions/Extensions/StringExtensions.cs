﻿namespace HelpfulTypesAndExtensions;

public static class StringExtensions
{
    /// <summary>
    /// Extension method that calls <see cref="String.IsNullOrWhiteSpace"/>>
    /// </summary>
    /// <param name="source">string to check</param>
    /// <returns>true if the string is null, empty (""), or whitespace (" "). Returns false otherwise.</returns>
    public static bool IsEmpty(this string? source) => string.IsNullOrWhiteSpace(source);
    
    /// <summary>
    /// Extension method that calls <see cref="String.IsNullOrWhiteSpace"/>>
    /// </summary>
    /// <param name="source">string to check</param>
    /// <returns>true if the string is not null, empty, or whitespace</returns>
    public static bool HasValue(this string? source) => !string.IsNullOrWhiteSpace(source);
    
    /// <summary>
    /// Compares two strings for equality, ignoring case
    /// Returns true if the source string is not null and it equals the target string, ignoring case
    /// returns false otherwise
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool EqualsCaseInsensitive(this string? source, string target) => source.IsNotNull() && source!.Equals(target, StringComparison.OrdinalIgnoreCase);
    
    ///<summary>
    /// Takes an object and prints its properties and their current value in a format of {Name}: {Value}, {Name2}: {Value2}, etc.
    /// </summary>
    public static string ToRecordLikeString<T>(this T source) where T: notnull 
        => source.GetType().GetProperties().Select(x => $"{x.Name}: {x.GetValue(source)}").ToCommaSeparatedString();
}