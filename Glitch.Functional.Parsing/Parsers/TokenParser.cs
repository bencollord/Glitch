using Glitch.Functional.Extensions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class TokenParser<TToken> : ITokenParser<TToken>
{
    private Func<TToken, bool> predicate;
    private Option<string> label;

    internal TokenParser(Func<TToken, bool> predicate, Option<string> label)
    {
        this.predicate = predicate;
        this.label = label;
    }

    /// <inheritdoc />
    public ITokenParser<TToken> WithLabel(string label) => new TokenParser<TToken>(predicate, Option.Some(label).Except(string.IsNullOrEmpty));

    /// <inheritdoc />
    public ITokenParser<TToken> Except(TToken token) => WithPredicate(x => predicate(x) && !x!.Equals(token));

    /// <inheritdoc />
    public ITokenParser<TToken> Except(Func<TToken, bool> predicate) => WithPredicate(x => this.predicate(x) && !predicate(x));

    private TokenParser<TToken> WithPredicate(Func<TToken, bool> predicate) => new(predicate, label);

    /// <inheritdoc />
    public IParseResult<TToken, TToken> Execute(ITokenSequence<TToken> input)
    {
        return predicate(input.Current)
             ? ParseResult<TToken>.Okay(input.Current, input.Advance())
             : ParseResult<TToken>.Error<TToken>(ParseError.Unexpected(input.Current, label.Iterate()), input);
    }
}
