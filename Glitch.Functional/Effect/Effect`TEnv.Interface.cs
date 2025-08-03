using System.Collections.Immutable;

namespace Glitch.Functional
{
    public partial class Effect<TInput, T> : IEffect<TInput, T>
    {
        IEffect<TInput, TResult> IEffect<TInput, T>.AndThen<TResult>(Func<T, IEffect<TInput, TResult>> bind)
            => new Effect<TInput, TResult>(i => thunk(i).AndThen(x => bind(x).Run(i)));

        IEffect<TInput, T> IEffect<TInput, T>.Catch<TException>(Func<TException, T> map) => Catch(map);

        IEffect<TInput, T> IEffect<TInput, T>.Guard(Func<T, bool> predicate, Func<T, Error> error) => Guard(predicate, error);

        IEffect<TInput, TResult> IEffect<TInput, T>.Map<TResult>(Func<T, TResult> map) => Map(map);

        IEffect<TInput, T> IEffect<TInput, T>.MapError(Func<Error, Error> map) => MapError(map);

        IEffect<TInput, TResult> IEffect<TInput, T>.Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> ifFail) => Match(ifOkay, ifFail);

        IResult<T, Error> IEffect<TInput, T>.Run(TInput input) => Run(input);
    }
}
