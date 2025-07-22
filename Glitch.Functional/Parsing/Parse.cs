using System.Collections.Immutable;
using System.Globalization;

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

        public static Parser<char, string> Numeric => Digit.AtLeastOnce().Token();

        public static Parser<char, int> Int
           => Digit.AtLeastOnce().Token().Map(int.Parse);

        public static Parser<char, decimal> Decimal
            => from whole in Numeric
               let format = CultureInfo.CurrentCulture.NumberFormat
               from fractional in Literal(format.NumberDecimalSeparator)
                   .Then(Numeric)
                   .Maybe()
               let token = fractional.Match(
                   ifSome: f => whole + f,
                   ifNone: whole)
               select decimal.Parse(token);

        public static Parser<char, TEnum> Enum<TEnum>()
            where TEnum : struct, Enum
            => OneOf(System.Enum.GetNames<TEnum>().Select(Literal))
                   .Map(System.Enum.Parse<TEnum>);

        public static Parser<char, char> Char(char c) => Char(x => x == c).WithLabel(c.ToString());

        public static Parser<char, char> Char(Func<char, bool> predicate)
            => new Parser<char, char>(input =>
                input.IsEnd
                    ? ParseResult.Fail<char, char>("End of input reached")
                    : ParseResult.Okay(input.Current, input.Advance())
                                 .Guard(predicate, Expectation.Unexpected(input.Current))
            );

        public static Parser<char, string> Literal(string text)
        {
            return text.Select(Char)
                       .PipeInto(Sequence)
                       .Map(chars => new string([.. chars]));
        }

        public static Parser<TToken, T> OneOf<TToken, T>(params IEnumerable<Parser<TToken, T>> parsers)
            => new(input =>
            {
                var results = parsers.Select(p => p.Execute(input));

                return results.FirstOrNone(p => p.WasSuccessful).IfNone(results.First());
            });

        public static Parser<TToken, IEnumerable<TToken>> Sequence<TToken>(IEnumerable<Parser<TToken, TToken>> parsers)
        {
            return parsers.Aggregate(
                Parser<TToken, ImmutableList<TToken>>.Return(ImmutableList<TToken>.Empty),
                (list, item) => list.Then(_ => item, (lst, i) => lst.Add(i)),
                list => list.Map(l => l.AsEnumerable()));
        }
    }
}
