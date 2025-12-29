using FluentAssertions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Tests.Parsers;

using static Parse;

public class CombinatorTests
{
    [Fact]
    public void Then_FirstSucceeds_NextSucceeds_Succeeds()
    {
        // Arrange
        var input = "# My heading";
        var parser = Char('#').Then(Space).Then(Char('M'));

        // Act
        var result = parser.Parse(input);

        // Assert
        result.Should().Be('M');
    }

    [Fact]
    public void Then_FirstFails_Fails()
    {
        // Arrange
        var input = "# My heading";
        var parser = Char('X').Then(Space).Then(Char('M'));

        // Act
        var result = parser.Execute(input);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Be("Unexpected '#'. Expected: 'X'");
    }

    [Fact]
    public void Then_FirstSucceeds_NextFails_Fails()
    {
        // Arrange
        var input = "# My heading";
        var parser = Char('#').Then(Space).Then(Char('X'));

        // Act
        var result = parser.Execute(input);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Be("Unexpected 'M'. Expected: 'X'");
    }

    [Fact]
    public void Before_OtherSucceeds_Succeeds()
    {
        // Arrange
        var input = "# My heading";
        var parser = Char('#').Before(Space);

        // Act
        var result = parser.Execute(input);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val.Should().Be('#');
    }

    [Fact]
    public void Before_OtherFails_Fails()
    {
        // Arrange
        var input = "Johnathan";
        var parser = Literal("John").Before(Literal("son"));

        // Act
        var result = parser.Execute(input);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Be("Unexpected 'a'. Expected: son");
    }

    [Fact]
    public void After_OtherSucceeds_Succeeds()
    {
        // Arrange
        var input = "Johnathan";
        var parser = Literal("nathan").After(Literal("Joh"));

        // Act
        var result = parser.Execute(input);

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val!.Should().Be("nathan");
    }

    [Fact]
    public void After_OtherFails_Fails()
    {
        // Arrange
        var input = "Johnathan";
        var parser = Literal("nathan").After(Literal("Jon"));

        // Act
        var result = parser.Execute(input);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Be("Unexpected 'h'. Expected: Jon");
    }

    [Fact]
    public void Before_MatchComesBeforeToken_Succeeds()
    {
        // Arrange
        var csv = "Alpha,";

        var parser = Letter.AtLeastOnce()
                           .Before(',');
        // Act
        var result = parser.Parse(csv);

        // Assert
        result.Should().Be("Alpha");
    }

    [Fact]
    public void Before_NoMatchForOther_Fails()
    {
        // Arrange
        var csv = "Alpha";

        var parser = Letter.AtLeastOnce()
                           .Before(',');
        // Act
        var result = parser.Execute(csv);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Contain("Expected: ','");
    }

    [Fact]
    public void After_MatchComesAfterToken_Succeeds()
    {
        // Arrange
        var csv = ",Bravo";

        var parser = Letter.AtLeastOnce()
                           .After(',');
        // Act
        var result = parser.Parse(csv);

        // Assert
        result.Should().Be("Bravo");
    }

    [Fact]
    public void After_NoMatchForOther_Fails()
    {
        // Arrange
        var csv = "Bravo";

        var parser = Letter.AtLeastOnce()
                           .Before(',');
        // Act
        var result = parser.Execute(csv);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Contain("Expected: ','");
    }

    [Fact]
    public void Between_LeftFails_Fails()
    {
        // Arrange
        var html = "<strong>My name is Slim Shady</strong>";

        var left = Literal("<p><strong>");
        var right = Literal("</strong>");

        var parser = Literal("My name is Slim Shady").Between(left, right);

        // Act
        var result = parser.Execute(html);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Be("Unexpected 's'. Expected: <p><strong>");
    }

    [Fact]
    public void Between_RightFails_Fails()
    {
        // Arrange
        var html = "<strong>My name is Slim Shady</strong>";

        var left = Literal("<strong>");
        var right = Literal("</p></strong>");

        var parser = Literal("My name is Slim Shady").Between(left, right);

        // Act
        var result = parser.Execute(html);

        // Assert
        result.IsFail(out var err).Should().BeTrue();
        err!.Message.Should().Be("Unexpected 's'. Expected: </p></strong>");
    }

    [Fact]
    public void Between_BothSucceed_Succeeds()
    {
        // Arrange
        var html = "<strong>My name is Slim Shady</strong>";

        var left = Literal("<strong>");
        var right = Literal("</strong>");

        var parser = Literal("My name is Slim Shady").Between(left, right);

        // Act
        var result = parser.Parse(html);

        // Assert
        result.Should().Be("My name is Slim Shady");
    }

    [Fact]
    public void Not_NegatedParserFails_Succeeds()
    {
        // Arrange
        var text = "Hi, my name is... what?";

        var parser = Literal("Hello").Not().Return("Good");

        // Act
        var result = parser.Parse(text);

        // Assert
        result.Should().Be("Good");
    }

    [Fact]
    public void Not_NegatedParserSucceeds_Fails()
    {
        // Arrange
        var text = "Hi, my name is... what?";

        var parser = Literal("Hi").Not().Return("Good");

        // Act
        var result = parser.Execute(text);

        // Assert
        result.Should().BeOfType<ParseFailure<char, string>>()
              .Which.Error.Message.Should().Be("Unexpected 'Hi'. Expected: Anything else");
    }

    [Fact]
    public void Except_ExceptedParserFails_Succeeds()
    {
        // Arrange
        var text = "Hi, my name is... what?";

        var parser = LetterOrDigit.Except(Char('G'));

        // Act
        var result = parser.Execute(text);

        // Assert
        result.Should().BeOfType<ParseSuccess<char, char>>()
              .Which.Value.Should().Be('H');
    }

    [Fact]
    public void Except_ExceptedParserSucceeds_Fails()
    {
        // Arrange
        var text = "Hi, my name is... what?";

        var parser = LetterOrDigit.Except(Char('H'));

        // Act
        var result = parser.Execute(text);

        // Assert
        result.Should().BeOfType<ParseFailure<char, char>>()
              .Which.Error.Message.Should().Be("Unexpected 'H'. Expected: Anything else");
    }

    [Fact]
    public void Or_LeftSucceeds_Succeeds()
    {
        // Arrange
        var greeting = "Hello";

        var parser = Literal("Hello") | Literal("Hi");

        // Act
        var result = parser.Parse(greeting);

        // Assert
        result.Should().Be("Hello");
    }

    [Fact]
    public void Or_RightSucceeds_Succeeds()
    {
        // Arrange
        var greeting = "Hi";

        var parser = Literal("Hello") | Literal("Hi");

        // Act
        var result = parser.Parse(greeting);

        // Assert
        result.Should().Be("Hi");
    }

    [Fact]
    public void Or_BothFail_Fails()
    {
        // Arrange
        var greeting = "Good evening";

        var parser = Literal("Hello") | Literal("Hi");

        // Act
        var result = parser.Execute(greeting);

        // Assert
        result.Should().BeOfType<ParseFailure<char, string>>()
              .Which.Error.Message.Should().Be("Expected: Hello or Hi");
    }
}
