using HelpfulTypesAndExtensions;
using HelpfulTypesAndExtensions.Interfaces;

namespace Testing;

class Program
{
    static async Task Main(string[] args)
    {
        TaskingEvent taskingEvent = new(async () =>
        {
            Console.WriteLine("Sleeping for 5 seconds");
            await Task.Delay(5000);
            Console.WriteLine("Sleep completed");
        });
        
        Subscription<TaskingEvent> taskSubscription = new(async (taskingEvent) =>
        {
            Console.WriteLine($"Tasking event status: {taskingEvent.Status}");
        });
        
        
        
        //await taskingEvent.Subscribe(taskSubscription);

        var targetTask = taskingEvent.Execute();
        var finishedTask = await Task.WhenAny([targetTask,Task.Delay(3000)]);
        if (finishedTask != targetTask)
        {
            taskingEvent.Status = TaskingEvent.TaskingEventEnum.TaskCancelled;
            await taskingEvent.NotifySubscribers();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
}


public class TaskingEvent : IEvent<Task>
{
    //public List<ISubscription<TaskingEvent>> Subscribers { get; set; } = new();
    private EventManager<Task> _eventManager = new();
    public DateTime LastEventTime { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
    internal Func<Task> WrappedTask { get; set; }
    internal TaskingEventEnum Status { get; set; }
    
    
    public TaskingEvent(Func<Task> task)
    {
        WrappedTask = task;
    }


    public async Task Execute()
    {
        Status = TaskingEventEnum.TaskStarted;
        LastEventTime = DateTime.Now;
        await _eventManager.NotifySubscribers();
        try
        {
            await WrappedTask();
            Status = TaskingEventEnum.TaskCompleted;
        }
        catch (Exception e)
        {
            Status = TaskingEventEnum.TaskFailed;
            throw;
        }
        finally
        {
            LastEventTime = DateTime.Now;
            await this.NotifySubscribers();
        }
    }
    
    internal enum TaskingEventEnum
    {
        TaskStarted,
        TaskCompleted,
        TaskFailed,
        TaskCancelled
    }
}

