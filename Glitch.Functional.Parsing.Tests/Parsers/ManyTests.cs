using FluentAssertions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Tests.Parsers;

using static Parse;

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
        result.Should().Be(text);
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
        result.IsOkay.Should().BeTrue();
        result.Remaining.Position.Should().Be(0);
        result.Remaining.ReadToEnd().Should().BeOfType<string>()
              .Which.Should().Be(text);
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
        result.Should().Be(text);
    }

    [Fact]
    public void AtLeastOnce_OneMatch_Succeeds_OnlyConsumesAndReturnsMatch()
    {
        // Arrange
        var text = "A1 Steak Sauce";

        var parser = Letter.AtLeastOnce();

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsOkay.Should().BeTrue();
        result.Remaining.ReadToEnd().Should().BeOfType<string>()
              .Which.Should().BeEquivalentTo(text[1..]);
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
        result.IsOkay.Should().BeFalse();
    }

    [Fact]
    public void ZeroOrMoreTimes_Succeeds_ReturnsItems()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Letter.ZeroOrMoreTimes();

        // Act
        var result = parser.Parse(text);

        // Assert
        result.Should().Be("Look");
    }

    [Fact]
    public void ZeroOrMoreTimes_Fails_SucceedsWithEmptyEnumerable()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Digit.ZeroOrMoreTimes();

        // Act
        var result = parser.Parse(text);

        // Assert
        result.Should().Be(string.Empty);
    }

    [Fact]
    public void Once_Succeeds_ReturnsSingletonCollection()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Letter.Once();

        // Act
        var result = parser.Parse(text);

        // Assert
        result.Should().Be("L");
    }

    [Fact]
    public void Once_SucceedsMoreThanOnce_SucceedsWithSingletonCollection_DoesNotConsumeFurther()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Letter.Once();

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val.Should().Be("L");
        result.Remaining.ReadToEnd()
              .Should().BeOfType<string>()
              .Which.Should().Be(text[1..]);
    }

    [Fact]
    public void Once_Fails_ReturnsError()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Digit.Once();

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Be("Unexpected 'L'. Expected: digit");
    }

    [Fact]
    public void AtLeast_Fails_ReturnsError()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Digit.AtLeast(4);

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().EndWith("Expected: digit at least 4 times");
    }

    [Fact]
    public void AtLeast_SucceedsLessThanSpecifiedTimes_Fails()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Letter.AtLeast(5);

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        
        // TODO Add "found only 4" to the end. That will require differentiating between Unexpected labels and Found labels,
        // meaning we'll need to subtype ParseError instead of using an enum tag.
        err!.Message.Should().EndWith("Expected: letter at least 5 times"); 
    }

    [Fact]
    public void AtLeast_SucceedsMoreThanCount_ReturnsAllMatches()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Letter.AtLeast(2);

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().Be("Look");
    }

    [Fact]
    public void AtMost_NoMatches_ReturnsEmptyCollection()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Digit.AtMost(4);

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().Be(string.Empty);
    }

    [Fact]
    public void AtMost_SucceedsLessThanCount_ReturnsAllMatches()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Letter.AtMost(6);

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().Be("Look");
    }

    [Fact]
    public void AtMost_SucceedsMoreThanCount_Fails()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Letter.Or(Char(',')).Or(Space).AtMost(4);

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsFail(out var err).Should().BeTrue();

        // TODO Pending adding labels or expectations to successful results
        // err!.Message.Should().Be("Expected letter, ',', or space only 4 times, found 7");
        err!.Message.Should().EndWith("no more than 4 times");
    }

    [Fact]
    public void Times_MatchesExactlyCount_Succeeds()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Letter.Times(4);

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().Be("Look");
    }

    [Fact]
    public void Times_MatchesLessThanCount_Fails()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Letter.Times(6);

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Be("Unexpected ','. Expected: letter exactly 6 times");
    }

    [Fact]
    public void Times_MatchesMoreThanCount_Fails()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = Letter.Times(2);

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().EndWith("exactly 2 times");

        // TODO Get labels into successful results so they can be used in error messages when the counts are wrong
        // err!.Message.Should().Be("Expected letter 2 times, found 4");
    }

    [Fact]
    public void Until_StopSucceeds_ConsumesStop_ReturnsAllSuccesses()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = AnyChar.Until(Literal("Howdy"));

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().Be("Look, I'm Woody! ");

        result.Remaining.ReadToEnd()
              .Should().BeOfType<string>() // TODO Send a nastygram to FluentAssertions for failing an IEnumerable<char> assertion because it's a string.
                                           // A string *is* an IEnumerable<char>, you morons. That's like saying "I was expecting one hundred dollars, this is a $100 bill." -_-
              .Which.Should().Be(" howdy howdy howdy.");
    }

    [Fact]
    public void Until_StopNeverSucceeds_ContinuesToEndOfInput_ReturnsAllSuccesses()
    {
        // Arrange
        var text = "Look, I'm Woody! Howdy howdy howdy howdy.";
        var parser = AnyChar.Until(Digit);

        // Act
        var result = parser.Execute(text);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().Be(text);
        result.Remaining.IsEnd.Should().BeTrue();
    }
}
