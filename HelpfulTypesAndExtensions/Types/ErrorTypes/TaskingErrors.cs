namespace HelpfulTypesAndExtensions;

public static class TaskingErrors
{
    public static TaskCancelledError TaskCancelled(string? message = null)
    {
        var taskCancelledError = new TaskCancelledError();
        return taskCancelledError.SetMessage(message);
    }
    
    public record struct TaskCancelledError() : ITaskingError<TaskCancelledError>
    {
        /// <inheritdoc />
        public string Name { get; } = "Task Cancelled";

        /// <inheritdoc />
        public string? Message { get; set; } = "The task was cancelled";

        /// <inheritdoc />
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        /// <inheritdoc />
        public string? Source { get; set; }

        /// <inheritdoc />
        public ErrorSeverity PriorityLevel { get; set; } = ErrorSeverity.Critical;

        /// <inheritdoc />
        public ErrorType Type { get; set; } = ErrorType.Timeout;

        /// <inheritdoc />
        public IError? InnerError { get; set; }

        /// <inheritdoc />
        public IDictionary<string, object>? MetaData { get; set; }
    }
    
    
}