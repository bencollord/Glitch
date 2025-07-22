using System.Collections.Immutable;

namespace Glitch.Functional
{
    public partial class Effect<TInput, TOutput> : IEffect<TInput, TOutput>
    {
        IEffect<TInput, TOutput> IEffect<TInput, TOutput>.Guard(Func<TOutput, bool> predicate, Func<TOutput, Error> error) => Guard(predicate, error);

        IEffect<TInput, TResult> IEffect<TInput, TOutput>.Map<TResult>(Func<TOutput, TResult> map) => Map(map);

        IEffect<TInput, TOutput> IEffect<TInput, TOutput>.MapError(Func<Error, Error> map) => MapError(map);

        IEffect<TInput, TResult> IEffect<TInput, TOutput>.Match<TResult>(Func<TOutput, TResult> ifOkay, Func<Error, TResult> ifFail) => Match(ifOkay, ifFail);

        IResult<TOutput, Error> IEffect<TInput, TOutput>.Run(TInput input) => Run(input);
    }
}
