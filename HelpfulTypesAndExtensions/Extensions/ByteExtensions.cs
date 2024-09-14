using System.Text;

namespace HelpfulTypesAndExtensions;

public static class ByteExtensions
{
    /// <summary>
    /// Tries to convert a byte array to a UTF8 string
    /// If the byte array is null, empty, or cannot be converted, an empty string is returned
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string ToUtf8String(this byte[]? bytes)
    {
        try
        {
            return bytes is null ? "" : Encoding.UTF8.GetString(bytes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "";
        }
    }
    
    
}