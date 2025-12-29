using FluentAssertions;
using Glitch.Functional;
using Glitch.Functional.Parsing;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Tests.Modules;

public class TokenTests
{
    [Fact]
    public void Satisfy_MatchingToken_Succeeds()
    {
        // Arrange
        var input = ByteSequence.From([6, 7, 8]);

        var parser = Parse<byte>.Satisfy(b => b > 5);

        // Act
        var result = parser.Parse(input);

        // Assert
        result.Should().Be(6);
    }

    [Fact]
    public void Satisfy_NonMatchingToken_Fails()
    {
        // Arrange
        var input = ByteSequence.From([6, 7, 8]);

        var parser = Parse<byte>.Satisfy(b => b < 5);

        // Act
        var result = parser.Execute(input);

        // Assert
        result.Should().BeOfType<ParseFailure<byte, byte>>()
              .Which.Error.Should().BeEquivalentTo(ParseError.Unexpected(6));
    }

    [Fact]
    public void Token_MatchingToken_Succeeds()
    {
        // Arrange
        var input = ByteSequence.From([6, 7, 8]);
        var parser = Parse<byte>.Token(6);

        // Act
        var result = parser.Parse(input);

        // Assert
        result.Should().Be(6);
    }

    [Fact]
    public void Token_NonMatchingToken_Fails()
    {
        // Arrange
        var input = ByteSequence.From([6, 7, 8]);

        var parser = Parse<byte>.Token(4);

        // Act
        var result = parser.Execute(input);

        // Assert
        result.Should().BeOfType<ParseFailure<byte, byte>>()
              .Which.Error.Should().BeEquivalentTo(ParseError.Unexpected(6, [4]));
    }

    [Fact]
    public void OneOf_IsMatch_Succeeds()
    {
        // Arrange
        var input = "Dog";

        var parser = Parse.OneOf("ABCD");

        // Act
        var result = parser.Execute(input);

        // Assert
        result.Should().BeOfType<ParseSuccess<char, char>>()
              .Which.Value.Should().Be('D');
    }

    [Fact]
    public void OneOf_NoMatch_Fails()
    {
        // Arrange
        var input = "Dog";

        var parser = Parse.OneOf("Cat");

        // Act
        var result = parser.Execute(input);

        // Assert
        result.Should().BeOfType<ParseFailure<char, char>>()
              .Which.Error.Message.Should().Be("Unexpected 'D'. Expected: One of 'C', 'a', 't'");
    }
}
