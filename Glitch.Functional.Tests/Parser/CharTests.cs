using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using static Glitch.Functional.Option;
using static Glitch.Functional.Parsing.Parse;

namespace Glitch.Functional.Tests.Parser;

public class CharTests
{
    [Fact]
    public void Char_Succeeds_ConsumesOneChar()
    {
        // Arrange
        var text = new CharSequence("Test");
        var parser = Char('T');

        // Act
        var result = parser.Execute(text);

        // Assert
        var ok = Assert.IsType<ParseSuccess<char, char>>(result);
        Assert.Equal('T', ok.Value);
        Assert.True(result.Remaining.ReadToEnd().SequenceEqual("est"));
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
        var error = Assert.IsType<ParseError<char, char>>(result);

        Assert.True(result.Remaining.ReadToEnd().SequenceEqual("Test"));
        Assert.Equal(Some('T'), error.Expectation.Unexpected);
        Assert.Contains('C', error.Expectation.Expected);
    }
}
