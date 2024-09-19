namespace HelpfulTypesAndExtensions;

public static class TryCatch
{
    /// <summary>
    /// Executes the provided method and catches any exceptions that occur <br/>
    /// If a onError method is provided, it will be called with the exception as a parameter <br/>
    /// Otherwise, this method throws when it fails <br/>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="onError"></param>
    public static bool Try(Action action, Action<Exception>? onError = null)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            if (onError is null)
            {
                throw;
            }
            onError(e);
            return false;
        }
        return true;
    }
    
    /// <summary>
    /// Executes the provided method and catches any exceptions that occur <br/>
    /// If a onError method is provided, it will be called with the exception as a parameter <br/>
    /// Otherwise, this method throws when it fails <br/>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="arg1"></param>
    /// <param name="onError"></param>
    public static bool Try<T>(Action<T> action, T arg1, Action<Exception>? onError = null)
    {
        try
        {
            action(arg1);
        }
        catch (Exception e)
        {
            if (onError is null)
            {
                throw;
            }
            onError(e);
            return false;
        }
        return true;
    }
    
    /// <summary>
    /// Tries to execute the provided method and catches any exceptions that occur <br/>
    /// If a onError method is provided, it will be called with the exception as a parameter <br/>
    /// Otherwise, this method throws when it fails <br/>
    /// Returns the result of the method if it succeeds, otherwise returns the default value of the type <br/>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="onError"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Try<T>(Func<T> action, Action<Exception>? onError = null)
    {
        try
        {
            return action();
        }
        catch (Exception e)
        {
            if (onError is null)
            {
                throw;
            }
            onError(e);
            return default!;
        }
    }
    
    /// <summary>
    /// Tries to execute the provided method and catches any exceptions that occur <br/>
    /// If a onError method is provided, it will be called with the exception as a parameter <br/>
    /// Otherwise, this method throws when it fails <br/>
    /// Returns the result of the method if it succeeds, otherwise returns the default value of the type <br/>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="arg1"></param>
    /// <param name="onError"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static TResult Try<T,TResult>(Func<T?,TResult> action, T? arg1, Action<Exception>? onError = null) where TResult : notnull
    {
        try
        {
            return action(arg1);
        }
        catch (Exception e)
        {
            if (onError is null)
            {
                throw;
            }
            onError(e);
            return default!;
        }
    }
    
    /// <summary>
    /// Executes the provided method and catches any exceptions that occur <br/>
    /// If a onError method is provided, it will be called with the exception as a parameter <br/>
    /// Otherwise, this method throws when it fails <br/>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="onError"></param>
    public static async Task<bool> Try(Func<Task> action, Action<Exception>? onError = null)
    {
        try
        {
            await action();
        }
        catch (Exception e)
        {
            if (onError is null)
            {
                throw;
            }
            onError(e);
            return false;
        }
        return true;
    }
    
    /// <summary>
    /// Tries to execute the provided task and catches any exceptions that occur <br/>
    /// If a onError method is provided, it will be called with the exception as a parameter <br/>
    /// Otherwise, this method throws when it fails <br/>
    /// Returns the result of the method if it succeeds, otherwise returns the default value of the type <br/>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="onError"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<T> Try<T>(Func<Task<T>> action, Action<Exception>? onError = null)
    {
        try
        {
            return await action();
        }
        catch (Exception e)
        {
            if (onError is null)
            {
                throw;
            }
            onError(e);
            return default!;
        }
    }
    

    /// <summary>
    /// Executes the provided method to handle the exception <br/>
    /// </summary>
    /// <param name="e"></param>
    /// <param name="onError"></param>
    public static void Catch(this Exception e, Action<Exception> onError)
    {
        onError(e);
    }

}