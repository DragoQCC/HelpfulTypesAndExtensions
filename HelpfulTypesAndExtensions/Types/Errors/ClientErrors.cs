namespace HelpfulTypesAndExtensions.Errors;

public static class ClientErrors
{
    public static ValidationFailureError ValidationFailure(string? message = null)
    {
        var error = new ValidationFailureError();
        return error.SetMessage(message);
    }
    public static BadRequestError BadRequest(string? message = null)
    {
        var badRequestError = new BadRequestError();
        return badRequestError.SetMessage(message);
    }
    public static NotFoundError NotFound(string? message = null)
    {
        var notFoundError = new NotFoundError();
        return notFoundError.SetMessage(message);
    }
    public static UnauthorizedError Unauthorized(string? message = null)
    {
        var unauthorizedError = new UnauthorizedError();
        return unauthorizedError.SetMessage(message);
    }
    public static ForbiddenError Forbidden(string? message = null)
    {
        var forbiddenError = new ForbiddenError();
        return forbiddenError.SetMessage(message);
    }
    public static MissingAuthenticationError MissingAuthentication(string? message = null)
    {
        var missingAuthenticationError = new MissingAuthenticationError();
        return missingAuthenticationError.SetMessage(message);
    }
    public static InvalidOperationError InvalidOperation(string? message = null)
    {
        var invalidOperationError = new InvalidOperationError();
        return invalidOperationError.SetMessage(message);
    }

    public record struct ValidationFailureError() : IClientError<ValidationFailureError>
    {
        /// <inheritdoc />
        public string Name { get; set; } = "Validation Failure";

        /// <inheritdoc />
        public string? Message { get; set; } = "A validation error occurred";
        
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.ValidationFailure;
        
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null; 
    }
    
    public record struct BadRequestError() : IClientError<BadRequestError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Bad Request";
        
        /// <inheritdoc />
        public string? Message { get; set; } = "The request was malformed or invalid";
        
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.BadRequest;
        
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct NotFoundError() : IClientError<NotFoundError>
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
    
    public record struct UnauthorizedError() : IClientError<UnauthorizedError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Unauthorized";
        
        /// <inheritdoc />
        public string? Message { get; set; } = "The request was unauthorized";
        
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.Unauthorized;
        
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct ForbiddenError() : IClientError<ForbiddenError>
    {
        /// <inheritdoc />
        public string Name { get; }  = "Forbidden";
        
        /// <inheritdoc />
        public string? Message { get; set; } = "The request was forbidden";
        
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.Forbidden;
        
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct MissingAuthenticationError() : IClientError<MissingAuthenticationError>
    {
        /// <inheritdoc />
        public string Name { get; }  = "Missing Authentication";
        
        /// <inheritdoc />
        public string? Message { get; set; } = "The request was missing authentication";
        
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.MissingAuthentication;
        
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
    public record struct InvalidOperationError() : IClientError<InvalidOperationError>
    {
        /// <inheritdoc />
        public string Name { get; } =  "Invalid Operation";
        
        /// <inheritdoc />
        public string? Message { get; set; } = "An invalid operation was attempted";
        
        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        
        /// <inheritdoc />
        public string? Source { get; set; } = "";
        
        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Medium;
        
        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.InvalidOperation;
        
        /// <inheritdoc />
        public IError? InnerError { get; set; } = null;
        
        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; } = null;
    }
    
}