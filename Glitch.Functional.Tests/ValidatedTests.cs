using FluentAssertions;
using Glitch.Functional;
using Glitch.Functional.Validation;

namespace Glitch.Test.Functional;

using static Validated;

public class ValidatedTests
{
    [Fact]
    public void ZipWith_BothResultsOkay_ShouldApplyFunction()
    {
        // Arrange
        var left  = Okay<int, string>(10);
        var right = Okay<int, string>(20);

        // Act
        var result = left.Zip(right, (x, y) => x + y);

        // Assert
        result.IsOkay.Should().BeTrue();
        result.Unwrap().Should().Be(30);
    }

    [Fact]
    public void And_BothResultsFailed_ShouldHaveBothErrors()
    {
        // Arrange
        var left = Fail<int, string>("Left failed");
        var right = Fail<int, string>("Right failed");

        // Act
        var result = left.And(right);

        // Assert
        result.Should().BeOfType<Validated<int, string>.Fail>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Left failed", "Right failed"]);
    }

    [Fact]
    public void And_LeftSucceeds_RightFails_ShouldReturnRightFail()
    {
        // Arrange
        var left  = Okay<int, string>(22);
        var right = Fail<int, string>("Right failed");

        // Act
        var result = left.And(right);

        // Assert
        result.Should().BeOfType<Validated<int, string>.Fail>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Right failed"]);
    }

    [Fact]
    public void And_LeftFails_RightSucceeds_ShouldReturnLeftFailure()
    {
        // Arrange
        var left = Fail<int, string>("Left failed");
        var right = Okay<int, string>(22);

        // Act
        var result = left.And(right);

        // Assert
        result.Should().BeOfType<Validated<int, string>.Fail>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Left failed"]);
    }

    [Fact]
    public void And_BothSucceed_ShouldReturnRight()
    {
        // Arrange
        var left = Okay<int, string>(22);
        var right = Okay<int, string>(44);

        // Act
        var result = left.And(right);

        // Assert
        result.Should().BeOfType<Validated<int, string>.Okay>()
              .Which.Value.Should()
              .Be(44);
    }

    [Fact]
    public void Or_BothResultsFailed_ShouldHaveBothErrors()
    {
        // Arrange
        var left  = Fail<int, string>("Left failed");
        var right = Fail<int, string>("Right failed");

        // Act
        var result = left.Or(right);

        // Assert
        result.Should().BeOfType<Validated<int, string>.Fail>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Left failed", "Right failed"]);
    }

    [Fact]
    public void OrElse_FunctionFails_ShouldHaveBothErrors()
    {
        // Arrange
        var left = Fail<int, string>("Left failed");

        // Act
        var result = left.OrElse(e => Fail<int, string>("Right failed"));

        // Assert
        result.Should().BeOfType<Validated<int, string>.Fail>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Left failed", "Right failed"]);
    }

    [Fact]
    public void Zip_BothResultsFailed_ShouldContainBothErrors()
    {
        // Arrange
        var okay = Okay<int, string>(10);
        var leftError = Fail<int, string>("Left failed");
        var rightError = Fail<int, string>("Right failed");

        // Act
        var result = okay
            .Zip(leftError, (x, y) => x + y)
            .Zip(rightError, (x, y) => x + y);

        // Assert
        result.IsFail.Should().BeTrue();
        result.IsOkay.Should().BeFalse();

        result.Should().BeOfType<Validated<int, string>.Fail>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Left failed", "Right failed"]);
    }
}
