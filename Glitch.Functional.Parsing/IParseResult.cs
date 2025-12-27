using System.Collections.Immutable;

namespace Glitch.Functional.Parsing;

public interface IParseResult<TToken, out T>
{
    bool IsOkay { get; }

    // TODO Replace these two fields with ParseState
    ITokenSequence<TToken> Remaining { get; init; }
    ImmutableArray<string> Expectations { get; init; }

    IParseResult<TToken, TResult> Select<TResult>(Func<T, TResult> map);

    virtual IParseResult<TToken, TResult> Cast<TResult>() => Select(DynamicCast<TResult>.From);

    // UNDONE
    //public abstract TResult Match<TResult>(Func<ParseSuccess<TToken, T>, TResult> okay, Func<ParseError<TToken, T>, TResult> fail);
}
