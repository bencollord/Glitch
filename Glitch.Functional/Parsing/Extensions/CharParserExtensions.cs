using Glitch.Functional.Parsing.Legacy.Input;
using Glitch.Functional.Parsing.Legacy.Results;
using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Parsing.Legacy;

public static partial class ParserExtensions
{
    public static ParseResult<char, T> Execute<T>(this Parser<char, T> parser, string input)
        => parser.Execute(new CharSequence(input));

    public static Expected<T> TryParse<T>(this Parser<char, T> parser, string input)
        => parser.TryParse(new CharSequence(input));

    public static T Parse<T>(this Parser<char, T> parser, string input)
        => parser.Parse(new CharSequence(input));
}
