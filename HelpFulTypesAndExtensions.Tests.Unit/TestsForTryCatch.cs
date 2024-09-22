using System.Diagnostics;
using FluentAssertions;
using HelpfulTypesAndExtensions;

namespace HelpFulTypesAndExtensions.Tests.Unit;

public class TestsForTryCatch
{
    [Test]
    public async Task TryCatchWithNoError()
    {
        //arrange
        bool actionExecuted = false;
        Action action = () => actionExecuted = true;
        
        //act
        var result = TryCatch.Try(action);
        
        //assert
        result.Should().BeTrue();
        actionExecuted.Should().BeTrue();
    }
    
    [Test]
    public void TryCatchWithError()
    {
        float result = 0;
        //arrange
        Func<Exception,float> onError = e =>
        {
            Console.WriteLine(e.Message);
            return -1;
        };
        
        //act
        result = TryCatch.Try(() => Divide(1,0), onError);
        
        //assert
        result.Should().Be(-1);
    }
    
    private float Divide(float a, float b)
    {
        if(b == 0)
        {
            throw new DivideByZeroException();
        }
        return a / b;
    }
}