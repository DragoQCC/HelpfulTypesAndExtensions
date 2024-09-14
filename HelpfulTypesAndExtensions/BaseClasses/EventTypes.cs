using HelpfulTypesAndExtensions.Interfaces;

namespace HelpfulTypesAndExtensions.BaseClasses;

public class TaskingEvent : IEvent<TaskingEvent>
{
    /// <inheritdoc />
    public EventMetaData EventMetaData { get; init; } = new();
    /// <inheritdoc />
    public List<Subscription<TaskingEvent>> Subscribers { get; init; } = new();


    /// <inheritdoc />
    public IEventArgs<TaskingEvent>? EventArgs { get; set; }
    
    internal Func<Task> WrappedTask { get; set; }
    private Task _task;
    public TaskStatus Status { get; internal set; }
    
    public TaskingEvent(Func<Task> task)
    {
        WrappedTask = task;
    }
    
    public async Task ExecuteTask()
    {
        _task = (Task)WrappedTask.Target;
        Status = _task.Status;
        EventMetaData.LastEventTime = DateTime.Now;
        await this.NotifySubscribers();
        try
        {
            await _task;
            Status = _task.Status;
        }
        catch (Exception e)
        {
            Status = _task.Status;
            throw;
        }
        finally
        {
            EventMetaData.LastEventTime = DateTime.Now;
            await this.NotifySubscribers();
        }
    }
    
}

public class GenericEvent : IEvent<GenericEvent>
{
    /// <inheritdoc />
    public EventMetaData EventMetaData { get; init; } = new();
    /// <inheritdoc />
    public List<Subscription<GenericEvent>> Subscribers { get; init; } = new();
    
    /// <inheritdoc />
    public IEventArgs<GenericEvent>? EventArgs { get; set; }


    public async Task RaiseEvent(IEventArgs<GenericEvent>? args = null)
    {
        EventMetaData.LastEventTime = DateTime.Now;
        try
        {
            Console.WriteLine($"Raising Event, Subscriber Count: {Subscribers.Count}");
            await this.NotifySubscribers(args);
        }
        catch (Exception e)
        {
            Console.WriteLine("Caught an exception inside raiseEvent call: " +e);
            throw;
        }
    }
    
}