namespace HelpfulTypesAndExtensions;

/// <summary>
/// Includes,
/// Custom,
/// Failure
/// </summary>
public static class CustomErrors
{
    public static CustomError Custom(string? message = null)
    {
        var customError = new CustomError();
        return customError.SetMessage(message);
    }
    public static FailureError Failure(string? message = null)
    {
        var failureError = new FailureError();
        return failureError.SetMessage(message);
    }
    public static GenericError Generic(string? message = null)
    {
        var genericError = new GenericError();
        return genericError.SetMessage(message);
    }
    public static ExceptionWrapperError Exception(Exception exception, string? message = null)
    {
        return new(exception, message);
    }

    public record struct CustomError() : ICustomError<CustomError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Custom";
        /// <inheritdoc />
        public string? Message { get; set; } = "A custom error occurred";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.Custom;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct FailureError() : ICustomError<FailureError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Failure";
        /// <inheritdoc />
        public string? Message { get; set; } = "A failure occurred";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.GenericFailure;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct GenericError() : ICustomError<GenericError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Generic Error";
        /// <inheritdoc />
        public string? Message { get; set; } = "A generic error occurred";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.GenericFailure;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct ExceptionWrapperError(Exception exception, string? message = null) : ICustomError<ExceptionWrapperError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Exception Wrapper";
        /// <inheritdoc />
        public string? Message { get; set; } = message ?? exception.Message;
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = exception.Source ?? "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.High;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.Custom;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }

}