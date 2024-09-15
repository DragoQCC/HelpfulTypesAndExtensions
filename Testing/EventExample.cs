namespace ClassicEventExample;

public class EventExample
{
    public static void Start()
    {
        Counter counter = new Counter();
        counter.ThresholdReached += HandleCounterThresholdReached;
        counter.ThresholdReachedNoArgs += HandleCounterThresholdReached;

        for (int i = 0; i < 15; i++)
        {
            counter.Add();
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
        Console.WriteLine("Threshold reached");
    }
}

class Counter
{
    private int threshold = 10;
    private int count;
    private DateTime thresholdReachedTime;
    public event EventHandler<ThresholdReachedEventArgs>? ThresholdReached;
    public event EventHandler? ThresholdReachedNoArgs;

    public void Add()
    {
        count++;
        if (count < threshold)
        {
            return;
        }
        if (thresholdReachedTime == DateTime.MinValue)
        {
            thresholdReachedTime = DateTime.Now;
        }
        ThresholdReachedEventArgs args = new ThresholdReachedEventArgs(threshold, count, thresholdReachedTime);
        ThresholdReached?.Invoke(this, args);
        ThresholdReachedNoArgs?.Invoke(this, EventArgs.Empty);
    }
}

public class ThresholdReachedEventArgs(int threshold, int currentCount, DateTime thresholdReachedTime) : EventArgs
{
    public int Threshold { get; set; } = threshold;
    public int CurrentCount {get; set;} = currentCount;
    public DateTime ThresholdReachedTime { get; set; } = thresholdReachedTime;
}