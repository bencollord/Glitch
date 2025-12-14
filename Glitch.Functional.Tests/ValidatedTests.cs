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
        result.HasValue.Should().BeTrue();
        result.Unwrap().Should().Be(30);
    }

    [Fact]
    public void And_BothResultsFailed_ShouldHaveBothErrors()
    {
        // Arrange
        var left = Fatal<int, string>("Left failed");
        var right = Fatal<int, string>("Right failed");

        // Act
        var result = left.And(right);

        // Assert
        result.Should().BeOfType<Validated<int, string>.Fatal>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Left failed", "Right failed"]);
    }

    [Fact]
    public void And_LeftSucceeds_RightFails_ShouldReturnRightFail()
    {
        // Arrange
        var left  = Okay<int, string>(22);
        var right = Fatal<int, string>("Right failed");

        // Act
        var result = left.And(right);

        // Assert
        result.Should().BeOfType<Validated<int, string>.Fatal>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Right failed"]);
    }

    [Fact]
    public void And_LeftFails_RightSucceeds_ShouldReturnLeftFailure()
    {
        // Arrange
        var left = Fatal<int, string>("Left failed");
        var right = Okay<int, string>(22);

        // Act
        var result = left.And(right);

        // Assert
        result.Should().BeOfType<Validated<int, string>.Fatal>()
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
        var left  = Fatal<int, string>("Left failed");
        var right = Fatal<int, string>("Right failed");

        // Act
        var result = left.Or(right);

        // Assert
        result.Should().BeOfType<Validated<int, string>.Fatal>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Left failed", "Right failed"]);
    }

    [Fact]
    public void OrElse_FunctionFails_ShouldHaveBothErrors()
    {
        // Arrange
        var left = Fatal<int, string>("Left failed");

        // Act
        var result = left.OrElse(e => Fatal<int, string>("Right failed"));

        // Assert
        result.Should().BeOfType<Validated<int, string>.Fatal>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Left failed", "Right failed"]);
    }

    [Fact]
    public void Zip_BothResultsFailed_ShouldContainBothErrors()
    {
        // Arrange
        var okay = Okay<int, string>(10);
        var leftError = Fatal<int, string>("Left failed");
        var rightError = Fatal<int, string>("Right failed");

        // Act
        var result = okay
            .Zip(leftError, (x, y) => x + y)
            .Zip(rightError, (x, y) => x + y);

        // Assert
        result.HasError.Should().BeTrue();
        result.HasValue.Should().BeFalse();

        result.Should().BeOfType<Validated<int, string>.Fatal>()
              .Which.Errors.Should()
              .BeEquivalentTo(["Left failed", "Right failed"]);
    }
}
