using System.Collections.Immutable;

namespace Glitch.Functional
{
    public partial class Effect<TEnv, T> : IEffect<TEnv, T>
    {
        IEffect<TEnv, TResult> IEffect<TEnv, T>.AndThen<TResult>(Func<T, IEffect<TEnv, TResult>> bind)
            => new Effect<TEnv, TResult>(i => thunk(i).AndThen(x => bind(x).Run(i)));

        IEffect<TEnv, T> IEffect<TEnv, T>.Catch<TException>(Func<TException, T> map) => Catch(map);

        IEffect<TEnv, T> IGuardable<IEffect<TEnv, T>, T, Error>.Guard(Func<T, bool> predicate, Func<T, Error> error) => Guard(predicate, error);

        IEffect<TEnv, TResult> IEffect<TEnv, T>.Map<TResult>(Func<T, TResult> map) => Map(map);

        IEffect<TEnv, T> IEffect<TEnv, T>.MapError(Func<Error, Error> map) => MapError(map);

        IEffect<TEnv, TResult> IEffect<TEnv, T>.Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> ifFail) => Match(ifOkay, ifFail);

        Result<T, Error> IEffect<TEnv, T>.Run(TEnv input) => Run(input);
    }
}
