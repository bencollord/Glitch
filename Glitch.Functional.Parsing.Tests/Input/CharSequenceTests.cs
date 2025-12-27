using FluentAssertions;
using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing.Tests.Input;

public class CharSequenceTests
{
    [Fact]
    public void Current_HasNotAdvanced_ReturnsFirstChar()
    {
        // Arrange
        var source = "## Markdown heading";

        // Act
        var tokens = CharSequence.From(source);

        // Assert
        tokens.Current.Should().Be('#');
    }

    [Fact]
    public void Advance_NoArgument_AdvancesOnePosition()
    {
        // Arrange
        var source = "ABCDEFG";
        var tokens = CharSequence.From(source);

        // Act
        tokens = tokens.Advance();

        // Assert
        tokens.Current.Should().Be('B');
    }

    [Fact]
    public void Advance_NotAtEnd_ReturnsCurrentChar()
    {
        // Arrange
        var source = "## Markdown heading";
        var tokens = CharSequence.From(source);

        // Act
        tokens = tokens.Advance(3);

        // Assert
        tokens.Current.Should().Be('M');
    }

    [Fact]
    public void Advance_ToEnd_ReturnsNullChar()
    {
        // Arrange
        var source = "## Markdown heading";
        var tokens = CharSequence.From(source);

        // Act
        tokens = tokens.Advance(source.Length);

        // Assert
        tokens.Current.Should().Be('\0');
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Lookahead_WithinBounds_ReturnsCorrectChars(int count)
    {
        // Arrange
        var source = "ABCDEFG";
        var tokens = CharSequence.From(source);

        // Act
        var span = tokens.Lookahead(count);

        // Assert
        for (int i = 0; i < count; i++)
        {
            span[i].Should().Be(source[i + 1]);
        }
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Lookback_WithinBounds_ReturnsCorrectChars(int count)
    {
        // Arrange
        var source = "ABCDEFG";
        var tokens = CharSequence.From(source).Advance(count);

        // Act
        var span = tokens.Lookback(count);

        // Assert
        for (int i = 0; i < count; i++)
        {
            span[i].Should().Be(source[i]);
        }
    }

    [Fact]
    public void Lookback_PastBounds_ReturnsCharsToBeginning()
    {
        // Arrange
        var source = "ABCDEFG";
        var tokens = CharSequence.From(source).Advance(3);

        // Act
        var span = tokens.Lookback(5);

        // Assert
        span.Length.Should().Be(3);
        span[0].Should().Be('A');
        span[1].Should().Be('B');
        span[2].Should().Be('C');
    }

    [Fact]
    public void Lookahead_PastBounds_ReturnsCharsToEnd()
    {
        // Arrange
        var source = "ABCDEFG";
        var tokens = CharSequence.From(source).Advance(3);

        // Act
        var span = tokens.Lookahead(5);

        // Assert
        span.Length.Should().Be(3);
        span[0].Should().Be('E');
        span[1].Should().Be('F');
        span[2].Should().Be('G');
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void ReadToEnd_ReturnsRemainingCharsAsString(int count)
    {
        // Arrange
        var source = "ABCDEFG";
        var tokens = CharSequence.From(source).Advance(count);

        // Act
        var remaining = tokens.ReadToEnd();

        // Assert
        remaining.Should().Be(source[count..]);
    }
}
