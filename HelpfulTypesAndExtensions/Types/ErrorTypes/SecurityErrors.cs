namespace HelpfulTypesAndExtensions;

/// <summary>
/// Includes
/// Unauthorized,
/// MissingAuthentication,
/// Forbidden
/// </summary>
public static class SecurityErrors 
{
    public static UnauthorizedError Unauthorized(string? message = null)
    {
        var unauthorizedError = new UnauthorizedError();
        return unauthorizedError.SetMessage(message);
    }
    public static MissingAuthenticationError MissingAuthentication(string? message = null)
    {
        var missingAuthenticationError = new MissingAuthenticationError();
        return missingAuthenticationError.SetMessage(message);
    }
    public static ForbiddenError Forbidden(string? message = null)
    {
        var forbiddenError = new ForbiddenError();
        return forbiddenError.SetMessage(message);
    }

    public record struct UnauthorizedError() : ISecurityError<UnauthorizedError>
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
    
    public record struct MissingAuthenticationError() : ISecurityError<MissingAuthenticationError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Missing Authentication";
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

    public record struct ForbiddenError() : ISecurityError<ForbiddenError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Forbidden";
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
}