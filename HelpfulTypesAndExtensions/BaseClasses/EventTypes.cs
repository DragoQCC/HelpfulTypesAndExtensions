using HelpfulTypesAndExtensions.Interfaces;


namespace HelpfulTypesAndExtensions.BaseClasses;
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

