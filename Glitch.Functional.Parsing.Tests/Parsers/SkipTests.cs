using FluentAssertions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Tests.Parsers;

using static Parse;

public class SkipTests
{
    [Fact]
    public void Skip_HasMatch_SkipsMatch()
    {
        // Arrange
        var text = "Show me the money! Fine. $1000";
        var nan = Letter | Space | OneOf("!.$");
        var parser = nan.ZeroOrMoreTimes().Skip();

        // Act
        var result = parser.Execute(text);

        // Assert
        result.Should().BeOfType<ParseSuccess<char, Unit>>()
              .Which.Remaining.ReadToEnd()
              .Should().BeOfType<string>()
              .Which.Should().Be("1000");
    }

    [Fact]
    public void Skip_NoMatch_NoOp()
    {
        // Arrange
        var text = "Show me the money! Fine. $1000";
        var parser = Digit.ZeroOrMoreTimes().Skip();

        // Act
        var result = parser.Execute(text);

        // Assert
        result.Should().BeOfType<ParseSuccess<char, Unit>>()
              .Which.Remaining.ReadToEnd()
              .Should().BeOfType<string>()
              .Which.Should().Be(text);
    }

    [Fact]
    public void SkipUntil_FindsMatch_SkipsAllItems()
    {
        // Arrange
        var text = "Show me the money, d00d! Fine. $1000";
        var nan = Digit | Letter | Space | OneOf("!.,");
        var parser = nan.AtLeastOnce().SkipUntil(Char('$'));

        // Act
        var result = parser.Execute(text);

        // Assert
        result.Should().BeOfType<ParseSuccess<char, Unit>>()
              .Which.Remaining.ReadToEnd()
              .Should().BeOfType<string>()
              .Which.Should().Be("1000");
    }

    [Fact]
    public void SkipUntil_NoMatch_ConsumesUntilEndOfInput()
    {
        // Arrange
        var text = "Show me the money, d00d! Fine. $1000";
        var nan = Digit | Letter | Space | OneOf("!.,$");
        var parser = nan.AtLeastOnce().SkipUntil(Literal("Not even real"));

        // Act
        var result = parser.Execute(text);

        // Assert
        result.Should().BeOfType<ParseSuccess<char, Unit>>()
              .Which.Remaining.IsEnd
              .Should().BeTrue();
    }
}
