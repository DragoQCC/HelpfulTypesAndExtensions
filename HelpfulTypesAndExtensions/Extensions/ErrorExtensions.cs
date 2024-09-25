namespace HelpfulTypesAndExtensions;

public static class ErrorExtensions
{
    public static TError SetMessage<TError>(this ref TError error, string? message) where TError : struct,IError 
        => error with {Message = message};

    public static void SetSource<TError>(this ref TError error,string source) where TError : struct,IError 
        => error.Source = source;
    
    public static void SetPriorityLevel<TError>(this ref TError error,ErrorSeverity priorityLevel) where TError : struct,IError 
        => error.PriorityLevel = priorityLevel;
    
    public static void SetInnerError<TError>(this ref TError error,IError innerError) where TError : struct,IError 
        => error.InnerError = innerError;
    
    public static void SetMetaData<TError>(this ref TError error,IDictionary<string, object> metaData) where TError : struct,IError 
        => error.MetaData = metaData;
    
    public static void AddMetaData<TError>(this ref TError error,string key, object value) where TError : struct,IError 
        => error.MetaData ??= new Dictionary<string, object> {{key, value}};
    
    public static void AddMetaData<TError>(this ref TError error,KeyValuePair<string, object> metaData) where TError : struct,IError 
        => error.MetaData ??= new Dictionary<string, object> {{metaData.Key, metaData.Value}};
    
    public static void AddMetaData<TError>(this ref TError error,IEnumerable<KeyValuePair<string, object>> metaData) where TError : struct,IError 
        => error.MetaData ??= metaData.ToDictionary(x => x.Key, x => x.Value);
    
    public static string GetName<TError>(this TError error) where TError : IError 
        => error.Name;
    
    public static string GetMessage<TError>(this TError error) where TError : IError 
        => error.Message ?? "No message provided";
    
    public static DateTime GetCreationTime<TError>(this TError error) where TError : IError 
        => error.CreationTime;
    
    public static DateTime GetCreationTimeAs<TError>(this TError error,TimeZoneInfo timeZone) where TError : IError 
        => TimeZoneInfo.ConvertTime(error.CreationTime, timeZone);
    
    public static string GetSource<TError>(this TError error) where TError : IError 
        => error.Source ?? string.Empty;
    
    public static ErrorSeverity GetPriorityLevel<TError>(this TError error) where TError : IError 
        => error.PriorityLevel;
    
    public static ErrorType GetType<TError>(this TError error) where TError : IError 
        => error.Type;
    
    public static IError? GetInnerError<TError>(this TError error) where TError : IError 
        => error.InnerError;
    
    public static IDictionary<string, object>? GetMetaData<TError>(this TError error) where TError : IError 
        => error.MetaData;
}