namespace Glitch.Functional.Effects;

public static partial class ReaderExtensions
{
    extension<TEnv, T>(Reader<TEnv, T> self)
    {
        public static Reader<TEnv, T> operator >>>(Reader<TEnv, T> x, Func<T, Reader<TEnv, Unit>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<TEnv, T, TResult>(Reader<TEnv, T> self)
    {
        // Map
        public static Reader<TEnv, TResult> operator *(Reader<TEnv, T> x, Func<T, TResult> map) => x.Select(map);
        public static Reader<TEnv, TResult> operator *(Func<T, TResult> map, Reader<TEnv, T> x) => x.Select(map);

        // Apply
        public static Reader<TEnv, TResult> operator *(Reader<TEnv, T> x, Reader<TEnv, Func<T, TResult>> apply) => x.Apply(apply);
        public static Reader<TEnv, TResult> operator *(Reader<TEnv, Func<T, TResult>> apply, Reader<TEnv, T> x) => x.Apply(apply);

        // Bind
        public static Reader<TEnv, TResult> operator >>>(Reader<TEnv, T> x, Func<T, Reader<TEnv, TResult>> bind) => x.AndThen(bind);

        public static Reader<TEnv, TResult> operator >>>(Reader<TEnv, T> x, Reader<TEnv, TResult> y) => x.Then(y);
    }
}
