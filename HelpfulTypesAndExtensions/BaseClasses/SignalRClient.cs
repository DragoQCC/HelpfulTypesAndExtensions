using Microsoft.AspNetCore.SignalR.Client;

namespace HelpfulTypesAndExtensions;

public abstract class SignalRClient : ISignalRClient
{
    public HubConnection? HubConnection { get; set; }
    public ISignalRClientModel? ClientModel { get; set; }
    public ISignalRHubModel? HubModel { get; set; }

    public abstract Task CreateHubClient();

    public abstract Task<bool> ConnectAsync();

    public abstract Task HandleDisconnectAsync(HubConnection hubConnection, Exception? exception);
    
    public virtual async Task<bool> DisconnectAsync()
    {
        if (HubConnection is null)
        {
            return false;
        }
        return await TryCatch.Try(() => HubConnection.StopAsync());
    }
   
    /// <summary>
    /// Tries to reconnect to the hub, if State changes to Connected, returns true, else false <br/>
    /// Optionally takes an action to perform on failure
    /// </summary>
    /// <param name="onFailure"></param>
    /// <returns></returns>
    virtual protected internal async Task<bool> TryReconnect(Action<Exception>? onFailure = null)
    {
        return await TryCatch.Try(Reconnect, onFailure);
    }

    private async Task<bool> Reconnect()
    {
        if (HubConnection is null)
        {
            return false;
        }
        await HubConnection.StartAsync();
        //check if reconnected 
        return HubConnection.State == HubConnectionState.Connected;
    }
   
}