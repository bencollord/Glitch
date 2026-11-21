using FluentAssertions;
using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Test.Functional;

using static Result;

public class ResultTests
{
    [Fact]
    public void Unwrap_Failed_ShouldThrowContainedException()
    {
        // Arrange
        Result<int, KeyNotFoundException> item = Fail(new KeyNotFoundException("The key wasn't found"));

        // Act/Assert
        item.Invoking(ex => ex.Unwrap())
            .Should().Throw<KeyNotFoundException>()
            .WithMessage("The key wasn't found");
    }

    [Fact]
    public void ZipWith_BothResultsOkay_ShouldApplyFunction()
    {
        // Arrange
        Result<int, string> left  = Okay(10);
        Result<int, string> right = Okay(20);

        // Act
        var result = left.Zip(right, (x, y) => x + y);

        // Assert
        Assert.True(result.IsOkay);
        Assert.Equal(30, result.Unwrap());
    }

    [Fact]
    public void ZipWith_OneResultFailed_ShouldReturnErroredResult()
    {
        // Arrange
        Result<int, string> okay = Okay(10);
        Result<int, string> leftError = Fail("Left failed");
        Result<int, string> rightError = Fail("Right failed");

        // Act
        var leftResult = leftError.Zip(okay, (x, y) => x + y);
        var rightResult = okay.Zip(rightError, (x, y) => x + y);

        // Assert
        Assert.False(leftResult.IsOkay);
        Assert.False(rightResult.IsOkay);
        Assert.Equal("Left failed", leftResult.UnwrapError());
        Assert.Equal("Right failed", rightResult.UnwrapError());
    }
}
