### A collection of useful Interfaces, Base Classes, Utilities, and Extension Methods for .NET

# Getting Started :bulb:

## Installation 
- `HelpfulTypesAndExtensions` is available as a [NuGet](https://www.nuget.org/packages/HelpfulTypesAndExtensions) package.
- The library targets `.NET Standard 2.0`, and `.NET 8` so it is compatible with a wide range of .NET projects.
- The library can be installed with `dotnet add package HelpfulTypesAndExtensions` or via the package manager in your IDE. 

# Included Types :gift:

## Types

### `TGuid` - A Timed Guid
- A Guid that includes a timestamp to allow for sorting and filtering by creation date.
- When created a `TGuid` will have the current tick count appended to it.
- The Resulting `TGuid` by default will be formatted in the format `Guid-TickCount`.

### `ObservableHashSet<T>` - A HashSet that implements INotifyCollectionChanged
- An observable hash set that implements INotifyCollectionChanged, INotifyPropertyChanged, INotifyPropertyChanging
- Exposes events for when items are added, removed, or the entire collection is modified.

### `ObsverableDictionary<TKey, TValue>` - A Dictionary that implements INotifyCollectionChanged
- An observable dictionary that implements INotifyCollectionChanged, INotifyPropertyChanged, INotifyPropertyChanging
- Exposes events for when items are added, removed, or the entire collection is modified.
- Events fire both for Keys and Values.

### `Enumeration<T>` - A record class that represents an enumeration
- Allows for creating enums that can have state and behavior and other typical class features
- Also allows for adding new enumeration values without requiring the typical painful refactoring of existing code
- Performance is faster than a typical enum 
- Read more about enumeration classes [here](https://josef.codes/enumeration-class-in-c-sharp-using-records/) and [here](https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/)

### `Empty` - A class that represents an empty value
- Void is not a real type in .NET, it cannot be used as a return type or a variable type
- Many other languages support the concepts of a `Unit` type that contains no data
- Makes chaining method calls, LINQ queries, and other operations easier where current void methods are used
- Can be used as a return type for methods that do not return a value or to represent a value that is not present
- Can convert a `void` method to a `Func<Empty>` method to allow for chaining and other operations
- Can also convert a `Task` method to a `Task<Empty>` method to allow for chaining and other operations

```csharp
public Empty DoSomething()
{
    var consoleMethod = GetConsoleWrite();
    return consoleMethod("Hello World");
}

private Func<string, Empty> GetConsoleWrite()
        => Empty.FromVoidMethod((string message) => Console.WriteLine(message));
```

### `BooleanExpression` - Wrapper Type for Expressions that return a boolean
- A wrapper type for expressions that return a boolean
- Allows for chaining expressions together and for creating complex boolean expressions

### `Errors` - A collection of classes that represent common error types
- Includes types for 
  - Client Errors
  - Server Errors
  - Validation Errors
  - Security Errors
  - Resource Errors
  - Network Errors
  - Custom Errors

```csharp

public class ResultORError<T>
{
    public T? Result { get; set; }
    public List<IError> Errors { get; set; } = new List<IError>();
}

void Method1()
{
    var resultOrError = Method2();
    if(resultOrError.Errors.Count > 0)
    {
        foreach(var error in resultOrError.Errors)
        {
            Console.WriteLine(error.Message);
        }
    }
    else
    {
        Console.WriteLine(resultOrError.Result);
    }
}

ResultORError<HttpMessage> Method2()
{
    HttpClient client = new HttpClient();
    var response = client.GetAsync("https://www.google.com").Result;
    if(response.IsSuccessStatusCode)
    {
        return new ResultORError<HttpMessage> { Result = response.Content };
    }
    else
    {
        return new ResultORError<HttpMessage> { Errors = [NetworkErrors.ConnectionFailure("Failed to connect to host")] };
    }
}
```

## Interfaces 
- IDatabaseService
- ISignalRClient
- ISignalRClientModel
- ISignalRHubModel
- IError


## Base Classes
- SignalRClient
- SignalRClientModel

## Utilities

### `DebugHelper` - A class that provides helper methods that only run in Debug mode
- Provides helper methods that only run in Debug mode using the `DEBUG` compiler directive
- Has methods for writing to the console, writing to a file, and customizing the log level

### `Pathing` - A class that provides helper methods for working with file paths
- Provides helper methods for working with file paths
- Has methods for: 
  - getting the base path of a project 
  - ensuring a path is formatted corrected to the OS
  - Traverse up a directory tree
  - If a string exists as a path or not
  - Getting a MD5 hash of a file

### `JSONSerilization` - A class that provides helper methods for serializing and deserializing JSON
- Uses the `System.Text.Json` library to serialize and deserialize JSON
- Provides methods for serializing and deserializing objects to and from strings and byte arrays
- Tries to validate and fix the incoming JSON where possible

### `TryCatch` - Provides helper methods for wrapping code in a try-catch blocks
- Provides helper methods for wrapping code in a try-catch block and handling the exceptions

```csharp

public void DoSomething()
{
    TryCatch.Try(() => 
    {
        // Code that may throw an exception
    },
    (Exception ex) => 
    {
        // Handle the exception
    });
}

public void DoSomething2()
{
    float result = 0;
   
    Func<Exception,float> onError = e =>
    {
        Console.WriteLine(e.Message); //Prints "Attempted to divide by zero."
        return -1;
    };

    result = TryCatch.Try(() => Divide(1,0), onError);
    result.Should().Be(-1); //true
}
```

## Extension Methods


### `ByteExtensions` - Provides extension methods for working with byte arrays
### `CollectionExtensions` - Provides extension methods for working with collections
### `ConfigurationExtensions` - Provides extension methods for working with the IConfiguration in ASP.NET Core
### `EmptyExtensions` - Provides extension methods for working with the Empty class
### `GenericExtensions` - Provides extension methods for working with generic types
### `StringExtensions` - Provides extension methods for working with strings
### `TaskExtensions` - Provides extension methods for working with Tasks
### `TGuidExtensions` - Provides extension methods for working with TGuids
### `TypeExtensions` - Provides extension methods for working with types
### `UriExtensions` - Provides extension methods for working with URIs


# Contributing
Feel free to submit a pull request if you have any useful types or extensions that you would like to add to this library.
An ideal contribution is a type or extension that is generally useful across a wide range of projects that you wish were included in .NET by default.    

# License
HelpfulTypesAndExtensions is licensed under the MIT License (i.e., Fully Open Source) - see the LICENSE file for details.