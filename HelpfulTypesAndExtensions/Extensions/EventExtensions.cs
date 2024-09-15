using HelpfulTypesAndExtensions.Interfaces;

namespace HelpfulTypesAndExtensions;

public static class EventExtensions
{
    public static async Task<Subscription<TEvent>> Subscribe<TEvent>(this TEvent subscribedEvent, 
        Func<TEvent,ValueTask> onEventExecute, Func<TEvent,Task>? onSubscribe = null, 
        Func<Task>? onUnsubscribe = null, Action<Exception>? exceptionHandler = null)
    where TEvent : IEvent<TEvent>
    {
        SubscriptionRequest<TEvent> subscriptionRequest = new(onEventExecute, onSubscribe, onUnsubscribe,exceptionHandler);
        return await subscribedEvent.Subscribe(subscriptionRequest);
    }
    
    public static async Task<Subscription<TEvent>> Subscribe<TEvent>(this TEvent subscribedEvent, SubscriptionRequest<TEvent> subscriptionRequest) 
    where TEvent : IEvent<TEvent>
    {
        return await subscribedEvent.Subscribe(subscriptionRequest);
    }
    
    public static async Task DeleteEvent<TEvent>(this TEvent @event) 
    where TEvent : IEvent<TEvent>
    {
        await @event.DeleteEvent();
    }
    
    public static async Task RaiseEvent<TEvent>(this TEvent @event) 
    where TEvent : IEvent<TEvent>
    {
        await @event.RaiseEvent();
    }
    
    public static async Task RaiseEvent<TEvent,TEventArgs>(this TEvent @event, TEventArgs args) 
    where TEvent : IEvent<TEvent,TEventArgs>
    where TEventArgs : IEventArgs<TEvent>
    {
        await @event.RaiseEvent(args);
    }
}
