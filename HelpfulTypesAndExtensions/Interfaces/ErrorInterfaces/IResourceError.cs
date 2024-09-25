namespace HelpfulTypesAndExtensions;

public interface IResourceError<T> : IError where T : struct, IResourceError<T>;