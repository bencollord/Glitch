using FluentAssertions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Tests.Results;

public class ParseErrorTests
{
    [Fact]
    public void Message_DisplaysLabelAsMessage()
    {
        // Arrange
        var message = "Invalid type";

        // Act
        var error = ParseError.New(message);

        // Assert
        error.Kind.Should().Be(ParseErrorKind.Message);
        error.Label.Should().Be(message);
        error.Message.Should().Be(message);
    }

    [Fact]
    public void Message_EmptyString_ThrowsArgumentNullException()
    {
        // Arrange/Act
        var exception = Assert.Throws<ArgumentNullException>(() => ParseError.New(""));

        // Assert
        exception.ParamName.Should().Be("label");
        exception.Message.Should().StartWith($"Label is required for {nameof(ParseErrorKind)}.{ParseErrorKind.Message}");
    }

    [Fact]
    public void Unexpected_NoExpectations_DisplaysUnexpectedTokenAsMessage()
    {
        // Arrange
        var message = "Unexpected 'C'";

        // Act
        var error = ParseError.Unexpected('C');

        // Assert
        error.Kind.Should().Be(ParseErrorKind.Unexpected);
        error.Message.Should().Be(message);
    }

    [Fact]
    public void Unexpected_WithExpectations_DisplaysLabelAsUnexpected_AndExpectationsAsMessage()
    {
        // Arrange
        var message = "Unexpected 'C'. Expected: 'A' or 'B'";

        // Act
        var error = ParseError.Unexpected('C', ["A", "B"]);

        // Assert
        error.Kind.Should().Be(ParseErrorKind.Unexpected);
        error.Message.Should().Be(message);
    }

    [Fact]
    public void Expected_WithLabel_DisplaysLabelAsUnexpected_AndExpectationsAsMessage()
    {
        // Arrange
        var message = "Unexpected 'C'. Expected: 'A' or 'B'";

        // Act
        var error = ParseError.Expected(["A", "B"], found: "C");

        // Assert
        error.Kind.Should().Be(ParseErrorKind.Expected);
        error.Message.Should().Be(message);
    }

    [Fact]
    public void Expected_WithoutLabel_DisplaysExpectationsAsMessage()
    {
        // Arrange
        var message = "Expected: 'A' or 'B'";

        // Act
        var error = ParseError.Expected("A", "B");

        // Assert
        error.Kind.Should().Be(ParseErrorKind.Expected);
        error.Message.Should().Be(message);
    }

    [Fact]
    public void Expected_EmptyArray_ThrowsArgumentException()
    {
        // Arrange/Act
        var exception = Assert.Throws<ArgumentException>(() => ParseError.Expected([]));

        // Assert
        exception.ParamName.Should().Be("expectations");
        exception.Message.Should().StartWith($"At least one expectation is required for {nameof(ParseErrorKind)}.{ParseErrorKind.Expected}");
    }
}
