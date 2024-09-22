using System.Runtime.CompilerServices;

namespace HelpfulTypesAndExtensions;


public static class TaskExtensions
{
    /// <summary>
    /// Converts a Task to an Empty type <br/>
    /// Useful for chaining tasks together even if they dont really return anything <br/>
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public static async Task<Empty> ToEmpty(this Task task)
    {
        await task.IgnoreContext();
        return Empty.Default;
    }
    
    /// <summary>
    /// Converts a ValueTask to an Empty type <br/>
    /// Useful for chaining tasks together even if they dont really return anything <br/>
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public static async ValueTask<Empty> ToEmpty(this ValueTask task)
    {
        await task.IgnoreContext();
        return Empty.Default;
    }
    
    /// <summary>
    /// Sets the task to ignore the current context using <see cref="Task.ConfigureAwait(bool)"/>> <br/>
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public static ConfiguredTaskAwaitable IgnoreContext(this Task task)
    {
        return task.ConfigureAwait(false);
    }
    
    /// <summary>
    /// Sets the value task to ignore the current context using <see cref="Task.ConfigureAwait(bool)"/>> <br/>
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public static ConfiguredValueTaskAwaitable IgnoreContext(this ValueTask task)
    {
        return task.ConfigureAwait(false);
    }
}