namespace HelpfulTypesAndExtensions;

#if NETCORE
/// <summary>
/// A strongly typed SignalR hub, that implements the ISignalRHubModel interface and has a strongly typed client model <br/>
/// Extend the <see cref="ISignalRHubModel"/> to define the methods inside the hub <br/>
/// Extend the <see cref="ISignalRClientModel"/> to define the methods that the hub can invoke on the client
/// </summary>
public abstract class StrongSignalRHub : Hub<ISignalRClientModel>, ISignalRHubModel;
#endif