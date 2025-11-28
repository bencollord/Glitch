namespace Glitch.Functional;

public static partial class RWSExtensions
{
    extension<TEnv, S, W, T>(RWS<TEnv, S, W, T> self)
        where W : IWritable<W>
    {
        public static RWS<TEnv, S, W, T> operator >>>(RWS<TEnv, S, W, T> x, Func<T, RWS<TEnv, S, W, Unit>> bind) => x.AndThen(bind, (x, _) => x);
        public static RWS<TEnv, S, W, T> operator >>>(RWS<TEnv, S, W, T> x, RWS<TEnv, S, W, Unit> y) => x.Then(y);
        public static RWS<TEnv, S, W, T> operator >>>(RWS<TEnv, S, W, T> x, Reader<TEnv, Unit> y) => x.Then(y);
        public static RWS<TEnv, S, W, T> operator >>>(RWS<TEnv, S, W, T> x, Writer<W, Unit> y) => x.Then(y);
        public static RWS<TEnv, S, W, T> operator >>>(RWS<TEnv, S, W, T> x, IStateful<S, Unit> y) => x.Then(y);
    }

    extension<TEnv, S, W, T, TResult>(RWS<TEnv, S, W, T> self)
        where W : IWritable<W>
    {
        // Map
        public static RWS<TEnv, S, W, TResult> operator *(RWS<TEnv, S, W, T> x, Func<T, TResult> map) => x.Select(map);
        public static RWS<TEnv, S, W, TResult> operator *(Func<T, TResult> map, RWS<TEnv, S, W, T> x) => x.Select(map);

        // Apply
        public static RWS<TEnv, S, W, TResult> operator *(RWS<TEnv, S, W, T> x, RWS<TEnv, S, W, Func<T, TResult>> apply) => x.Apply(apply);
        public static RWS<TEnv, S, W, TResult> operator *(RWS<TEnv, S, W, Func<T, TResult>> apply, RWS<TEnv, S, W, T> x) => x.Apply(apply);

        // Bind
        public static RWS<TEnv, S, W, TResult> operator >>>(RWS<TEnv, S, W, T> x, Func<T, RWS<TEnv, S, W, TResult>> bind) => x.AndThen(bind);

        public static RWS<TEnv, S, W, TResult> operator >>>(RWS<TEnv, S, W, T> x, RWS<TEnv, S, W, TResult> y) => x.Then(y);

        public static RWS<TEnv, S, W, TResult> operator >>>(RWS<TEnv, S, W, T> x, Reader<TEnv, TResult> y) => x.Then(y);
        public static RWS<TEnv, S, W, TResult> operator >>>(RWS<TEnv, S, W, T> x, Func<T, Reader<TEnv, TResult>> y) => x.AndThen(y);
        
        public static RWS<TEnv, S, W, TResult> operator >>>(RWS<TEnv, S, W, T> x, Writer<W, TResult> y) => x.Then(y);
        public static RWS<TEnv, S, W, TResult> operator >>>(RWS<TEnv, S, W, T> x, Func<T, Writer<W, TResult>> y) => x.AndThen(y);
        
        public static RWS<TEnv, S, W, TResult> operator >>>(RWS<TEnv, S, W, T> x, IStateful<S, TResult> y) => x.Then(y);
        public static RWS<TEnv, S, W, TResult> operator >>>(RWS<TEnv, S, W, T> x, Func<T, IStateful<S, TResult>> y) => x.AndThen(y);
    }
}
