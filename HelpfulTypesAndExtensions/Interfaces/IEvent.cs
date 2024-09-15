using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace HelpfulTypesAndExtensions.Interfaces;

public record EventMetaData
{
    public DateTime LastEventTime { get; set; } = DateTime.UtcNow;
    public DateTime CreationTime { get; init; } = DateTime.UtcNow;
}

public interface IEventArgs;

public interface IEventArgs<TEvent> : IEventArgs where TEvent : IEvent;


public interface IEvent;

public interface IEvent<TEvent> : IEvent
where TEvent : IEvent
{
    public EventMetaData EventMetaData { get; init; }
    public List<Subscription<TEvent>> Subscribers { get; init; }
    
    public async Task RaiseEvent()
    {
        EventMetaData.LastEventTime = DateTime.Now;
        try
        {
            await NotifySubscribers();
        }
        catch (Exception e)
        {
            throw;
        }
    }
    
    public async Task<Subscription<TEvent>> Subscribe(SubscriptionRequest<TEvent> subscriptionRequest)
    {
        Subscription<TEvent> subscription = new(subscriptionRequest);
        await AddSubscriber(subscription);
        return subscription;
    }
    
    public async Task<bool> Unsubscribe(Subscription<TEvent> subscription)
    {
        if(Subscribers.Contains(subscription))
        {
            await RemoveSubscriber(subscription);
            return true;
        }
        return false;
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
        //TODO: Eval performance with long running or error prone event handlers
        await subscription.HandleSubscribe((TEvent)this);
    }
    
    public async Task RemoveSubscriber(Subscription<TEvent> subscription)
    {
        if(Subscribers.Contains(subscription))
        {
            Subscribers.Remove(subscription);
        }
        await subscription.DisposeAsync();
    }
    
    public async Task NotifySubscribers()
    {
        foreach (Subscription<TEvent> subscription in Subscribers)
        {
            try
            {
                //TODO: Eval performance with long running or error prone event handlers
                await subscription.HandleEventExecute((TEvent)this);
            }
            catch
            {
                throw;
            }
        }
    }
   
    public static Subscription<TEvent> operator +(IEvent<TEvent> @event, SubscriptionRequest<TEvent> subscriptionRequest)
    {
        return @event.Subscribe(subscriptionRequest).Result;
    } 
}

public interface IEvent<TEvent,TEventArgs> : IEvent<TEvent>
where TEvent : IEvent
where TEventArgs : IEventArgs<TEvent>
{
    public TEventArgs EventArgs { get; set; }

    
    public async Task RaiseEvent(TEventArgs args)
    {
        EventMetaData.LastEventTime = DateTime.Now;
        EventArgs = args;
        try
        {
            await NotifySubscribers();
        }
        catch (Exception e)
        {
            throw;
        }
    }
   
    public static Subscription<TEvent> operator +(IEvent<TEvent,TEventArgs> @event, SubscriptionRequest<TEvent> subscriptionRequest)
    {
        return @event.Subscribe(subscriptionRequest).Result;
    } 
}



public record struct SubscriptionRequest<TEvent> where TEvent : IEvent
{
    public Func<TEvent, ValueTask> OnEventExecute { get; init; }
    public Func<Task>? OnUnsubscribe { get; init; }
    public Func<TEvent,Task>? OnSubscribe { get; init; }
    public Action<Exception>? ExceptionHandler { get; init; }
    
    public SubscriptionRequest(Func<TEvent, ValueTask> onEventExecute, Func<TEvent,Task>? onSubscribe = null, Func<Task>? onUnsubscribe = null, Action<Exception>? exceptionHandler = null)
    {
        OnEventExecute = onEventExecute;
        OnUnsubscribe = onUnsubscribe;
        OnSubscribe = onSubscribe;
        ExceptionHandler = exceptionHandler;
    }
    
    public static implicit operator SubscriptionRequest<TEvent>(Func<TEvent, ValueTask> onEventExecute)
    {
        return new(onEventExecute);
    }
    
}


public sealed record Subscription<TEvent> : IDisposable, IAsyncDisposable
where TEvent : IEvent
{
    private Func<TEvent, ValueTask> OnEventExecute { get; set; }
    private Func<Task>? OnUnsubscribe { get; init; }
    private Func<TEvent,Task>? OnSubscribe { get; init; }
    private Action<Exception>? ExceptionHandler { get; init; }
    internal CancellationToken SubCancelToken { get; init; }
    internal CancellationTokenSource SubCancelTokenSource { get; init; }
    
    private bool _isDisposed;
    
    
    public Subscription(Func<TEvent, ValueTask> onEventExecute, Func<TEvent,Task>? onSubscribe = null, Func<Task>? onUnsubscribe = null, Action<Exception>? exceptionHandler = null)
    {
        OnEventExecute = onEventExecute;
        OnUnsubscribe = onUnsubscribe;
        OnSubscribe = onSubscribe;
        ExceptionHandler = exceptionHandler;
        SubCancelTokenSource = new();
        SubCancelToken = SubCancelTokenSource.Token;
    }
    
    public Subscription(SubscriptionRequest<TEvent> subscriptionRequest)
    {
        OnEventExecute = subscriptionRequest.OnEventExecute;
        OnUnsubscribe = subscriptionRequest.OnUnsubscribe;
        OnSubscribe = subscriptionRequest.OnSubscribe;
        ExceptionHandler = subscriptionRequest.ExceptionHandler;
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
    
    public async ValueTask HandleEventExecute(TEvent @event)
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
            if (ExceptionHandler is not null)
            {
                ExceptionHandler.Invoke(e);
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


