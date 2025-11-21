using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects
{
    public partial class Effect<TEnv, T> : IEffect<TEnv, T>
    {
        public IEffect<TEnv, TResult> AndThen<TResult>(Func<T, IEffect<TEnv, TResult>> bind)
            => new Effect<TEnv, TResult>(i => thunk(i).AndThen(x => bind(x).Run(i)));

        public IEffect<TEnv, TResult> AndThen<TElement, TResult>(Func<T, IEffect<TEnv, TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Select(project.Curry(x)));

        IEffect<TEnv, T> IEffect<TEnv, T>.Catch<TException>(Func<TException, T> map) => Catch(map);

        IEffect<TEnv, TResult> IEffect<TEnv, T>.Select<TResult>(Func<T, TResult> map) => Select(map);

        IEffect<TEnv, T> IEffect<TEnv, T>.SelectError(Func<Error, Error> map) => SelectError(map);

        IEffect<TEnv, TResult> IEffect<TEnv, T>.Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> ifFail) => Match(ifOkay, ifFail);
    }
}
