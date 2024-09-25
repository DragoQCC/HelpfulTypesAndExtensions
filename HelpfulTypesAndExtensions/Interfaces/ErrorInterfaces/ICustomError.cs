namespace HelpfulTypesAndExtensions;

public interface ICustomError<T> : IError where T : struct, ICustomError<T>;