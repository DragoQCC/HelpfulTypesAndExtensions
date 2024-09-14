using System.Runtime.CompilerServices;
using System.Xml.Schema;

namespace HelpfulTypesAndExtensions.Interfaces;


public interface IEvent<out TEvent>
{
    IDisposable Subscribe(Subscription<TEvent> subscription);
}

internal record struct EventData<TEvent>
{
    internal TEvent EventType { get; init; }
    public DateTime LastEventTime { get; set; }
    public DateTime CreationTime { get; init; }
    
    
    public EventData(TEvent eventType)
    {
        EventType = eventType;
        CreationTime = DateTime.UtcNow;
        LastEventTime = DateTime.UtcNow;
    }
}

public class EventManager<TEvent> where TEvent : notnull
{
    private Dictionary<TEvent,List<Subscription<TEvent>>> Subscribers { get; init; }
    private EventData<TEvent> EventData { get; init; }
    
    public EventManager()
    {
        Subscribers = [];
    }
    
    public EventManager(TEvent @event)
    {
        Subscribers = [];
        EventData = new(@event);
    }

    public async Task AddSubscriber(TEvent @event, Subscription<TEvent> subscription)
    {
        if (Subscribers.TryGetValue(@event, out List<Subscription<TEvent>>? value))
        {
            value.Add(subscription);
        }
        else
        {
            Subscribers.Add(@event, [subscription]);
        }
        await subscription.HandleSubscribe(@event);
    }
    
    public async Task RemoveSubscriber(TEvent @event, Subscription<TEvent> subscription)
    {
        if (Subscribers.TryGetValue(@event, out List<Subscription<TEvent>>? value))
        {
            value.Remove(subscription);
        }
        else
        {
            Subscribers.Remove(@event);
        }
        await subscription.DisposeAsync();
    }
    
    public virtual async Task NotifySubscribers(TEvent @event)
    {
        foreach (Subscription<TEvent> subscription in Subscribers.TryGetValue(@event, out List<Subscription<TEvent>>? value) ? value : [])
        {
            await subscription.HandleEventExecute(@event);
        }
    }
    
   
}


public interface ISubscription<in TEvent> : IDisposable, IAsyncDisposable
{
    internal Task HandleSubscribe(TEvent @event);
    internal Task HandleUnsubscribe();

    public ValueTask HandleEventExecute(TEvent @event, Action<Exception>? exceptionHandler = null);
}

public sealed class Subscription<TEvent> : ISubscription<TEvent>
{
    private Func<TEvent, ValueTask> OnEventExecute { get; set; }
    private Func<Task>? OnUnsubscribe { get; init; }
    private Func<TEvent,Task>? OnSubscribe { get; init; }
    
    internal CancellationToken SubCancelToken { get; init; }
    internal CancellationTokenSource SubCancelTokenSource { get; init; }
    
    private bool _isDisposed;
    
    
    public Subscription(Func<TEvent, ValueTask> onEventExecute, Func<TEvent,Task>? onSubscribe = null, Func<Task>? onUnsubscribe = null)
    {
        OnEventExecute = onEventExecute;
        OnUnsubscribe = onUnsubscribe;
        OnSubscribe = onSubscribe;
        SubCancelTokenSource = new();
        SubCancelToken = SubCancelTokenSource.Token;
    }
    
    async Task ISubscription<TEvent>.HandleSubscribe(TEvent @event)
    {
        await HandleSubscribe(@event);
    }
    
    internal async Task HandleSubscribe(TEvent @event)
    {
        if (OnSubscribe is not null)
        {
            await OnSubscribe.Invoke(@event);
        }
        else
        {
            await Task.CompletedTask;
        }
    }
    
    async Task ISubscription<TEvent>.HandleUnsubscribe()
    {
        await HandleUnsubscribe();
    }
    
    internal async Task HandleUnsubscribe()
    {
        if (OnUnsubscribe is not null)
        {
            await OnUnsubscribe.Invoke();
        }
        else
        {
            await Task.CompletedTask;
        }
    }
    
    public async ValueTask HandleEventExecute(TEvent @event, Action<Exception>? exceptionHandler = null)
    {
        try
        {
            if(SubCancelToken.IsCancellationRequested)
            {
                return;
            }
            await OnEventExecute(@event);
        }
        catch (Exception e)
        {
            if (exceptionHandler is not null)
            {
                exceptionHandler.Invoke(e);
            }
            else
            {
                throw;
            }
        }
    }
    
    
    /// <inheritdoc />
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }
        _isDisposed = true;
        HandleUnsubscribe().Wait(SubCancelToken);
        SubCancelTokenSource.Cancel();
        SubCancelTokenSource.Dispose();
    }
    
    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if(_isDisposed)
        {
            return;
        }
        _isDisposed = true;
        await HandleUnsubscribe();
        await SubCancelTokenSource.CancelAsync();
        SubCancelTokenSource.Dispose();
    }
}