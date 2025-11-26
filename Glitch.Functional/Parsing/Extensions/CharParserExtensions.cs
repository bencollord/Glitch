using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Parsing;

public static partial class ParserExtensions
{
    public static ParseResult<char, T> Execute<T>(this Parser<char, T> parser, string input)
        => parser.Execute(new CharSequence(input));

    public static Expected<T> TryParse<T>(this Parser<char, T> parser, string input)
        => parser.TryParse(new CharSequence(input));

    public static T Parse<T>(this Parser<char, T> parser, string input)
        => parser.Parse(new CharSequence(input));
}
