using FluentAssertions;
using HelpfulTypesAndExtensions;

namespace HelpFulTypesAndExtensions.Tests.Unit;


public class TestsForTasks
{
    private class TestClass
    {
        public TestClass()
        {
            string hello = "hello";
            Task MyTask = Task.Run(async() =>
            {
                Console.WriteLine(hello);
                throw new Exception("Test Exception triggered");
            });

            //can't await in a constructor
            //await MyTask;

            MyTask.FireAndForget(onError: (ex) => Console.WriteLine(ex.Message));
        }
    }
    
    
    [Test]
    public async Task TestConstructorTask()
    {
        //arrange
        //act
        var test = new TestClass();
        await Task.Delay(1000);
        //assert
        //no assert, just make sure it doesn't throw an exception
        test.Should().NotBeNull();
        
    }
    
    
}