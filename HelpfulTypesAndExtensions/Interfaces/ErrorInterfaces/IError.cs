using HelpfulTypesAndExtensions.Errors;

namespace HelpfulTypesAndExtensions;

public interface IError
{
    /// <summary>
    /// The name of the error
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// The message that describes the error
    /// </summary>
    public string? Message { get; set; }
    
    /// <summary>
    /// The DateTime that the error was created
    /// </summary>
    public DateTime CreationTime { get; set; }
    
    /// <summary>
    /// The source of the error, often used to identify the class or method that caused the error
    /// </summary>
    public string? Source { get; set; }
    
    /// <summary>
    /// The severity of the error <br/>
    /// Options: Unknown, Low, Medium, High, Critical
    /// </summary>
    public ErrorSeverity PriorityLevel { get; set; }
    
    /// <summary>
    /// The type of error that occurred <br/>
    /// Example: ValidationFailure, Timeout, NotFound, NetworkingError, Custom, etc. <br/>
    /// Unique error types can be defined by creating a new record that inherits from ErrorTypes
    /// </summary>
    public ErrorType Type { get; set; }
    
    /// <summary>
    /// Optional inner error that caused this error, often used for chaining errors 
    /// </summary>
    public IError? InnerError { get; set; }
    
    /// <summary>
    /// Arbitrary metadata that can be attached to the error
    /// </summary>
    public IDictionary<string, object>? MetaData { get; set; }
}








