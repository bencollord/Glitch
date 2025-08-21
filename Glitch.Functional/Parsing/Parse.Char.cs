using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<char, char> AnyChar => Any<char>();

        public static Parser<char, char> Letter => Char(char.IsLetter).WithLabel("letter");
        
        public static Parser<char, char> Digit => Char(char.IsDigit).WithLabel("digit");
        
        public static Parser<char, char> LetterOrDigit => Char(char.IsLetterOrDigit).WithLabel("letter or digit");
        
        public static Parser<char, char> Whitespace => Char(char.IsWhiteSpace).WithLabel("whitespace");

        public static Parser<char, string> LineBreak => from cr in Char('\r').Maybe()
                                                        from lf in Char('\n')
                                                        select Environment.NewLine;
        public static Parser<char, char> Char(char c) => Satisfy(x => x == c, Expectation.Expected(c));

        public static Parser<char, char> Char(Func<char, bool> predicate) => Satisfy(predicate);

        public static Parser<char, char> OneOf(string chars) => OneOf(chars.AsEnumerable());
    }
}
