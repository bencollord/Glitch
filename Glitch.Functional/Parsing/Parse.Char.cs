using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static TokenParser<char> AnyChar => Any<char>();

    public static TokenParser<char> Letter => Char(char.IsLetter).WithLabel("letter");
    
    public static TokenParser<char> Digit => Char(char.IsDigit).WithLabel("digit");
    
    public static TokenParser<char> LetterOrDigit => Char(char.IsLetterOrDigit).WithLabel("letter or digit");
    
    public static TokenParser<char> Whitespace => Char(char.IsWhiteSpace).WithLabel("whitespace");

    public static TokenParser<char> Tab => Char('\t');
    
    public static TokenParser<char> Space => Char(' ');

    public static Parser<char, string> LineBreak => from cr in Char('\r').Maybe()
                                                    from lf in Char('\n')
                                                    select Environment.NewLine;

    public static Parser<char, Unit> SkipWhitespace => Whitespace.ZeroOrMoreTimes().IgnoreResult();

    public static TokenParser<char> Char(char c) => Satisfy(x => x == c, Expectation.Expected(c));

    public static TokenParser<char> Char(Func<char, bool> predicate) => Satisfy(predicate);

    public static TokenParser<char> OneOf(string chars) => OneOf(chars.AsEnumerable());
}
