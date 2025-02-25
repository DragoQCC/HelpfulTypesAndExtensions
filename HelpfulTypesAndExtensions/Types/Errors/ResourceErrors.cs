namespace HelpfulTypesAndExtensions.Errors;

/// <summary>
/// Includes,
/// NotFound
/// ServiceUnavailable
/// RateLimit
/// CircuitBreaker
/// DependencyFailure
/// </summary>
public static class ResourceErrors 
{
    public static NotFoundError NotFound(string? message = null)
    {
        var notFoundError = new NotFoundError();
        return notFoundError.SetMessage(message);
    }
    public static ServiceUnavailableError ServiceUnavailable(string? message = null)
    {
        var serviceUnavailableError = new ServiceUnavailableError();
        return serviceUnavailableError.SetMessage(message);
    }
    public static RateLimitError RateLimit(string? message = null)
    {
        var rateLimitError = new RateLimitError();
        return rateLimitError.SetMessage(message);
    }
    public static CircuitBreakerError CircuitBreaker(string? message = null)
    {
        var circuitBreakerError = new CircuitBreakerError();
        return circuitBreakerError.SetMessage(message);
    }
    public static DependencyFailureError DependencyFailure(string? message = null)
    {
        var dependencyFailureError = new DependencyFailureError();
        return dependencyFailureError.SetMessage(message);
    }

    public record struct NotFoundError() : IResourceError<NotFoundError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Not Found";
        /// <inheritdoc />
        public string? Message { get; set; } = "The requested resource was not found";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.NotFound;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct ServiceUnavailableError() : IResourceError<ServiceUnavailableError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Service Unavailable";
        /// <inheritdoc />
        public string? Message { get; set; } = "The service is currently unavailable";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.ServiceUnavailable;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }

    public record struct RateLimitError() : IResourceError<RateLimitError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Rate Limit";
        /// <inheritdoc />
        public string? Message { get; set; } = "The rate limit has been exceeded";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.RateLimit;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }

    public record struct CircuitBreakerError() : IResourceError<CircuitBreakerError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Circuit Breaker";
        /// <inheritdoc />
        public string? Message { get; set; } = "The circuit breaker has been tripped";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.CircuitBreaker;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct DependencyFailureError() : IResourceError<DependencyFailureError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Dependency Failure";
        /// <inheritdoc />
        public string? Message { get; set; } = "A dependency failure occurred";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.DependencyFailure;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
}