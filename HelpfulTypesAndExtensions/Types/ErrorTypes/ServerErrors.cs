namespace HelpfulTypesAndExtensions;

/// <summary>
/// Includes
/// GenericFailure,  
///Unexpected,  
///ServiceUnavailable,  
///CircuitBreaker,  
///DependencyFailure,  
///DataIntegrity,  
///PreconditionFailed  
/// </summary>
public static class ServerErrors
{
    public static GenericFailureError GenericFailure(string? message = null)
    {
        var genericFailureError = new GenericFailureError();
        return genericFailureError.SetMessage(message);
    }
    public static UnexpectedError Unexpected(string? message = null)
    {
        var unexpectedError = new UnexpectedError();
        return unexpectedError.SetMessage(message);
    }
    public static ServiceUnavailableError ServiceUnavailable(string? message = null)
    {
        var serviceUnavailableError = new ServiceUnavailableError();
        return serviceUnavailableError.SetMessage(message);
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
    public static DataIntegrityError DataIntegrity(string? message = null)
    {
        var dataIntegrityError = new DataIntegrityError();
        return dataIntegrityError.SetMessage(message);
    }
    public static PreconditionFailedError PreconditionFailed(string? message = null)
    {
        var preconditionFailedError = new PreconditionFailedError();
        return preconditionFailedError.SetMessage(message);
    }

    public record struct GenericFailureError() : IServerError<GenericFailureError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Generic Failure";
        
        /// <inheritdoc />
        public string? Message { get; set; } = "A generic failure occurred";
        
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

    public record struct UnexpectedError() : IServerError<UnexpectedError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Unexpected Error";


        /// <inheritdoc />
        public string? Message { get; set; } = "An unexpected error occurred";


        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;


        /// <inheritdoc />
        public string? Source { get; set; } = "";


        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;


        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.Unexpected;


        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;


        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct ServiceUnavailableError() : IServerError<ServiceUnavailableError>
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
    
    public record struct CircuitBreakerError() : IServerError<CircuitBreakerError>
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
    
    public record struct DependencyFailureError() : IServerError<DependencyFailureError>
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
    
    public record struct DataIntegrityError() : IServerError<DataIntegrityError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Data Integrity";
        
        /// <inheritdoc />
        public string? Message { get; set; } = "A data integrity error occurred";
        
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.DataIntegrity;
        
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct PreconditionFailedError() : IServerError<PreconditionFailedError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Precondition Failed";
        
        /// <inheritdoc />
        public string? Message { get; set; } = "A precondition failed error occurred";
        
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.PreconditionFailed;
        
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }

}