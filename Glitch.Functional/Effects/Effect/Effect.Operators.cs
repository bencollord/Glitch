using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects;

// Instance
public partial class Effect<T>
{
    public static implicit operator Effect<T>(Expected<T> result) => Return(result);

    public static implicit operator Effect<T>(T value) => Return(value);

    public static implicit operator Effect<T>(Error error) => Fail(error);

    public static implicit operator Effect<T>(Effect<Unit, T> effect) => new(effect);

    public static Effect<T> operator |(Effect<T> x, Effect<T> y) => x.Or(y);

    public static Effect<T> operator |(Effect<T> x, Expected<T> y) => x.Or(y);

    public static Effect<T> operator |(Effect<T> x, Result<T, Error> y) => x.Or((Expected<T>)y);

    public static Effect<T> operator |(Effect<T> x, Error y) => x.Or(y);

    public static Effect<T> operator >>(Effect<T> x, Effect<T> y) => x.Then(y);

    public static Effect<T> operator >>(Effect<T> x, Effect<Unit> y) => x.Then(y, (v, _) => v);

    public static Effect<T> operator >>(Effect<T> x, Func<Expected<T>> y) => x.AndThen(_ => y());

    public static Effect<T> operator >>(Effect<T> x, Func<Result<T, Error>> y) => x.AndThen(_ => y().Match(Expected.Okay, Expected.Fail<T>));

    public static Effect<T> operator >>(Effect<T> x, Func<T> y) => x.Select(_ => y());
}

// Extensions
public static partial class EffectExtensions
{
    extension<T>(Effect<T> self)
    {
        public static Effect<T> operator >>(Effect<T> x, Func<T, Effect<Unit>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<T, TResult>(Effect<T> self)
    {
        // Map
        public static Effect<TResult> operator *(Effect<T> x, Func<T, TResult> map) => x.Select(map);
        public static Effect<TResult> operator *(Func<T, TResult> map, Effect<T> x) => x.Select(map);

        // Apply
        public static Effect<TResult> operator *(Effect<T> x, Effect<Func<T, TResult>> apply) => x.Apply(apply);
        public static Effect<TResult> operator *(Effect<Func<T, TResult>> apply, Effect<T> x) => x.Apply(apply);

        // Bind
        public static Effect<TResult> operator >>(Effect<T> x, Func<T, Effect<TResult>> bind) => x.AndThen(bind);

        public static Effect<TResult> operator >>(Effect<T> x, Effect<TResult> y) => x.Then(y);
    }
}
