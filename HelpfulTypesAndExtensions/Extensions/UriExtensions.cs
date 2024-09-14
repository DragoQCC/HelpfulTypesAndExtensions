namespace HelpfulTypesAndExtensions;

public static class UriHelpers
{
    public static bool IsHttps(this Uri uri) => string.Equals(uri.Scheme, "https", StringComparison.Ordinal);

    
    /// <summary>
    /// Checks if a provided address is a valid hostname or ip address
    /// Throws an ArgumentException if the address is null, empty or not a valid hostname or ip address
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static bool IsValidUrl(this string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address cannot be null or empty");
        }
        //check if address is a hostname, if not check it is a valid ipv4 or ipv6 address
        var addressCheckResult =  Uri.CheckHostName(address);
        if (addressCheckResult != UriHostNameType.Unknown)
        {
            return true;
        }
        throw new ArgumentException("Address is not a valid hostname or ip address");
    }
}