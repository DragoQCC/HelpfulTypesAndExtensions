using System.Collections.Concurrent;
using System.Reflection;

namespace HelpfulTypesAndExtensions;

public static class TypeExtensions
{
    internal static readonly ConcurrentDictionary<MethodBase, IReadOnlyList<Type>> ParameterMap = new ConcurrentDictionary<MethodBase, IReadOnlyList<Type>>();
    
    
    #if NET
    /// <summary>
    /// Returns the first interface that the type implements from the list of interfaces
    /// </summary>
    /// <param name="type"></param>
    /// <param name="interfaces"></param>
    /// <returns></returns>
    public static Type? ImplementsInterfaceFrom(this Type type, List<Type> interfaces)
    {
        foreach (var interfaceType in interfaces)
        {
            if(interfaceType.Name == type.Name)
            {
                //skip this so we don't try to assign the type we are looking for to itself
                continue;
            }
            if (type.IsAssignableTo(interfaceType))
            {
                return interfaceType;
            }
        }
        return null;
    }
    #endif
    
    #if NETSTANDARD
    /// <summary>
    /// Returns the first interface that the type implements from the list of interfaces
    /// </summary>
    /// <param name="type"></param>
    /// <param name="interfaces"></param>
    /// <returns></returns>
    public static Type? ImplementsInterfaceFrom(this Type type, List<Type> interfaces)
    {
        foreach (var interfaceType in interfaces)
        {
            if(interfaceType.Name == type.Name)
            {
                //skip this so we don't try to assign the type we are looking for to itself
                continue;
            }
            if (interfaceType.IsAssignableFrom(type))
            {
                return interfaceType;
            }
        }
        return null;
    }
    #endif
    
}

public static class MethodBaseExtensions
{
    /// <summary>
    /// Gets the types for a method's parameters.
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public static IReadOnlyList<Type> GetParameterTypes(this MethodBase method) => 
        TypeExtensions.ParameterMap.GetOrAdd(method, c => c.GetParameters().Select(p => p.ParameterType).ToArray());
    
}