using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

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
    
    /// <summary>
    /// Allows for a safe way to fire and forget a task in situations where it would otherwise not be possible such as a constructor <br/>
    /// </summary>
    /// <param name="task"></param>
    /// <param name="onError"></param>
    /// <param name="continueOnCapturedContext"></param>
    public static async void FireAndForget(this Task task, Action<Exception>? onError = null, bool continueOnCapturedContext = false)
    {
        try
        {
            await task.ConfigureAwait(continueOnCapturedContext);
        }
        catch (Exception e) when (onError is not null)
        {
            onError?.Invoke(e);
        }
        catch (Exception e)
        {
            //Should help maintain the stack trace
            ExceptionDispatchInfo.Capture(e).Throw();
        }
    }
    
    public static async void FireAndForget(this ValueTask task, Action<Exception>? onError = null, bool continueOnCapturedContext = false)
    {
        try
        {
            await task.ConfigureAwait(continueOnCapturedContext);
        }
        catch (Exception e) when (onError is not null)
        {
            onError?.Invoke(e);
        }
        catch (Exception e)
        {
            //Should help maintain the stack trace
            ExceptionDispatchInfo.Capture(e).Throw();
        }
    }
}