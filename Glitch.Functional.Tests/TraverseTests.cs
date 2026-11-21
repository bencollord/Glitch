using Glitch.Functional;
using Glitch.Functional.Extensions.Traverse;

namespace Glitch.Test.Functional;

public class TraverseTests
{
    [Fact]
    public void Expected_IfOneFails_AllFail()
    {
        // Arrange
        var successfulResults = Enumerable.Range(1, 10).Select(Result.Okay<int, string>).ToList();

        var failedResult = Result.Fail<int, string>("Bad result");

        // Act
        successfulResults.Add(failedResult);

        Result<IEnumerable<int>, string> result = successfulResults.Traverse();

        // Assert
        Assert.True(result.IsFail);
        Assert.Equal("Bad result", result.UnwrapError());
    }
}
