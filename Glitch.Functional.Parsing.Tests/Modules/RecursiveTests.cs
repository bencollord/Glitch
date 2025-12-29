using FluentAssertions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glitch.Functional.Parsing.Tests.Modules;

using static Parse;

public class RecursiveTests
{
    abstract record Node;
    abstract record Text : Node;
    record Element(string Tag, Node Content) : Node;
    record LiteralText(string Value) : Text;
    record BoldText(Text Text) : Text;
    record ItalicText(Text Text) : Text;

    [Fact]
    public void Ref_MutualRefsBetweenParsers_Succeeds()
    {
        // Arrange
        IParser<char, Text> literal = LetterOrDigit.Or(Space).AtLeastOnce().Select(x => new LiteralText(x));
        IParser<char, Text> bold = null!;
        IParser<char, Text> italic = null!;
        IParser<char, Text> text = null!;

        bold = Ref(() => from __0 in Literal("<strong>")
                         from txt in text
                         from __1 in Literal("</strong>")
                         select new BoldText(txt));

        italic = Ref(() => from __0 in Literal("<em>")
                           from txt in text
                           from __1 in Literal("</em>")
                           select new ItalicText(txt));

        text = Ref(() => bold | italic | literal);

        var input = "<strong><em>This is emphasized text</em></strong>";

        // Act
        var result = text.Parse(input);

        // Assert
        result.Should().BeOfType<BoldText>()
              .Which.Text.Should().BeOfType<ItalicText>()
              .Which.Text.Should().BeOfType<LiteralText>()
              .Which.Value.Should().Be("This is emphasized text");
    }

    [Fact]
    public void Rec_SelfReferencingParser_Succeeds()
    {
        // Arrange
        var input = "<strong><em>This is emphasized text</em></strong>";

        var tagName = LetterOrDigit.AtLeastOnce();
        var startTag = Char('<') >> tagName >> Char('>').Discard();
        var endTag = Char('<') >> Char('/') >> tagName >> Char('>').Discard();
        var plainText = LetterOrDigit.Or(Space).AtLeastOnce().Select(x => new LiteralText(x) as Node);

        var element = Rec<Element>(elem =>
            from tag in startTag
            from con in plainText | elem
            from ___ in endTag
            select new Element(tag, con));

        // Act
        var result = element.Parse(input);

        // Assert
        result.Tag.Should().Be("strong");
        result.Content.Should().BeOfType<Element>()
              .And.Satisfy<Element>(elem =>
              {
                  elem.Tag.Should().Be("em");
                  elem.Content.Should().BeOfType<LiteralText>()
                      .Which.Value.Should().Be("This is emphasized text");
              });
    }
}
