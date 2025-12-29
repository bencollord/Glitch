using Glitch.Functional.Parsing.Legacy;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

/// <summary>
/// Module for text parsing.
/// </summary>
public partial class Parse
{
    public static ITokenParser<char> AnyChar => Parse<char>.Any;

    public static ITokenParser<char> Letter => Char(char.IsLetter).WithLabel("letter");
    
    public static ITokenParser<char> Digit => Char(char.IsDigit).WithLabel("digit");
    
    public static ITokenParser<char> LetterOrDigit => Char(char.IsLetterOrDigit).WithLabel("letter or digit");
    
    public static ITokenParser<char> Whitespace => Char(char.IsWhiteSpace).WithLabel("whitespace");

    public static IParser<char, string> Numeric => Digit.AtLeastOnce();

    public static IParser<char, string> Hex
        => Digit.Or(OneOf("ABCDEF")) // TODO Case-insensitive
                .Or(OneOf("abcdef"))
                .AtLeastOnce()
                .After(Literal("0x").Maybe());

    public static ITokenParser<char> Tab => Char('\t');
    
    public static ITokenParser<char> Space => Char(' ');

    public static IParser<char, char> NonBreakingSpace => Char(c => char.IsWhiteSpace(c) && !Environment.NewLine.Contains(c), "non-breaking space");

    public static IParser<char, string> NonBreakingSpaces => NonBreakingSpace.AtLeastOnce();

    public static IParser<char, string> LineBreak => from cr in Char('\r').Maybe()
                                                     from lf in Char('\n')
                                                     select Environment.NewLine;

    public static IParser<char, Unit> SkipWhitespace => Whitespace.SkipAll();

    public static ITokenParser<char> Char(char c) => Parse<char>.Token(c);

    public static ITokenParser<char> Char(Func<char, bool> predicate) => Parse<char>.Satisfy(predicate);

    public static ITokenParser<char> Char(Func<char, bool> predicate, string label) => Parse<char>.Satisfy(predicate, label);

    public static ITokenParser<char> OneOf(string chars) => OneOf(chars.AsEnumerable());

    public static ITokenParser<char> OneOf(params IEnumerable<char> chars) => Char(chars.Contains, $"One of {chars.Select(c => $"'{c}'").Join(", ")}"); // DESIGN This error message will differ from other OneOf methods

    public static IParser<char, string> Literal(string text)
    {
        return text.Select(Char)
                   .PipeInto(Sequence)
                   .Select(chars => new string([.. chars]))
                   .WithExpected(text);
    }
}
