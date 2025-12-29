using FluentAssertions;
using Glitch.Functional;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Tests.Parsers;

using static Parse;

public class SliceTests
{
    [Fact]
    public void Slice_ReturnsAllMatchedCharacters()
    {
        // Arrange
        var comment = "/* This is a comment */";

        var slash = Char('/');
        var star  = Char('*');

        var parser = Lexeme(slash >> star)
            >> LetterOrDigit.Or(Space).Until(Space >> star >> slash);

        // Act
        var result = parser.Slice((matches, value) => new { Text = new string(matches), Value = value })
                           .Parse(comment);

        // Assert
        result.Value.Should().Be("This is a comment");
        result.Text.Should().Be(comment);
    }
}
