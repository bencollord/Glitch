namespace Glitch.Functional
{
    public abstract partial class IO<TEnv, T> : IGuardable<IO<TEnv, T>, T, Error>
    {
        public IO<TNewEnv, T> With<TNewEnv>(Func<TNewEnv, TEnv> map) => new MapEnvIO<TNewEnv, TEnv, T>(this, map);

        public IO<TEnv, TResult> Select<TResult>(Func<T, TResult> map) => AndThen(x => new ReturnIO<TEnv, TResult>(map(x)));

        public IO<TEnv, TResult> AndThen<TResult>(Func<T, IO<TEnv, TResult>> bind) => AndThen(bind, (_, p) => p);

        public IO<TEnv, TResult> AndThen<TElement, TResult>(Func<T, IO<TEnv, TElement>> bind, Func<T, TElement, TResult> project) => new ContinueIO<TEnv, T, TElement, TResult>(this, bind, project);

        public IO<TEnv, T> Or(IO<TEnv, T> other) => OrElse(_ => other);
        public IO<TEnv, T> OrElse(Func<Error, IO<TEnv, T>> bind) => new CatchIO<TEnv, T, Error>(this, bind);

        public IO<TEnv, T> Guard(Func<T, bool> predicate, Func<T, Error> error) => new GuardIO<TEnv, T>(this, predicate, error);

        public IO<TEnv, TResult> Match<TResult>(Func<T, TResult> okay, Func<Error, TResult> error)
            => Select(okay).Catch(error);

        public IO<TEnv, Nothing> Match(Action<T> okay, Action<Error> error)
            => Match(okay.Return(), error.Return());

        public IO<TEnv, T> Catch<TError>(Func<TError, T> map) => new CatchIO<TEnv, T, TError>(this, map.Then(Return));

        public abstract T Run(TEnv input);

        public IO<TEnv, TResult> Cast<TResult>() => Select(DynamicCast<TResult>.From);

        public IO<TEnv, T> Do(Action<T> action)
            => Select(v =>
            {
                action(v);
                return v;
            });

        public static implicit operator IO<TEnv, T>(T value) => Return(value);
        public static implicit operator IO<TEnv, T>(Error error) => Fail(error);

        public static IO<TEnv, T> operator |(IO<TEnv, T> x, T y) => x | Return(y);
        public static IO<TEnv, T> operator |(IO<TEnv, T> x, Error y) => x | Fail(y);
        public static IO<TEnv, T> operator |(IO<TEnv, T> x, IO<TEnv, T> y) => x.Or(y);
        public static IO<TEnv, T> operator |(IO<TEnv, T> x, IO<Nothing, T> y) => x | y.With<TEnv>(e => Nothing.Value);

        public static IO<TEnv, T> operator >>(IO<TEnv, T> x, IO<TEnv, T> y) => x.AndThen(_ => y);
        public static IO<TEnv, T> operator >>(IO<TEnv, T> x, IO<Nothing, T> y) => x >> y.With<TEnv>(e => Nothing.Value);
        public static IO<TEnv, T> operator >>(IO<TEnv, T> x, IO<TEnv, Nothing> y) => x.AndThen(v => y.Select(_ => v));
        public static IO<TEnv, T> operator >>(IO<TEnv, T> x, IO<Nothing, Nothing> y) => x >> y.With<TEnv>(e => Nothing.Value);
    }
}