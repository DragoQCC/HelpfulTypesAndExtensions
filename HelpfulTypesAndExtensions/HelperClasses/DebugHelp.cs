using System.Diagnostics;

namespace HelpfulTypesAndExtensions;

public static class DebugHelp
{
    public static logLevel LogLevel = logLevel.Info;
    
    public static void SetLogLevel(logLevel logLevel)
    {
        LogLevel = logLevel;
    }
    
    [Conditional("DEBUG")]
    public static void DebugWriteLine(string message, logLevel logLevel = logLevel.Info)
    {
        if(logLevel < LogLevel)
        {
            return;
        }
        Console.WriteLine(message);
    }

    
    [Conditional("DEBUG")]
    public static void DebugLog(string message, string file, logLevel logLevel = logLevel.Info)
    {
        if(logLevel < LogLevel)
        {
            return;
        }
        string updatedPath = file.EnsurePathFormat();
        if (updatedPath.DoesNotExistAsPath())
        {
            File.WriteAllText(updatedPath,message);
        }
        else
        {
            File.AppendAllText(updatedPath, message);
        }
    }
}


public enum logLevel
{
    Trace,
    Info,
    Debug,
    Warning,
    Error
}