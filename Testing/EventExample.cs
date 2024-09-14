/*namespace ClassicEventExample;

public class EventExample
{
    public static void Start()
    {
        Counter counter = new Counter();
        counter.ThresholdReached += HandleCounterThresholdReached;

        Console.WriteLine("press 'a' key to increase total");
        while (Console.ReadKey(true).KeyChar == 'a')
        {
            Console.WriteLine("adding one");
            counter.Add(1);
        }
    }

    static void HandleCounterThresholdReached(object sender, ThresholdReachedEventArgs e)
    {
        Console.WriteLine($"Threshold: {e.Threshold}");
        Console.WriteLine($"Current count: {e.CurrentCount}");
        Console.WriteLine($"Threshold reached at: {e.ThresholdReachedTime}");
    }
}

class Counter
{
    private int threshold = 10;
    private int count;
    private DateTime thresholdReachedTime;
    public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;

    public void Add(int x)
    {
        count += x;
        if (count >= threshold)
        {
            if (thresholdReachedTime == DateTime.MinValue)
            {
                thresholdReachedTime = DateTime.Now;
            }
            ThresholdReachedEventArgs args = new ThresholdReachedEventArgs(threshold, count, thresholdReachedTime);
            OnThresholdReached(args);
        }
    }

    protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
    {
        EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
        if (handler != null)
        {
            handler(this, e);
        }
    }
}

public class ThresholdReachedEventArgs : EventArgs
{
    public int Threshold { get; set; }
    public int CurrentCount {get; set;}
    public DateTime ThresholdReachedTime { get; set; }
    
    public ThresholdReachedEventArgs(int threshold, int currentCount, DateTime thresholdReachedTime)
    {
        Threshold = threshold;
        CurrentCount = currentCount;
        ThresholdReachedTime = thresholdReachedTime;
    }
}*/