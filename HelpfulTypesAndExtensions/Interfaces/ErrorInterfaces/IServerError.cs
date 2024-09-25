namespace HelpfulTypesAndExtensions;

public interface IServerError<T> : IError where T : struct, IServerError<T>;