using HelpfulTypesAndExtensions.Interfaces;

namespace HelpfulTypesAndExtensions;

public static class EventExtensions
{
    public static async Task<Subscription<TEvent>> Subscribe<TEvent>(this TEvent subscribedEvent, Func<TEvent,ValueTask> onEventExecute, Func<TEvent,Task>? onSubscribe = null, Func<Task>? onUnsubscribe = null)
    where TEvent : IEvent<TEvent>
    {
        Console.WriteLine("Subscribing to event");
        SubscriptionRequest<TEvent> subscriptionRequest = new(onEventExecute, onSubscribe, onUnsubscribe);
        return await subscribedEvent.Subscribe(subscriptionRequest);
    }
    
    public static async Task<Subscription<TEvent>> Subscribe<TEvent>(this TEvent subscribedEvent, SubscriptionRequest<TEvent> subscriptionRequest) 
    where TEvent : IEvent<TEvent>
    {
        return await subscribedEvent.Subscribe(subscriptionRequest);
    }
    
    public static async Task NotifySubscribers<TEvent>(this TEvent @event, IEventArgs<TEvent>? args = null)
    where TEvent : IEvent<TEvent>
    {
        Console.WriteLine($"NotifySubscribers Extension Method, Subscriber Count: {@event.Subscribers.Count}");
        await @event.NotifySubscribers(args);
    }
    
    public static async Task DeleteEvent<TEvent>(this TEvent @event) where TEvent : IEvent<TEvent>
    {
        await @event.DeleteEvent();
    }
}
