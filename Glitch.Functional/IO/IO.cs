using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public abstract partial class IO<T>
    {
        public IO<TResult> Select<TResult>(Func<T, TResult> map) => AndThen(x => new ReturnIO<TResult>(map(x)));

        public IO<TResult> AndThen<TResult>(Func<T, IO<TResult>> bind) => AndThen(bind, (_, p) => p);

        public IO<TResult> AndThen<TElement, TResult>(Func<T, IO<TElement>> bind, Func<T, TElement, TResult> project) => new ContinueIO<T, TElement, TResult>(this, bind, project);

        public IO<T> Or(IO<T> other) => OrElse(_ => other);

        public IO<T> OrElse(Func<Error, IO<T>> bind) => new CatchIO<T, Error>(this, bind);

        public IO<T> Guard(Func<T, bool> predicate, Func<T, Error> error) => new GuardIO<T>(this, predicate, error);

        public IO<TResult> Match<TResult>(Func<T, TResult> okay, Func<Error, TResult> error)
            => Select(okay).Catch(error);

        public IO<Unit> Match(Action<T> okay, Action<Error> error)
            => Match(okay.Return(), error.Return());

        public IO<T> Catch<TError>(Func<TError, T> map) => new CatchIO<T, TError>(this, map.Then(Return));

        public abstract T Run(IOEnv env);

        public IO<TResult> Cast<TResult>() => Select(DynamicCast<TResult>.From);

        public IO<T> Do(Action<T> action)
            => Select(v =>
            {
                action(v);
                return v;
            });

        public static implicit operator IO<T>(T value) => Return(value);
        public static implicit operator IO<T>(Error error) => Fail(error);

        public static IO<T> operator |(IO<T> x, T y) => x | Return(y);
        public static IO<T> operator |(IO<T> x, Error y) => x | Fail(y);
        public static IO<T> operator |(IO<T> x, IO<T> y) => x.Or(y);

        public static IO<T> operator >>(IO<T> x, IO<T> y) => x.AndThen(_ => y);
        public static IO<T> operator >>(IO<T> x, IO<Unit> y) => x.AndThen(v => y.Select(_ => v));
    }
}