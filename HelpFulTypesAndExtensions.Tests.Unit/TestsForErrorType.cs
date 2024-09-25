using FluentAssertions;
using HelpfulTypesAndExtensions;

namespace HelpFulTypesAndExtensions.Tests.Unit;

public class TestsForErrorType
{
    [Test]
    public void TestErrorType()
    {
        var error = new ClientErrors.ValidationFailureError();
        error.GetName().Should().Be("Validation Failure");
    }
    
    
}
