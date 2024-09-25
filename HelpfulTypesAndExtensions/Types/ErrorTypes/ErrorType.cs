namespace HelpfulTypesAndExtensions;

public partial record struct ErrorType : IEnumeration<ErrorType>
{
    public static Enumeration<ErrorType> Types { get; internal set; } = new Enumeration<ErrorType>();
    
    /// <inheritdoc />
    public int Value { get; }


    /// <inheritdoc />
    public string DisplayName { get; }
    
    public ErrorType(int value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }
    
    public void Switch(ErrorType targetType, Action action)
    {
        if (this == targetType)
        {
            action();
        }
    }
    
    public void Switch(Action? defaultAction = null, params ValueTuple<ErrorType,Action>[] targetType)
    {
        foreach (var (type, action) in targetType)
        {
            if (this == type)
            {
                action();
            }
            else
            {
                defaultAction?.Invoke();
            }
        }
    }
    
    public static ErrorType Custom => new ErrorType(0, "Custom");
    public static ErrorType Unexpected => new ErrorType(1, "Unexpected");
    public static ErrorType GenericFailure => new ErrorType(2, "Generic Failure");
    public static ErrorType ValidationFailure => new ErrorType(3, "Validation Failure");
    public static ErrorType NotFound => new ErrorType(4, "Not Found");
    public static ErrorType Unauthorized => new ErrorType(5, "Unauthorized");
    public static ErrorType MissingAuthentication => new ErrorType(6, "Missing Authentication");
    public static ErrorType Forbidden => new ErrorType(7, "Forbidden");
    public static ErrorType Timeout => new ErrorType(8, "Timeout");
    public static ErrorType RateLimit => new ErrorType(9, "Rate Limit");
    public static ErrorType ServiceUnavailable => new ErrorType(10, "Service Unavailable");
    public static ErrorType BadRequest => new ErrorType(11, "Bad Request");
    public static ErrorType NetworkingError => new ErrorType(12, "Networking Error");
    public static ErrorType InvalidOperation => new ErrorType(13, "Invalid Operation");
    public static ErrorType DependencyFailure => new ErrorType(14, "Dependency Failure");
    public static ErrorType DataIntegrity => new ErrorType(15, "Data Integrity");
    public static ErrorType PreconditionFailed => new ErrorType(16, "Precondition Failed");
    public static ErrorType CircuitBreaker => new ErrorType(17, "Circuit Breaker");
}