namespace HelpfulTypesAndExtensions;

public static class EmptyTypeExtensions
{
    public static Empty AsEmpty<T>(this T ignoredItem) => Empty.Return();

}