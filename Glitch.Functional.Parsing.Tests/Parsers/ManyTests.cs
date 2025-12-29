using FluentAssertions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glitch.Functional.Parsing.Tests.Parsers;

using static Parse;

public class ManyTests
{
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
        var parser = Digit.Once();

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
        result.Remaining.ReadToEnd().Should().BeEquivalentTo(text[1..]);
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
        err!.Message.Should().Be("Expected: digit");
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
        err!.Message.Should().Be("Expected: digit");
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
        err!.Message.Should().Be("Expected: letter at least 5 times, found only 4");
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
        err!.Message.Should().Be("Expected letter, ',', or space only 4 times, found 7");
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
        err!.Message.Should().Be("Expected letter 6 times, found 4");
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
        err!.Message.Should().Be("Expected letter 2 times, found 4");
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
        result.Remaining.ReadToEnd().Should().BeEquivalentTo(" howdy howdy howdy.");
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
