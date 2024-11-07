namespace HelpfulTypesAndExtensions;

public interface ITaskingError<T> : IError where T : struct, ITaskingError<T>;