using ClassicEventExample;
using HelpfulTypesAndExtensions;
using HelpfulTypesAndExtensions.BaseClasses;
using HelpfulTypesAndExtensions.Interfaces;

namespace Testing;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Main Program Started");
        await RunCounter();
        Console.WriteLine("Main Program Finished, press any key to exit");
        Console.ReadLine();
    }

    public static async Task RunCounter()
    {
        Counter counter = new();
        await using var subscription1 = await counter.ThresholdEventWithArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdReached);
        for (int i = 0; i < 15; i++)
        {
            await counter.Increment();
        }
        Console.WriteLine("Counter processing finished");
    }
}

/// <summary>
/// Not required just used to separate event handlers from the main program
/// </summary>
public static class CounterEventHandlers
{

    public static async Task<List<Subscription<CounterThresholdEvent<CounterThresholdEventArgs>>>> SubscribeToCounterEvents(Counter counter)
    {
        var subscription1 = await counter.ThresholdEventWithArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdReached);
        var subscription2 = await counter.ThresholdEventWithArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdReached2);
        var subscription3 = await counter.ThresholdEventWithArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdReached3, exceptionHandler: (e) => Console.WriteLine($"Exception handled: {e.Message}"));
        return [subscription1,subscription2,subscription3];
    }
    
    public static async ValueTask HandleCounterThresholdReached(CounterThresholdEvent<CounterThresholdEventArgs> @event)
    {
        Console.WriteLine("Event Subscriber #1");
        CounterThresholdEventArgs args = @event.EventArgs;
        Console.WriteLine("\t Event args:");
        Console.WriteLine($"\t\t {args}");
        Console.WriteLine();
        await Task.Delay(1000);
    }
    
    //This second event subscriber is going to throw an exception
    public static async ValueTask HandleCounterThresholdReached2(CounterThresholdEvent<CounterThresholdEventArgs> @event)
    {
        Console.WriteLine("Event Subscriber #2");
        Console.WriteLine("Mock Exception Throwing without handler");
        throw new Exception("Mock Exception Thrown");
        Console.WriteLine();
        await Task.Delay(1000);
    }
    
    //This third event subscriber is going to throw an exception but it will have registered an exception handler
    public static async ValueTask HandleCounterThresholdReached3(CounterThresholdEvent<CounterThresholdEventArgs> @event)
    {
        Console.WriteLine("Event Subscriber #3");
        Console.WriteLine("Mock Exception Throwing with handler");
        throw new Exception("Mock Exception Thrown");
        Console.WriteLine();
        await Task.Delay(1000);
    }
    
    public static async ValueTask HandleCounterThresholdReached(CounterThresholdEvent @event)
    {
        Console.WriteLine($"\t Event metadata: {@event.EventMetaData}");
        Console.WriteLine($"\t Event Caller: {@event.EventMetaData.EventCaller}");
        Console.WriteLine();
        await Task.Delay(1000);
    }
}


public class Counter
{
    public CounterThresholdEvent<CounterThresholdEventArgs> ThresholdEventWithArgs { get; } = new();
    public CounterThresholdEvent ThresholdEventNoArgs { get; } = new();
    
    public int Count { get; set; }
    private int Threshold { get; set; } = 10;
    private DateTime ThresholdReachedTime { get; set; }

    public async Task Increment()
    {
        Count++;
        if (Count < Threshold)
        {
            return;
        }
        if(ThresholdReachedTime == DateTime.MinValue)
        {
            ThresholdReachedTime = DateTime.Now;
        }
        Console.WriteLine("Raising threshold event");
        await ThresholdEventNoArgs.RaiseEvent(this);
        Console.WriteLine("Raising threshold event with args");
        var args = new CounterThresholdEventArgs(Threshold, Count, ThresholdReachedTime);
        await ThresholdEventWithArgs.RaiseEvent(args,this);
    }
}

/*public class OtherCounter
{
    public CounterThresholdEvent<CounterThresholdEventArgs> ThresholdEventWithArgs { get; } = new();
    public CounterThresholdEvent ThresholdEventNoArgs { get; } = new();
    
    public int Count { get; set; }
    private int Threshold { get; set; } = 5;
    private DateTime ThresholdReachedTime { get; set; }

    public async Task Increment()
    {
        Count++;
        if (Count < Threshold)
        {
            return;
        }
        if(ThresholdReachedTime == DateTime.MinValue)
        {
            ThresholdReachedTime = DateTime.Now;
        }
        Console.WriteLine("Raising threshold event");
        await ThresholdEventNoArgs.RaiseEvent(this);
        Console.WriteLine("Raising threshold event with args");
        var args = new CounterThresholdEventArgs(Threshold, Count, ThresholdReachedTime);
        await ThresholdEventWithArgs.RaiseEvent(args,this);
    }
}*/


public record CounterThresholdEvent() : GenericEvent<CounterThresholdEvent>;
public record CounterThresholdEvent<TEventArgs>() : GenericEvent<CounterThresholdEvent<TEventArgs>, TEventArgs> where TEventArgs : IEventArgs<CounterThresholdEvent<TEventArgs>>;

public record struct CounterThresholdEventArgs(int Threshold, int CurrentCount, DateTime ThresholdReachedTime) : IEventArgs<CounterThresholdEvent<CounterThresholdEventArgs>>;
