namespace HelpfulTypesAndExtensions;

public interface INetworkError<T> : IError where T : struct, INetworkError<T>;