using FluentAssertions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Tests.Modules;

using static Parse;

// UNDONE
public class BclTests
{
    [Fact]
    public void Boolean_True_ReturnsTrue()
    {
        Boolean.Parse("true").Should().BeTrue();
    }

    [Fact]
    public void Boolean_False_ReturnsTrue()
    {
        Boolean.Parse("false").Should().BeFalse();
    }

    [Fact]
    public void Boolean_NotTrueOrFalse_Fails()
    {
        var result = Boolean.Execute("Not a bool");

        result.Should().BeOfType<ParseFailure<char, bool>>()
              .Which.Error.Message.Should().EndWith("Expected: true or false");
    }

    [Fact]
    public void Int_Decimal_ParsesInt()
    {
        // Arrange
        var text = "42";

        // Act
        var result = Int.Parse(text);

        // Assert
        result.Should().Be(42);
    }

    [Fact]
    public void Int_Hexadecimal_ParsesInt()
    {
        // Arrange
        var text = "0xBEEF";

        // Act
        var result = Int.Parse(text);

        // Assert
        result.Should().Be(0xBEEF);
    }

    [Fact]
    public void Int_Binary_ParsesInt()
    {
        // Arrange
        var text = "0b10011001";

        // Act
        var result = Int.Parse(text);

        // Assert
        result.Should().Be(0b10011001);
    }

    [Fact]
    public void Int_NotANumber_Fails()
    {
        var result = Int.Execute("NAN");

        result.Should().BeOfType<ParseFailure<char, int>>()
              .Which.Error.Message.Should().Contain("digit at least 1 times"); // UNDONE Better error messages for Many parsers
    }

    [Fact]
    public void Decimal_WholeNumber_ParsesValue()
    {
        // Arrange
        var text = "1234";

        // Act
        var result = Decimal.Parse(text);

        // Assert
        result.Should().Be(1234m);
    }

    [Fact]
    public void Decimal_WholeAndFractional_ParsesValue()
    {
        // Arrange
        var text = "1234.5678";

        // Act
        var result = Decimal.Parse(text);

        // Assert
        result.Should().Be(1234.5678m);
    }

    [Fact]
    public void Decimal_FractionalOnly_ParsesValue()
    {
        // Arrange
        var text = ".5678";

        // Act
        var result = Decimal.Parse(text);

        // Assert
        result.Should().Be(.5678m);
    }

    [Fact]
    public void Decimal_NotANumber_Fails()
    {
        // Arrange
        var text = "NAN";

        // Act
        var result = Decimal.Execute(text);

        // Assert
        result.Should().BeOfType<ParseFailure<char, decimal>>()
              
              // TODO Better error messaging for Many parser
              .Which.Error.Message.Should().Contain("Expected")
              .And.Contain("digit at least 1 times");
    }

    [Theory]
    [InlineData("Sunday", DayOfWeek.Sunday)]
    [InlineData("Monday", DayOfWeek.Monday)]
    [InlineData("Tuesday", DayOfWeek.Tuesday)]
    [InlineData("Wednesday", DayOfWeek.Wednesday)]
    [InlineData("Thursday", DayOfWeek.Thursday)]
    [InlineData("Friday", DayOfWeek.Friday)]
    [InlineData("Saturday", DayOfWeek.Saturday)]
    public void Enum_ValueIsDefined_Succeeds(string text, DayOfWeek expected)
    {
        var result = Enum<DayOfWeek>().Parse(text);

        result.Should().Be(expected);
    }

    [Fact]
    public void Enum_ValueIsNotDefined_Fails()
    {
        var expected = System.Enum.GetNames<DayOfWeek>()
            .Join(", ")
            .PipeInto(t => t.Insert(t.LastIndexOf(',') + 1, " or"));

        var result = Enum<DayOfWeek>().Execute("Sonntag");

        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Contain($"Expected: {expected}");
    }
}