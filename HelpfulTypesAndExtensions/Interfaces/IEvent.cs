using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace HelpfulTypesAndExtensions.Interfaces;

public record EventMetaData
{
    public DateTime LastEventTime { get; set; }
    public DateTime CreationTime { get; init; }
    
    
    public EventMetaData()
    {
        CreationTime = DateTime.UtcNow;
        LastEventTime = DateTime.UtcNow;
    }
}


//need to fill in, might make generic
public interface IEventArgs<TEvent> where TEvent : IEvent<TEvent>
{
    
}

public interface IEvent<TEvent> : IEventArgs<TEvent> where TEvent : IEvent<TEvent>
{
    public EventMetaData EventMetaData { get; init; }
    public List<Subscription<TEvent>> Subscribers { get; init; }

    public IEventArgs<TEvent>? EventArgs { get; set; }

    public async Task<Subscription<TEvent>> Subscribe(SubscriptionRequest<TEvent> subscriptionRequest)
    {
        Subscription<TEvent> subscription = new(subscriptionRequest);
        await AddSubscriber(subscription);
        return subscription;
    }
    
    public async Task DeleteEvent()
    {
        foreach (Subscription<TEvent> subscription in Subscribers)
        {
            await subscription.DisposeAsync();
        }
        Subscribers.Clear();
    }
    
    public async Task AddSubscriber(Subscription<TEvent> subscription)
    {
        if(Subscribers.Contains(subscription))
        {
            return;
        }
        Subscribers.Add(subscription);
        Console.WriteLine("Adding subscriber");
        Console.WriteLine("Subscribers: " + Subscribers.Count);
        _ = Task.Run(async () => await subscription.HandleSubscribe((TEvent)this));
    }
    
    public async Task RemoveSubscriber(Subscription<TEvent> subscription)
    {
        if(Subscribers.Contains(subscription))
        {
            Subscribers.Remove(subscription);
        }
        await subscription.DisposeAsync();
    }
    
    public async Task NotifySubscribers(IEventArgs<TEvent>? args = null)
    {
        Console.WriteLine("Notifying subscribers");
        Console.WriteLine("Subscriber Count: " + Subscribers.Count);
        EventArgs = args;
        foreach (Subscription<TEvent> subscription in Subscribers)
        {
            await subscription.HandleEventExecute((TEvent)this);
        }
    }
   
    public static Subscription<TEvent> operator +(IEvent<TEvent> @event, SubscriptionRequest<TEvent> subscriptionRequest)
    {
        return @event.Subscribe(subscriptionRequest).Result;
    } 
    
    /*public static Subscription<TEvent> operator +(IEvent<TEvent> @event, TEvent event2)
    {
        @event = event2;
        return (TEvent)@event;
    } */
}



public record struct SubscriptionRequest<TEvent> where TEvent : IEvent<TEvent>
{
    public Func<TEvent, ValueTask> OnEventExecute { get; init; }
    public Func<Task>? OnUnsubscribe { get; init; }
    public Func<TEvent,Task>? OnSubscribe { get; init; }
    
    public SubscriptionRequest(Func<TEvent, ValueTask> onEventExecute, Func<TEvent,Task>? onSubscribe = null, Func<Task>? onUnsubscribe = null)
    {
        OnEventExecute = onEventExecute;
        OnUnsubscribe = onUnsubscribe;
        OnSubscribe = onSubscribe;
    }
    
    public static implicit operator SubscriptionRequest<TEvent>(Func<TEvent, ValueTask> onEventExecute)
    {
        return new(onEventExecute);
    }
    
}


public sealed record Subscription<TEvent> : IDisposable, IAsyncDisposable
where TEvent : IEvent<TEvent>
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
    
    public Subscription(SubscriptionRequest<TEvent> subscriptionRequest)
    {
        OnEventExecute = subscriptionRequest.OnEventExecute;
        OnUnsubscribe = subscriptionRequest.OnUnsubscribe;
        OnSubscribe = subscriptionRequest.OnSubscribe;
        SubCancelTokenSource = new();
        SubCancelToken = SubCancelTokenSource.Token;
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


