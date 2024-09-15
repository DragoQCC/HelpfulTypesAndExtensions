using HelpfulTypesAndExtensions.Interfaces;

namespace HelpfulTypesAndExtensions.BaseClasses;

/*public class TaskingEvent : IEvent<TaskingEvent>
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
    
}*/

public abstract record GenericEvent<TEvent> : IEvent<TEvent>
where TEvent : IEvent<TEvent>
{
    /// <inheritdoc />
    public EventMetaData EventMetaData { get; init; } = new();

    /// <inheritdoc />
    public List<Subscription<TEvent>> Subscribers { get; init; } = new();
}

public abstract record GenericEvent<TEvent,TEventArgs> : IEvent<TEvent,TEventArgs>
where TEvent : IEvent<TEvent,TEventArgs>
where TEventArgs : IEventArgs<TEvent>
{
    /// <inheritdoc />
    public EventMetaData EventMetaData { get; init; } = new();
    
    /// <inheritdoc />
    public List<Subscription<TEvent>> Subscribers { get; init; } = new();
    
    /// <inheritdoc />
    public TEventArgs EventArgs { get; set; }
}