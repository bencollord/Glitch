using Glitch.Functional;
using Glitch.Functional.Collections;
using Glitch.Functional.Errors;
using Glitch.Functional.Extensions.Traverse;

namespace Glitch.Test.Functional;

public class TraverseTests
{
    [Fact]
    public void Result_IfOneFails_AllFail()
    {
        // Arrange
        var successfulResults = Sequence.Range(1, 10).Select(Result.Okay).ToList();

        var failedResult = Result.Fail<int>("Bad result");

        // Act
        successfulResults.Add(failedResult);

        Result<Sequence<int>> result = successfulResults.Traverse();

        // Assert
        Assert.True(result.IsFail);
        Assert.Equal("Bad result", result.UnwrapError().Message);
    }
}
