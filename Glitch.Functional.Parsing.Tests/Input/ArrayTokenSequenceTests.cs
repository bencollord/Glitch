using FluentAssertions;
using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing.Tests.Input;

/// <summary>
/// Tests for <see cref="ArrayTokenSequence{TToken}"/>.
/// </summary>
/// <remarks>
/// Tests are done against a <see cref="ByteSequence"/>, which is just
/// an <see cref="ArrayTokenSequence{byte}"/> with a different ToString display.
/// </remarks>
public class ArrayTokenSequenceTests
{
    [Fact]
    public void Current_HasNotAdvanced_ReturnsFirstToken()
    {
        // Arrange/Act
        var tokens = ByteSequence.From([0xD, 0xE, 0xA, 0xD, 0xB, 0xE, 0xE, 0xF]);

        // Assert
        tokens.Current.Should().Be(0xD);
    }

    [Fact]
    public void Advance_NoArgument_AdvancesOnePosition()
    {
        // Arrange
        byte[] source = [0xD, 0xE, 0xA, 0xD, 0xB, 0xE, 0xE, 0xF];

        TokenSequence<byte> tokens = ByteSequence.From(source);

        // Act
        tokens = tokens.Advance();

        // Assert
        tokens.Current.Should().Be(0xE);
    }

    [Fact]
    public void Advance_NotAtEnd_ReturnsCurrentToken()
    {
        // Arrange
        byte[] source = [0xD, 0xE, 0xA, 0xD, 0xB, 0xE, 0xE, 0xF];

        TokenSequence<byte> tokens = ByteSequence.From(source);

        // Act
        tokens = tokens.Advance(3);

        // Assert
        tokens.Current.Should().Be(0xD);
    }

    [Fact]
    public void Advance_ToEnd_ReturnsDefaultValue()
    {
        // Arrange
        byte[] source = [0xD, 0xE, 0xA, 0xD, 0xB, 0xE, 0xE, 0xF];

        TokenSequence<byte> tokens = ByteSequence.From(source);

        // Act
        tokens = tokens.Advance(source.Length);

        // Assert
        tokens.IsEnd.Should().BeTrue();
        tokens.Current.Should().Be(default);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Lookahead_WithinBounds_ReturnsCorrectTokens(int count)
    {
        // Arrange
        byte[] source = [0xD, 0xE, 0xA, 0xD, 0xB, 0xE, 0xE, 0xF];

        TokenSequence<byte> tokens = ByteSequence.From(source);

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
    public void Lookback_WithinBounds_ReturnsCorrectTokens(int count)
    {
        // Arrange
        byte[] source = [0xD, 0xE, 0xA, 0xD, 0xB, 0xE, 0xE, 0xF];

        TokenSequence<byte> tokens = ByteSequence.From(source).Advance(count);

        // Act
        var span = tokens.Lookback(count);

        // Assert
        for (int i = 0; i < count; i++)
        {
            span[i].Should().Be(source[i]);
        }
    }

    [Fact]
    public void Lookback_PastBounds_ReturnsTokensToBeginning()
    {
        // Arrange
        byte[] source = [0xD, 0xE, 0xA, 0xD, 0xB, 0xE, 0xE, 0xF];

        TokenSequence<byte> tokens = ByteSequence.From(source).Advance(3);

        // Act
        var span = tokens.Lookback(5);

        // Assert
        span.Length.Should().Be(3);
        span[0].Should().Be(0xD);
        span[1].Should().Be(0xE);
        span[2].Should().Be(0xA);
    }

    [Fact]
    public void Lookahead_PastBounds_ReturnsTokensToEnd()
    {
        // Arrange
        byte[] source = [0xD, 0xE, 0xA, 0xD, 0xB, 0xE, 0xE, 0xF];

        TokenSequence<byte> tokens = ByteSequence.From(source).Advance(3);

        // Act
        var span = tokens.Lookahead(5);

        // Assert
        span.Length.Should().Be(4);
        span[0].Should().Be(0xB);
        span[1].Should().Be(0xE);
        span[2].Should().Be(0xE);
        span[3].Should().Be(0xF);
    }

    [Fact]
    public void ReadToEnd_ReturnsRemainingTokens_IncludingCurrent_AsArray()
    {
        // Arrange
        byte[] source = [0xD, 0xE, 0xA, 0xD, 0xB, 0xE, 0xE, 0xF];

        TokenSequence<byte> tokens = ByteSequence.From(source).Advance(3);

        // Act
        var remaining = tokens.ReadToEnd();

        // Assert
        remaining.Should().HaveCount(5)
                 .And.BeEquivalentTo([0xD, 0xB, 0xE, 0xE, 0xF]);
    }
}
