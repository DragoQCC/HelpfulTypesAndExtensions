using System.Text;
using System.Text.Json;

namespace HelpfulTypesAndExtensions;

public static class JsonSerialization
{

    /// <summary>
    /// Serializes an object to a JSON byte array
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static byte[] Serialize<T>(this T data)
    {
        try
        {
            using var ms = new MemoryStream();
            JsonSerializer.Serialize(ms, data);
            return ms.ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return [];
        }
    }

   
    /// <summary>
    /// Serializes an object to a JSON string
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string? SerializeToString<T>(this T data)
    {
        try
        {
            using var ms = new MemoryStream();
            JsonSerializer.Serialize(ms, data);
            string json = Encoding.UTF8.GetString(ms.ToArray());
            return json;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return null;
        }
    }


    /// <summary>
    /// Attempts to deserialize a JSON byte array to an object
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? Deserialize<T>(this byte[] data)
    {
        var jsonSerializerOptions = new JsonSerializerOptions();
        jsonSerializerOptions.AllowTrailingCommas = true;
        jsonSerializerOptions.PropertyNameCaseInsensitive = true;
        jsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        try
        {
            switch (data.Length)
            {
                case 0:
                    return default;
                //if the last byte is a 0 then remove it
                case >= 1 when data[^1] == 0:
                    data = data.Take(data.Length - 1).ToArray();
                    break;
            }
            if (IsValidJson(data))
            {
                return JsonSerializer.Deserialize<T>(data, jsonSerializerOptions);
            }
            
            Console.WriteLine("Input data is not a valid JSON, returning default value");
            return default;
        }
        catch (Exception ex)
        {
            string? fixedJson = TryToFixJson(Encoding.UTF8.GetString(data));
            if (fixedJson != null)
            {
                return JsonSerializer.Deserialize<T>(fixedJson, jsonSerializerOptions);
            }
            Console.WriteLine("Error during JSON deserialization");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return default;
        }
    }
    
    /// <summary>
    /// Takes in a json string and deserializes it into an object
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? Deserialize<T>(this string data)
    {
        try
        {
            return Deserialize<T>(Encoding.UTF8.GetBytes(data));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return default;
        }
    }
    
    
    /// <summary>
    /// Given a byte array, determines if it is valid JSON
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool IsValidJson(byte[] data)
    {
        try
        {
            // Attempt to parse the byte array as JSON
            using JsonDocument doc = JsonDocument.Parse(data);
            // If parsing succeeds, it's valid JSON
            return true;
        }
        catch (Exception)
        {
            // If an Exception is thrown, the byte array is not valid JSON
            return false;
        }
    }

    /// <summary>
    /// Given a string, determines if it is valid JSON
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool IsValidJson(string data)
    {
        try
        {
            // Attempt to parse the string as JSON
            using JsonDocument doc = JsonDocument.Parse(data);
            // If parsing succeeds, it's valid JSON
            return true;
        }
        catch (Exception)
        {
            // If an Exception is thrown, the string is not valid JSON
            return false;
        }
    }
    
    
    /// <summary>
    /// this assumes the json data is a list of objects that is currently structured like {"type":"data"}{"type":"data"} instead of [{"type":"data"},{"type":"data"}]
    /// this will fix the json by adding a comma between the objects and wrapping the whole thing in square brackets
    /// </summary>
    /// <param name="brokenJson"></param>
    /// <returns></returns>
    private static string? TryToFixJson(string brokenJson)
    {
        try
        {
            string fixedJson = brokenJson.Replace("}{", "},{");
            fixedJson = $"[{fixedJson}]";
            return fixedJson;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return null;
        }
    }
}