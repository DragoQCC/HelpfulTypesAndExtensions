using System;
using System.Threading.Tasks;
using FluentAssertions;
using HelpfulTypesAndExtensions;

namespace HelpFulTypesAndExtensions.Tests.Unit;

public class TestsForEmpty
{
    [Test]
    public async Task VoidMethodToFunc()
    {
       //arrange
       string arg = "hello";
       string expected = "hello";
        
        //act
        var consoleMethod = GetConsoleWrite();
        var result = consoleMethod(arg);
        string output = TestContext.Current.GetStandardOutput();
        
        //assert
        result.Should().BeOfType<Empty>();
        output.Should().Be(expected);
    }
    
    private Func<string, Empty> GetConsoleWrite()
        => Empty.FromVoidMethod((string message) => Console.WriteLine(message));
    
}
