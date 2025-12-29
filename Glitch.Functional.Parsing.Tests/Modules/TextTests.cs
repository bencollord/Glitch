using FluentAssertions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glitch.Functional.Parsing.Tests.Modules;

using static Parse;

public class TextTests
{
    [Fact]
    public void AnyChar_Succeeds()
    {
        // Arrange
        var parser = AnyChar;

        // Act
        var result = parser.Parse("Test");

        // Assert
        result.Should().Be('T');
    }

    [Theory]
    [InlineData("Test")]
    [InlineData("1122")]
    public void LetterOrDigit_IsLetterOrDigit_Succeeds(string value)
    {
        // Arrange
        var parser = LetterOrDigit;

        // Act
        var result = parser.Parse(value);

        // Assert
        result.Should().Be(value[0]);
    }

    [Fact]
    public void LetterOrDigit_NonLetterOrDigit_Fails()
    {
        // Arrange
        var parser = LetterOrDigit;

        // Act
        var result = parser.Execute("***");

        // Assert
        result.Should().BeOfType<ParseFailure<char, char>>()
              .Which.Error.Message.Should().Be("Unexpected '*'. Expected: letter or digit");
    }

    [Theory]
    [InlineData(' ')]
    [InlineData('\t')]
    public void NonBreakingSpace_TabOrSpace_Succeeds(char character)
    {
        // Arrange
        var parser = NonBreakingSpace;

        // Act
        var result = parser.Execute([character]);

        // Assert
        result.Should().BeOfType<ParseSuccess<char, char>>()
              .Which.Value.Should().Be(character);
    }

    [Theory]
    [InlineData('\r')]
    [InlineData('\n')]
    public void NonBreakingSpace_LineBreak_Fails(char character)
    {
        // Arrange
        var parser = NonBreakingSpace;

        // Act
        var result = parser.Execute([character]);

        // Assert
        result.Should().BeOfType<ParseFailure<char, char>>()
              .Which.Error.Message.Should().Be($"Unexpected '{character}'. Expected: non-breaking space");
    }

    [Fact]
    public void Literal_ExactMatch_Succeeds()
    {
        // Arrange
        var text = "Hello there";
        var parser = Literal("Hello there");

        // Act
        var result = parser.Parse(text);

        // Assert
        result.Should().Be(text);
    }

    [Fact]
    public void Literal_NonExactMatch_Fails()
    {
        // Arrange
        var text = "hello there";
        var parser = Literal("Hello there");

        // Act
        var result = parser.Execute(text);

        // Assert
        result.Should().BeOfType<ParseFailure<char, string>>()
              .Which.Error.Message.Should().Be("Unexpected 'h'. Expected: Hello there");
    }
}
