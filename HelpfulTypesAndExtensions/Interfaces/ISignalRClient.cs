using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;

namespace HelpfulTypesAndExtensions.Interfaces;


/// <summary>
/// NOT used for invoking methods on the client <br/>
/// defines general methods for the SignalR client to connect to the server such as OnConnectedAsync, OnDisconnectedAsync, etc. <br/>
/// For defining methods that the hub can invoke on the client, use <see cref="ISignalRClientModel"/> <br/>
/// </summary>
public interface ISignalRClient
{
    internal HubConnection? HubConnection { get; set; }
    internal ISignalRClientModel ClientModel { get; set; }
    internal ISignalRHubModel HubModel { get; set; }
    
    public Task CreateHubClient();
    public Task<bool> ConnectAsync();
    public Task<bool> DisconnectAsync();
    public Task HandleDisconnectAsync(HubConnection hubConnection, Exception? exception);
}


