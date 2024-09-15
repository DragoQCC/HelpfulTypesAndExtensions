using ClassicEventExample;
using HelpfulTypesAndExtensions;
using HelpfulTypesAndExtensions.BaseClasses;
using HelpfulTypesAndExtensions.Interfaces;

namespace Testing;

class Program
{
    static async Task Main(string[] args)
    {
        /*Counter counter = new();
        await counter.ThresholdEventWithArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdReached);
        await counter.ThresholdEventNoArgs.Subscribe(CounterEventHandlers.HandleCounterThresholdReached);
        
        for (int i = 0; i < 15; i++)
        {
            await counter.Increment();
        }*/
        
        EventExample.Start();
    }
    
    
}

public static class CounterEventHandlers
{
    public static async ValueTask HandleCounterThresholdReached(CounterThresholdEvent @event)
    {
        Console.WriteLine($"\t Event metadata: {@event.EventMetaData}");
        CounterThresholdEventArgs args = @event.EventArgs;
        Console.WriteLine("\t Event args:");
        Console.WriteLine($"\t\t Threshold: {args.Threshold}");
        Console.WriteLine($"\t\t Current count: {args.CurrentCount}");
        Console.WriteLine($"\t\t Threshold reached at: {args.ThresholdReachedTime}");
        Console.WriteLine();
        await Task.Delay(1000);
    }
    
    public static async ValueTask HandleCounterThresholdReached(CounterThresholdEventNoArgs @event)
    {
        Console.WriteLine($"\t Event metadata: {@event.EventMetaData}");
        Console.WriteLine();
        await Task.Delay(1000);
    }
}


public class Counter
{
    public CounterThresholdEvent ThresholdEventWithArgs { get; } = new();
    public CounterThresholdEventNoArgs ThresholdEventNoArgs { get; } = new();
    
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
        await ThresholdEventNoArgs.RaiseEvent();
        Console.WriteLine("Raising threshold event with args");
        var args = new CounterThresholdEventArgs(Threshold, Count, ThresholdReachedTime);
        await ThresholdEventWithArgs.RaiseEvent(args);
    }
}


public record CounterThresholdEvent() : GenericEvent<CounterThresholdEvent, CounterThresholdEventArgs>;
public record CounterThresholdEventNoArgs() : GenericEvent<CounterThresholdEventNoArgs>;
public record struct CounterThresholdEventArgs(int Threshold, int CurrentCount, DateTime ThresholdReachedTime) : IEventArgs<CounterThresholdEvent>;
