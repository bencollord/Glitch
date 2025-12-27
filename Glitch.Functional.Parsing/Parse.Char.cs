namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static ITokenParser<char> AnyChar => Any<char>();

    public static ITokenParser<char> Letter => Char(char.IsLetter).WithLabel("letter");
    
    public static ITokenParser<char> Digit => Char(char.IsDigit).WithLabel("digit");
    
    public static ITokenParser<char> LetterOrDigit => Char(char.IsLetterOrDigit).WithLabel("letter or digit");
    
    public static ITokenParser<char> Whitespace => Char(char.IsWhiteSpace).WithLabel("whitespace");

    public static ITokenParser<char> Tab => Char('\t');
    
    public static ITokenParser<char> SingleSpace => Char(' ');

    public static IParser<char, char> NonBreakingSpace => Char(c => char.IsWhiteSpace(c) && !Environment.NewLine.Contains(c));

    //public static IParser<char, string> NonBreakingSpaces => NonBreakingSpace.AtLeastOnce().AsString();

    //public static IParser<char, string> LineBreak => from cr in Char('\r').Maybe()
    //                                                 from lf in Char('\n')
    //                                                 select Environment.NewLine;

    //public static IParser<char, Unit> EndOfInput =>
    //    from state in State<char>()
    //    from check in state.IsEnd
    //                ? Parser<char>.Return(Unit.Value)
    //                : Parser<char>.Fail<Unit>("Expected EOF")
    //    select Unit.Value;

    //public static IParser<char, Unit> SkipWhitespace => Whitespace.ZeroOrMoreTimes().IgnoreResult();

    public static ITokenParser<char> Char(char c) => Token(c);

    public static ITokenParser<char> Char(Func<char, bool> predicate) => Satisfy(predicate);

    //public static ITokenParser<char> OneOf(string chars) => OneOf(chars.AsEnumerable());
}
