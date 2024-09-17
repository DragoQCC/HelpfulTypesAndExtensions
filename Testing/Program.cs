using System.Diagnostics;
using ClassicEventExample;
using HelpfulTypesAndExtensions;
using HelpfulTypesAndExtensions.BaseClasses;
using HelpfulTypesAndExtensions.Interfaces;

namespace Testing;

public class Program
{
    public static int EventInvocationLimit { get; set; } = 10;
    public static int EventInvocationCount { get; set; } = 0;
    
    public static int MaxSubscriberCount { get; set; } = 100;
    
    public static string NewEventLogFile = "C:\\BlogPostContent\\EventingFramework\\NewEventLog.txt";
    public static string ClassicEventLogFile = "C:\\BlogPostContent\\EventingFramework\\ClassicEventLog.txt";
    
    private static Stopwatch _stopwatch = new();
    
    public static async Task Main(string[] args)
    {
        EventingConfiguration eventingConfiguration = new(options =>
        {
            options.SyncType = EventingSyncType.Async;
            options.MaxThreadsPerEvent = -1;
        });
        await RunCounter();
    }

    public static async Task RunCounter()
    {
        Console.WriteLine("Starting Custom Event Example");
        _stopwatch.Start();
        Counter counter = new();
        for (int i = 0; i < MaxSubscriberCount; i++)
        {
            await counter.ThresholdEventNoArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdWithoutArgs);
        }
        
        for (int i = EventInvocationCount; i < EventInvocationLimit; i++)
        {
            await counter.Increment();
        }
        Console.WriteLine("Finished Custom Event Example");
        _stopwatch.Stop();
        Console.WriteLine($"Elapsed time: {_stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine();
        
        Console.WriteLine("Starting Classic Event Example");
        _stopwatch.Restart();
        await EventExample.Start();
        Console.WriteLine("Finished Classic Event Example");
        _stopwatch.Stop();
        Console.WriteLine($"Elapsed time: {_stopwatch.ElapsedMilliseconds} ms");
        
    }
}

/// <summary>
/// Not required just used to separate event handlers from the main program
/// </summary>
public static class CounterEventHandlers
{

    public static async Task<List<ISubscription>> SubscribeToCounterEvents(Counter counter)
    {
        var subscription1 = await counter.ThresholdEventWithArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdReached1);
        var subscription2 = await counter.ThresholdEventWithArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdReached2, exceptionHandler: (e) => Console.WriteLine($"Exception handled: {e.Message}"));
        var subscription3 = await counter.ThresholdEventWithArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdReached3 );
        var subscription4 = await counter.ThresholdEventWithArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdReached4);
        return [subscription1,subscription2,subscription3,subscription4];
    }
    
    public static async ValueTask HandleCounterThresholdWithoutArgs(CounterThresholdEvent @event)
    {
        DateTime startTime = DateTime.UtcNow;
        await Task.Delay(100);
        DateTime endTime = DateTime.UtcNow;
        Debug.WriteLine($"New Event #{Program.EventInvocationCount} : Start: {startTime:MM/dd/yyyy hh:mm:ss.fff} : End: {endTime:MM/dd/yyyy hh:mm:ss.fff}");
    }
    
    // This first event subscriber runs without issues
    public static async ValueTask HandleCounterThresholdReached1(CounterThresholdEvent<CounterThresholdEventArgs> @event)
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
    }
    
    //This third event subscriber is going to run forever
    public static async ValueTask HandleCounterThresholdReached3(CounterThresholdEvent<CounterThresholdEventArgs> @event)
    {
        Console.WriteLine("Event Subscriber #3");
        Console.WriteLine("Mock Exception Throwing with handler");
        while (true)
        {
            await Task.Delay(1000);
            Console.WriteLine("Still running");
        }
    }
    
    // this fourth one is back to a normal event subscriber
    public static async ValueTask HandleCounterThresholdReached4(CounterThresholdEvent<CounterThresholdEventArgs> @event)
    {
        Console.WriteLine("Event Subscriber #4");
        CounterThresholdEventArgs args = @event.EventArgs;
        Console.WriteLine("\t Event args:");
        Console.WriteLine($"\t\t {args}");
        Console.WriteLine();
        await Task.Delay(1000);
    }
}


public class Counter
{
    public CounterThresholdEvent<CounterThresholdEventArgs> ThresholdEventWithArgs { get; } = new();
    public CounterThresholdEvent ThresholdEventNoArgs { get; } = new();
    
    private int Threshold { get; set; } = 0;
    private DateTime ThresholdReachedTime { get; set; }

    public async Task Increment()
    {
        Program.EventInvocationCount++;
        if (Program.EventInvocationCount < Threshold)
        {
            return;
        }
        if(ThresholdReachedTime == DateTime.MinValue)
        {
            ThresholdReachedTime = DateTime.Now;
        }
        await ThresholdEventNoArgs.RaiseEvent(this);
    }
}


public record CounterThresholdEvent() : GenericEvent<CounterThresholdEvent>;
public record CounterThresholdEvent<TEventArgs>() : GenericEvent<CounterThresholdEvent<TEventArgs>, TEventArgs> where TEventArgs : IEventArgs<CounterThresholdEvent<TEventArgs>>;

public record struct CounterThresholdEventArgs(int Threshold, int CurrentCount, DateTime ThresholdReachedTime) : IEventArgs<CounterThresholdEvent<CounterThresholdEventArgs>>;
