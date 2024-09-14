namespace HelpfulTypesAndExtensions.Interfaces;


/// <summary>
/// Allows strongly typed hub clients so that the hub may invoke methods on the client <br/>
/// These are handled by the client with .On methods <br/>
/// Extend this interface to define the methods that the hub can invoke on the client
/// </summary>
public partial interface ISignalRClientModel
{
    
}