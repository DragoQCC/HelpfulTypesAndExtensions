namespace HelpfulTypesAndExtensions;

public interface IClientError<T> : IError where T : struct, IClientError<T>;