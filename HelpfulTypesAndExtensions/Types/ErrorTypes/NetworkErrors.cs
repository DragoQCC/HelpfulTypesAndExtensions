namespace HelpfulTypesAndExtensions;

/// <summary>
/// Includes
/// NetworkingError,
/// RateLimit,
/// Timeout,
/// ConnectionFailure,
/// AuthenticationFailure
/// </summary>
public static class NetworkErrors
{
    public static NetworkingError GenericNetworking(string? message = null)
    {
        var networkingError = new NetworkingError();
        return networkingError.SetMessage(message);
    }
    public static RateLimitError RateLimit(string? message = null)
    {
        var rateLimitError = new RateLimitError();
        return rateLimitError.SetMessage(message);
    }
    public static TimeoutError Timeout(string? message = null)
    {
        var timeoutError = new TimeoutError();
        return timeoutError.SetMessage(message);
    }
    public static ConnectionFailureError ConnectionFailure(string? message = null)
    {
        var connectionFailureError = new ConnectionFailureError();
        return connectionFailureError.SetMessage(message);
    }
    public static AuthenticationFailureError AuthenticationFailure(string? message = null)
    {
        var authenticationFailureError = new AuthenticationFailureError();
        return authenticationFailureError.SetMessage(message);
    }

    public record struct NetworkingError() : INetworkError<NetworkingError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Networking Error";
        
        /// <inheritdoc />
        public string? Message { get; set; } = "A networking error occurred";
        
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.NetworkingError;
        
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct RateLimitError() : INetworkError<RateLimitError>
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

    public record struct TimeoutError() : INetworkError<TimeoutError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Timeout";
        /// <inheritdoc />
        public string? Message { get; set; } = "The request timed out";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.Timeout;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct ConnectionFailureError() : INetworkError<ConnectionFailureError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Connection Failure";
        /// <inheritdoc />
        public string? Message { get; set; } = "A connection failure occurred";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.NetworkingError;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct AuthenticationFailureError() : INetworkError<AuthenticationFailureError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Authentication Failure";
        /// <inheritdoc />
        public string? Message { get; set; } = "An authentication failure occurred";
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.NetworkingError;
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
}