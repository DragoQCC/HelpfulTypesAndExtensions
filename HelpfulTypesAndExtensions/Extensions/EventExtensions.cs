using HelpfulTypesAndExtensions.Interfaces;

namespace HelpfulTypesAndExtensions;

public static class EventExtensions
{
    public static async Task<Subscription<TEvent>> Subscribe<TEvent>(this TEvent subscribedEvent, Func<TEvent,Task> onEventExecute, Func<Task>? onSubscribe = null, Func<Task>? unsubscribe = null)
    {
        Subscription<TEvent> subscription = new Subscription<TEvent>(onEventExecute,unsubscribe);
        if (onSubscribe is not null)
        {
            await onSubscribe();
        }
        return subscription;
    }
    
    public static async Task Subscribe<TEvent>(this TEvent subscribedEvent, Subscription subscription)
    {
        subscribedEvent.AddSubscriber(subscription);
    }
    
    public static async Task NotifySubscribers<TEvent>(this TEvent @event)
    {
       await @event.NotifySubscribers();
    }
}
