using Glitch.Functional.Parsing;
using Glitch.Functional.Parsing.Results;
using static Glitch.Functional.Parsing.Parse;

namespace Glitch.Test.Functional;

public class ManyTests
{
    [Fact]
    public void ZeroOrMoreTimes_MultipleMatches_ReturnsAllMatches()
    {
        // Arrange
        var text = "Alpha";

        var parser = Letter.ZeroOrMoreTimes();

        // Act
        var result = parser.Parse(text);

        // Assert
        Assert.True(result.SequenceEqual(text));
    }

    [Fact]
    public void ZeroOrMoreTimes_NoMatches_Succeeds_WithoutConsumingInput()
    {
        // Arrange
        var text = "Alpha";

        var parser = Digit.ZeroOrMoreTimes();

        // Act
        var result = parser.Execute(text);

        // Assert
        Assert.True(result.IsOkay);
        Assert.Equal(0, result.Remaining.Position);
        Assert.True(result.Remaining.ReadToEnd().SequenceEqual(text));
    }

    [Fact]
    public void AtLeastOnce_MultipleMatches_ReturnsAllMatches()
    {
        // Arrange
        var text = "Alpha";

        var parser = Letter.AtLeastOnce();

        // Act
        var result = parser.Parse(text);

        // Assert
        Assert.True(result.SequenceEqual(text));
    }

    [Fact]
    public void AtLeastOnce_OneMatch_Succeeds_OnlyConsumesAndReturnsMatch()
    {
        // Arrange
        var text = "A1 Steak Sauce";

        var parser = Letter.AtLeastOnce().AsString();

        // Act
        var result = parser.Execute(text);

        // Assert
        Assert.True(result.IsOkay);
        Assert.Equal(text[1..], result.Remaining.ReadToEnd());
    }

    [Fact]
    public void AtLeastOnce_NoMatches_Fails()
    {
        // Arrange
        var text = "Alpha";

        var parser = Digit.AtLeastOnce();

        // Act
        var result = parser.Execute(text);

        // Assert
        Assert.False(result.IsOkay);
        Assert.Equal("digit", result.Expectation.Label);
    }

    [Fact]
    public void Times_MatchCount_MatchesExactly_Succeeds()
    {
        // Arrange
        var text = "Alpha,";

        var parser = Letter.Times(5).AsString();

        // Act
        var result = parser.Execute(text);

        // Assert
        var ok = Assert.IsType<ParseSuccess<char, string>>(result);

        Assert.Equal("Alpha", ok.Value);
        Assert.Equal(',', ok.Remaining.ReadToEnd().Single());
    }

    [Fact]
    public void Times_MatchCount_TooLow_Fails()
    {
        // Arrange
        var text = "Alpha,";

        var parser = Letter.Times(6).AsString();

        // Act
        var result = parser.Execute(text);

        // Assert
        var error = Assert.IsType<ParseError<char, string>>(result);

        // TODO Expectation check
    }

    [Fact]
    public void Times_MatchCount_TooHigh_Fails()
    {
        // Arrange
        var text = "Alpha,";

        var parser = Letter.Times(4).AsString();

        // Act
        var result = parser.Execute(text);

        // Assert
        var error = Assert.IsType<ParseError<char, string>>(result);

        // TODO Expectation check
    }
}
