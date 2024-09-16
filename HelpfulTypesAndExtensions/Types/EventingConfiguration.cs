namespace HelpfulTypesAndExtensions;

// The options for configuring the eventing system
public class EventingOptions
{
    // controls if the event handlers are called sequentially or in parallel
    public EventingSyncType SyncType { get; set; } = EventingSyncType.Async;
    
    //The maximum number of threads that can be used to handle an event, each new thread will start a new subscriber
    //only used for async event handling
    public int MaxThreadsPerEvent { get; set; }
    
    //If true then more than one subscriber can subscribe to the same event
    public bool AllowMultipleSubscribers { get; set; } = true;
    
    //If Subscriber A blocks for longer then 1 second then the next subscriber will be called on a new thread regardless of the MaxThreadsPerEvent or SyncType
    public TimeSpan StartNextEventHandlerAfter { get; set; } = TimeSpan.FromMilliseconds(1000);

    
    
    public EventingOptions()
    {
        if(MaxThreadsPerEvent == 0)
        {
            MaxThreadsPerEvent = SetThreadCount(this);
        }
    }
    
    
    internal static int SetThreadCount(EventingOptions options)
    {
        return options.SyncType == EventingSyncType.Async ? 10 : 1;
    }
    
    
}


// Stores the configuration for the eventing system
public class EventingConfiguration
{
    
}


public enum EventingSyncType
{
    Sync,
    Async
}