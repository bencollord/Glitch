using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects;

// Instance
public abstract partial class IO<T>
{
    public static implicit operator IO<T>(T value) => Return(value);
    public static implicit operator IO<T>(Error error) => Fail(error);

    public static IO<T> operator |(IO<T> x, T y) => x | Return(y);
    public static IO<T> operator |(IO<T> x, Error y) => x | Fail(y);
    public static IO<T> operator |(IO<T> x, IO<T> y) => x.Or(y);

    public static IO<T> operator >>(IO<T> x, IO<T> y) => x.AndThen(_ => y);
    public static IO<T> operator >>(IO<T> x, IO<Unit> y) => x.AndThen(v => y.Select(_ => v));
}

// Extensions
// Extensions
public static partial class IOExtensions
{
    extension<T>(IO<T> self)
    {
        public static IO<T> operator >>(IO<T> x, Func<T, IO<Unit>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<T, TResult>(IO<T> self)
    {
        // Map
        public static IO<TResult> operator *(IO<T> x, Func<T, TResult> map) => x.Select(map);
        public static IO<TResult> operator *(Func<T, TResult> map, IO<T> x) => x.Select(map);

        // Apply
        public static IO<TResult> operator *(IO<T> x, IO<Func<T, TResult>> apply) => x.Apply(apply);
        public static IO<TResult> operator *(IO<Func<T, TResult>> apply, IO<T> x) => x.Apply(apply);

        // Bind
        public static IO<TResult> operator >>(IO<T> x, Func<T, IO<TResult>> bind) => x.AndThen(bind);

        public static IO<TResult> operator >>(IO<T> x, IO<TResult> y) => x.Then(y);
    }
}