using System.Diagnostics;
using Testing;

namespace ClassicEventExample;

public class EventExample
{
    public static int EventInvocationLimit { get; set; } = 10;
    public static int EventInvocationCount { get; set; } = 0;
    public static int MaxSubscriberCount { get; set; } = 100;
    
    public static async Task Start()
    {
        Counter counter = new Counter();
        for (int i = 0; i < MaxSubscriberCount; i++)
        {
            counter.ThresholdReachedNoArgs += HandleCounterThresholdReached;
        }
        for (int i = EventInvocationCount; i < EventInvocationLimit; i++)
        {
           await counter.Add();
        }
    }

    static void HandleCounterThresholdReached(object? sender, ThresholdReachedEventArgs e)
    {
        Console.WriteLine($"Threshold: {e.Threshold}");
        Console.WriteLine($"Current count: {e.CurrentCount}");
        Console.WriteLine($"Threshold reached at: {e.ThresholdReachedTime}");
    }
    
    static void HandleCounterThresholdReached(object? sender, EventArgs e)
    {
        DateTime startTime = DateTime.UtcNow;
        Thread.Sleep(100);
        DateTime endTime = DateTime.UtcNow;
        Debug.WriteLine($"New Event #{Program.EventInvocationCount} : Start: {startTime:MM/dd/yyyy hh:mm:ss.fff} : End: {endTime:MM/dd/yyyy hh:mm:ss.fff}");
    }
}


class Counter
{
    private int threshold = 0;
    private DateTime thresholdReachedTime;
    public event EventHandler<ThresholdReachedEventArgs>? ThresholdReached;
    public event EventHandler? ThresholdReachedNoArgs;

    public async Task Add()
    {
        EventExample.EventInvocationCount++;
        if (EventExample.EventInvocationCount < threshold)
        {
            return;
        }
        if (thresholdReachedTime == DateTime.MinValue)
        {
            thresholdReachedTime = DateTime.Now;
        }
        //Console.WriteLine("Raising Threshold reached event");
        //ThresholdReachedEventArgs args = new ThresholdReachedEventArgs(threshold, count, thresholdReachedTime);
        //ThresholdReached?.Invoke(this, args);
        ThresholdReachedNoArgs?.Invoke(this, EventArgs.Empty);
    }
}

public class ThresholdReachedEventArgs(int threshold, int currentCount, DateTime thresholdReachedTime) : EventArgs
{
    public int Threshold { get; set; } = threshold;
    public int CurrentCount {get; set;} = currentCount;
    public DateTime ThresholdReachedTime { get; set; } = thresholdReachedTime;
}