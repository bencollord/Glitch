using FluentAssertions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Tests.Parsers;

using static Parse;

public class MaybeTests
{
    [Fact]
    public void SourceParser_Succeeds_ReturnsSome()
    {
        // Arrange
        var text = "This is a test";
        var parser = Literal("This").Maybe();

        // Act
        var result = parser.Parse(text);

        // Assert
        result.IsSome(out var val).Should().BeTrue();
        val.Should().Be("This");
    }

    [Fact]
    public void SourceParser_Fails_Succeeds_ReturnsNone()
    {
        // Arrange
        var text = "This is a test";
        var parser = Literal("That").Maybe();

        // Act
        var result = parser.Parse(text);

        // Assert
        result.IsSome.Should().BeFalse();
    }

    [Fact]
    public void IfNone_SourceSucceeds_ReturnsSource()
    {
        // Arrange
        var text = "This is a test";
        var parser = Literal("This").Maybe().IfNone("the other");

        // Act
        var result = parser.Parse(text);

        // Assert
        result.Should().Be("This");
    }

    [Fact]
    public void IfNone_SourceFails_ReturnsFallback()
    {
        // Arrange
        var text = "This is a test";
        var parser = Literal("That").Maybe().IfNone("the other");

        // Act
        var result = parser.Parse(text);

        // Assert
        result.Should().Be("the other");
    }
}
