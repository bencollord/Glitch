using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, TResult> Select<TResult>(Func<T, TResult> selector) => source.BiSelect(selector, FN.Identity);

        public IParser<TToken, T> SelectError(Func<ParseError, ParseError> selector) => source.BiSelect(FN.Identity, selector);

        public IParser<TToken, TResult> BiSelect<TResult>(Func<T, TResult> okay, Func<ParseError, ParseError> fail) =>
            source.Match(ok => ParseResult<TToken>.Okay(okay(ok.Value), ok.Remaining),
                         err => ParseResult<TToken>.Error<TResult>(fail(err.Error), err.Remaining));

        public IParser<TToken, TResult> Apply<TResult>(IParser<TToken, Func<T, TResult>> apply) => source.Then(x => apply.Select(y => y(x)));

        public IParser<TToken, TResult> Return<TResult>(TResult value) => source.Then(Parse<TToken>.Return(value));

        public IParser<TToken, Unit> Discard() => source.Select(_ => Unit.Value);
    }
}