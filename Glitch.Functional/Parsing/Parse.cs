using System.Collections.Immutable;

namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<char, char> Letter => Char(char.IsLetter).WithLabel("letter");
        public static Parser<char, char> Digit => Char(char.IsDigit).WithLabel("digit");
        public static Parser<char, char> Alphanumeric => Char(char.IsLetterOrDigit).WithLabel("alphanumeric");
        public static Parser<char, char> Whitespace => Char(char.IsWhiteSpace).WithLabel("whitespace");

        public static Parser<char, string> LineBreak => from cr in Char('\r').Maybe()
                                                        from lf in Char('\n')
                                                        select Environment.NewLine;

        public static Parser<char, char> Char(char c) => Char(x => x == c).WithLabel(c.ToString());

        public static Parser<char, char> Char(Func<char, bool> predicate)
            => new Parser<char, char>(input =>
                input.IsEnd
                    ? ParseResult.Fail<char, char>("End of input reached")
                    : ParseResult.Okay(input.Current, input.Advance())
                                 .Guard(predicate, $"Unexpected {input.Current}")
            );

        public static Parser<char, string> Literal(string text)
        {
            return text.Select(Char)
                       .PipeInto(Sequence)
                       .Map(chars => new string([.. chars]));
        }

        public static Parser<TToken, IEnumerable<TToken>> Sequence<TToken>(IEnumerable<Parser<TToken, TToken>> parsers)
        {
            return parsers.Aggregate(
                Parser<TToken, ImmutableList<TToken>>.Return(ImmutableList<TToken>.Empty),
                (list, item) => list.Then(_ => item, (lst, i) => lst.Add(i)),
                list => list.Map(l => l.AsEnumerable()));
        }
    }
}
