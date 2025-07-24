namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<char, char> Letter => Char(char.IsLetter).WithLabel("letter");
        public static Parser<char, char> Digit => Char(char.IsDigit).WithLabel("digit");
        public static Parser<char, char> LetterOrDigit => Char(char.IsLetterOrDigit).WithLabel("letter or digit");
        public static Parser<char, char> Whitespace => Char(char.IsWhiteSpace).WithLabel("whitespace");

        public static Parser<char, string> LineBreak => from cr in Char('\r').Maybe()
                                                        from lf in Char('\n')
                                                        select Environment.NewLine;
        public static Parser<char, char> Char(char c) => Char(x => x == c).WithExpected(c);

        public static Parser<char, char> Char(Func<char, bool> predicate) => Satisfy(predicate);

        public static Parser<char, char> OneOf(string chars) => OneOf(chars.AsEnumerable());

        public static Parser<char, string> Literal(string text)
        {
            return text.Select(Char)
                       .PipeInto(Sequence)
                       .Map(chars => new string([.. chars]));
        }
    }
}
