using System.Reflection;
using System.Security.Cryptography;

namespace HelpfulTypesAndExtensions;

public static class Pathing
{
    private static string PathSeperator = Path.DirectorySeparatorChar.ToString();
    private static string PathingTraverseUp = ".." + PathSeperator;
    private static string _baseFolder = "";
    
    static string[] pathSplitCharacters = ["/", "\\"];
    
    public static string GetBaseFolderLocation()
    {
        if(_baseFolder != "")
        {
            return _baseFolder;
        }
        string folderpath = AppDomain.CurrentDomain.BaseDirectory;
        //split the folder path by the name of the assembly to get the base folder
        string _splitKey = Assembly.GetEntryAssembly()?.GetName().Name ?? "";
        //if _splitKey contains a . then split on the dots and take the last element
        if (_splitKey.Contains('.'))
        {
            _splitKey = _splitKey.Split(".")[^1];
        }
        string baseFolderPath = Path.Combine(folderpath.Split(_splitKey)[0] + _splitKey);
        return baseFolderPath;
    }
    
    internal static void SetBaseFolder(string baseFolder) => _baseFolder = baseFolder;
    
    public static string TraverseUp(this string path, int levels = 1)
    {
        string traverseString = "";
        for (int i = 0; i < levels; i++)
        {
            traverseString += PathingTraverseUp;
        }
        return Path.GetFullPath(Path.Combine(path, traverseString));
    }
    
    /// <summary>
    /// Returns the path with platform specific path seperators
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string EnsurePathFormat(this string path) 
        => pathSplitCharacters.Aggregate(path, (current, splitChar) => current.Replace(splitChar, PathSeperator));
    
    /// <summary>
    /// Gets the MD5 hash for a file, Checks to ensure path is correct for the platform and that the file exists
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetMD5HashForFile(this string fileName)
    {
        try
        {
            var updatedPath = fileName.EnsurePathFormat();
            if (updatedPath.DoesNotExistAsPath())
            {
                return string.Empty;
            }
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(updatedPath);
            byte[]? hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return string.Empty;
        }
    }
    
    
    /// <summary>
    /// Extension method that calls <see cref="File.Exists"/>
    /// </summary>
    /// <param name="path">The path to check as a string</param>
    /// <returns>true if the file was not found, false if the file was found</returns>
    public static bool DoesNotExistAsPath(this string? path) => !File.Exists(path);
    
    /// <summary>
    /// Extension method that calls <see cref="File.Exists"/>
    /// Returns true if the file was found, false if the file was not found
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool ExistsAsPath(this string? path) => File.Exists(path);
}