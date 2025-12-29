using FluentAssertions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Tests.Parsers;

using static Parse;

public class SeparatedTests
{
    [Fact]
    public void SeparatedBy_WithStringSeparator_Succeeds()
    {
        // Arrange
        var separator = ", ";
        var csv = "Foo, Bar, Baz";

        var parser = Letter.AtLeastOnce()
                           .SeparatedBy(Literal(separator))
                           .AtLeastOnce();

        // Act
        var result = parser.Parse(csv);

        // Assert
        result.Should().BeEquivalentTo(csv.Split(separator));
    }

    [Fact]
    public void SeparatedBy_WithSpecificNumberOfTimes_Succeeds_WhenCountMatches()
    {
        // Arrange
        var separator = ", ";
        var csv = "Foo, Bar, Baz";

        var parser = Letter.AtLeastOnce()
                           .SeparatedBy(Literal(separator))
                           .Times(3);

        // Act
        var result = parser.Parse(csv).ToArray();

        // Assert
        result[0].Should().Be("Foo");
        result[1].Should().Be("Bar");
        result[2].Should().Be("Baz");
        result.Length.Should().Be(3);
    }

    [Fact]
    public void ZeroOrMoreTimes_Succeeds_ReturnsItems()
    {
        // Arrange
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";

        var parser = Letter.AtLeastOnce()
                           .SeparatedBy(Char(','))
                           .ZeroOrMoreTimes();

        // Act
        var words = parser.Parse(csv);

        // Assert
        words.Should().BeEquivalentTo(csv.Split(','));
    }

    [Fact]
    public void ZeroOrMoreTimes_Fails_SucceedsWithEmptyEnumerable()
    {
        // Arrange
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";

        var parser = Digit.AtLeastOnce()
                          .SeparatedBy(Char(','))
                          .ZeroOrMoreTimes();

        // Act
        var words = parser.Parse(csv);

        // Assert
        words.Should().BeEquivalentTo([]);
    }

    [Fact]
    public void AtLeast_Fails_ReturnsError()
    {
        // Arrange
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";

        var parser = Digit.AtLeastOnce()
                          .SeparatedBy(Char(','))
                          .AtLeast(4);

        // Act
        var result = parser.Execute(csv);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().EndWith("Expected: digit at least 4 times");
    }

    [Fact]
    public void AtLeast_SucceedsLessThanSpecifiedTimes_ReturnsError()
    {
        // Arrange
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";

        var parser = Letter.AtLeastOnce()
                           .SeparatedBy(Char(','))
                           .AtLeast(7);

        // Act
        var result = parser.Execute(csv);

        // Assert
        result.IsFail(out var err).Should().BeTrue();

        // TODO Add "found only 4" to the end. That will require differentiating between Unexpected labels and Found labels,
        // meaning we'll need to subtype ParseError instead of using an enum tag.
        // err!.Message.Should().EndWith("Expected: letter at least 7 times");

        // Stopgap. This at least tells us the behavior is expected even if it's not my ideal.
        err!.Message.Should().Be("Unexpected 'End of input'");
    }

    [Fact]
    public void AtLeast_SucceedsMoreThanCount_ReturnsAllMatches()
    {
        // Arrange
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";

        var parser = Letter.AtLeastOnce()
                           .SeparatedBy(Char(','))
                           .AtLeast(4);

        // Act
        var result = parser.Execute(csv);
        
        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().BeEquivalentTo(csv.Split(','));
    }

    [Fact]
    public void AtMost_NoMatches_ReturnsEmptyCollection()
    {
        // Arrange
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";
        var parser = Digit.AtLeastOnce()
                          .SeparatedBy(Char(','))
                          .AtMost(4);

        // Act
        var result = parser.Execute(csv);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().BeEquivalentTo([]);
    }

    [Fact]
    public void AtMost_SucceedsLessThanCount_ReturnsAllMatches()
    {
        // Arrange
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";
        var parser = Letter.AtLeastOnce()
                           .SeparatedBy(Char(','))
                           .AtMost(6);

        // Act
        var result = parser.Execute(csv);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().BeEquivalentTo(csv.Split(','));
    }

    [Fact]
    public void AtMost_SucceedsMoreThanCount_Fails()
    {
        // Arrange
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";
        var parser = Letter.AtLeastOnce()
                           .SeparatedBy(Char(','))
                           .AtMost(4);

        // Act
        var result = parser.Execute(csv);

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
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";
        var parser = Letter.AtLeastOnce()
                           .SeparatedBy(Char(','))
                           .Times(5);

        // Act
        var result = parser.Execute(csv);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().BeEquivalentTo(csv.Split(','));
    }

    [Fact]
    public void Times_MatchesLessThanCount_Fails()
    {
        // Arrange
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";
        var parser = Letter.AtLeastOnce()
                           .SeparatedBy(Char(','))
                           .Times(6);

        // Act
        var result = parser.Execute(csv);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Be("Unexpected 'End of input'");
        // TODO Propagate labels of successful results for error hanlding
        // err!.Message.Should().Be("Unexpected 'End of input'. Expected: letter exactly 6 times");
    }

    [Fact]
    public void Times_MatchesMoreThanCount_Fails()
    {
        // Arrange
        var csv = "Alpha,Bravo,Charlie,Delta,Echo";
        var parser = Letter.AtLeastOnce()
                           .SeparatedBy(Char(','))
                           .Times(2);

        // Act
        var result = parser.Execute(csv);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().EndWith("exactly 2 times");

        // TODO Get labels into successful results so they can be used in error messages when the counts are wrong
        // err!.Message.Should().Be("Expected letter 2 times, found 4");
    }
}
