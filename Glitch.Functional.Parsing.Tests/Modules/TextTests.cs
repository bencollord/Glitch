using FluentAssertions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

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

    [Fact]
    public void AnyChar_ZeroOrMoreTimes_ReturnsAndConsumesWholeString()
    {
        // Arrange
        var text = "Foo, Bar, Baz";
        var parser = AnyChar.ZeroOrMoreTimes();

        // Act
        var result = parser.Execute(text);

        // Assert
        result.Should().BeOfType<ParseSuccess<char, string>>()
              .Which.Value.Should().Be(text);

        result.Remaining.IsEnd.Should().BeTrue();
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

    [Fact]
    public void Char_Succeeds_ConsumesOneChar()
    {
        // Arrange
        var text = new CharSequence("Test");
        var parser = Char('T');

        // Act
        var result = parser.Execute(text);

        // Assert
        var ok = result.Should().BeOfType<ParseSuccess<char, char>>()
                       .Which.Value.Should().Be('T');
        
        result.Remaining.ReadToEnd().Should().BeOfType<string>()
              .Which.Should().BeEquivalentTo("est");
    }

    [Fact]
    public void Char_Fails_DoesNotConsume()
    {
        // Arrange
        var text = new CharSequence("Test");
        var parser = Char('C');

        // Act
        var result = parser.Execute(text);

        // Assert
        result.Should().BeOfType<ParseFailure<char, char>>()
              .Which.Error.Message.Should().Be("Unexpected 'T'. Expected: 'C'");

        result.Remaining.ReadToEnd().Should().BeOfType<string>()
              .Which.Should().BeEquivalentTo("Test");
    }
}
