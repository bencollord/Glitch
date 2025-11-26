using Glitch.Functional;
using Glitch.Functional.Collections;
using Glitch.Functional.Errors;
using Glitch.Functional.Extensions.Traverse;

namespace Glitch.Test.Functional;

public class TraverseTests
{
    [Fact]
    public void Expected_IfOneFails_AllFail()
    {
        // Arrange
        var successfulResults = Sequence.Range(1, 10).Select(Expected.Okay).ToList();

        var failedResult = Expected.Fail<int>("Bad result");

        // Act
        successfulResults.Add(failedResult);

        Expected<Sequence<int>> result = successfulResults.Traverse();

        // Assert
        Assert.True(result.IsFail);
        Assert.Equal("Bad result", result.UnwrapError().Message);
    }
}
