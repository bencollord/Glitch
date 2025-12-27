using FluentAssertions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glitch.Functional.Parsing.Tests.Parse;

public class SatisfyTests
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
}
