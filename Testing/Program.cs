using HelpfulTypesAndExtensions;
using HelpfulTypesAndExtensions.BaseClasses;
using HelpfulTypesAndExtensions.Interfaces;

namespace Testing;

class Program
{
    static async Task Main(string[] args)
    {
        /*/#1#/Example task Event that prints a message, sleeps, and then prints another message
        TaskingEvent taskingEvent = new(async () =>
        {
            Console.WriteLine("Sleeping for 5 seconds");
            await Task.Delay(5000);
            Console.WriteLine("Sleep completed");
        });
        
        //example subscription request that prints a message on initial subscription and unsubscription
        //also prints the status of the tasking event when the event is executed
        SubscriptionRequest<TaskingEvent> subscriptionRequest = new
        (
            onEventExecute: async (@event) =>
            {
                Console.WriteLine($"Tasking event status: {@event.Status}");
            },
            onSubscribe: async (@event) =>
            {
                Console.WriteLine($"Subscribed to new tasking event {@event}");
            },
            onUnsubscribe: async () =>
            {
                Console.WriteLine("Unsubscribed from tasking event");
            }
        );#1#
        
       //var mySubscription = await taskingEvent.Subscribe(subscriptionRequest);
       //Subscription<TaskingEvent> mySecondSubscription = (IEvent<TaskingEvent>)taskingEvent + (SubscriptionRequest<TaskingEvent>)HandleEvent;
       
       
       

        // This is a test to see if the task will be cancelled after 3 seconds and that the subscribers will be notified correctly
        //if so it will print "Tasking event status: TaskCancelled"
        /*var targetTask = taskingEvent.ExecuteTask();
        var finishedTask = await Task.WhenAny([targetTask,Task.Delay(3000)]);
        if (finishedTask != targetTask)
        {
            taskingEvent.Status = TaskingEvent.TaskingEventEnum.TaskCancelled;
            await taskingEvent.NotifySubscribers();
        }
        if (targetTask.Status != TaskStatus.Running)
        {
            await mySubscription.DisposeAsync();
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();#1#*/
        
        Counter counter = new();
        await counter.ThresholdEvent.Subscribe(HandleCounterThresholdReached);
        
        for (int i = 0; i < 15; i++)
        {
            //await counter.Increment();
            await counter.Increment();
        }
        Console.ReadLine();
    }
    
    static async ValueTask HandleCounterThresholdReached(GenericEvent @event)
    {
        Console.WriteLine("Counter threshold reached");
        if (@event.EventArgs is CounterThresholdEventArgs args)
        {
            Console.WriteLine($"Threshold: {args.Threshold}");
            Console.WriteLine($"Current count: {args.CurrentCount}");
            Console.WriteLine($"Threshold reached at: {args.ThresholdReachedTime}");
        }
    }
    
    
    /*static async ValueTask HandleEvent(TaskingEvent @event)
    {
        Console.WriteLine($"Tasking event status: {@event.Status}");
    }*/
}


public class Counter
{
    //public GenericEvent ThresholdEvent => new(); // breaks
    public GenericEvent ThresholdEvent { get; } = new(); //works -> this should be the same as the => new() version but that version breaks
    
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
        Console.WriteLine($"Increment threshold Event trigger, Subscriber count: {ThresholdEvent.Subscribers.Count}");
        var args = new CounterThresholdEventArgs(Threshold, Count, ThresholdReachedTime);
        //await ThresholdEvent.RaiseEvent(args);
        await ThresholdEvent.RaiseEvent(args);
       
        /*if (Count == Threshold + 1)
        {
            Console.WriteLine("Handling threshold breach");
            await ThresholdEvent.DeleteEvent();
        }
        else
        {
            
        }*/
    }
}

internal class CounterThresholdEventArgs : IEventArgs<GenericEvent>
{
    public int Threshold { get; init; }
    public int CurrentCount { get; init; }
    public DateTime ThresholdReachedTime { get; init; }
    
    public CounterThresholdEventArgs(int threshold, int currentCount, DateTime thresholdReachedTime)
    {
        Threshold = threshold;
        CurrentCount = currentCount;
        ThresholdReachedTime = thresholdReachedTime;
    }
}
