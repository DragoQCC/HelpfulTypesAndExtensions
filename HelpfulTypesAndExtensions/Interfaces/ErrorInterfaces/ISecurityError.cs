namespace HelpfulTypesAndExtensions;

public interface ISecurityError<T> : IError where T : struct, ISecurityError<T>;