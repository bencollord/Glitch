using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects;

// Instance
public partial class Effect<TEnv, T>
{
    public static implicit operator Effect<TEnv, T>(Effect<T> effect) => effect.With<TEnv>();

    public static implicit operator Effect<TEnv, T>(Expected<T> result) => new(_ => result);

    public static implicit operator Effect<TEnv, T>(Result<T, Error> result) => new(_ => result);

    public static implicit operator Effect<TEnv, T>(T value) => Return(value);

    public static implicit operator Effect<TEnv, T>(Error error) => Fail(error);

    public static Effect<TEnv, T> operator |(Effect<TEnv, T> x, Effect<TEnv, T> y) => x.Or(y);
    public static Effect<TEnv, T> operator |(Effect<TEnv, T> x, Effect<T> y) => x.Or(y);
    public static Effect<TEnv, T> operator |(Effect<TEnv, T> x, Expected<T> y) => x.Or(y);
    public static Effect<TEnv, T> operator |(Effect<TEnv, T> x, Result<T, Error> y) => x.Or(y);

    public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Effect<TEnv, T> y)
        => x.Then(y);

    public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Effect<T> y)
        => x.AndThen(_ => Lift(y));

    public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Effect<TEnv, Unit> y)
        => x.AndThen(v => y.Select(_ => v));

    public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Effect<Unit> y)
        => x.AndThen(v => Lift(y.Select(_ => v)));


    public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Func<TEnv, Result<T, Error>> y)
        => new(i =>
        {
            _ = x.thunk(i);
            return y(i);
        });

    public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Func<T> y)
        => new(i =>
        {
            _ = x.thunk(i);
            return Expected.Okay(y());
        });
}


// Extensions
public static partial class EffectExtensions
{
    extension<TEnv, T>(Effect<TEnv, T> self)
    {
        public static Effect<TEnv, T> operator >>>(Effect<TEnv, T> x, Func<T, Effect<TEnv, Unit>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<TEnv, T, TResult>(Effect<TEnv, T> self)
    {
        // Map
        public static Effect<TEnv, TResult> operator *(Effect<TEnv, T> x, Func<T, TResult> map) => x.Select(map);
        public static Effect<TEnv, TResult> operator *(Func<T, TResult> map, Effect<TEnv, T> x) => x.Select(map);

        // Apply
        public static Effect<TEnv, TResult> operator *(Effect<TEnv, T> x, Effect<TEnv, Func<T, TResult>> apply) => x.Apply(apply);
        public static Effect<TEnv, TResult> operator *(Effect<TEnv, Func<T, TResult>> apply, Effect<TEnv, T> x) => x.Apply(apply);

        // Bind
        public static Effect<TEnv, TResult> operator >>>(Effect<TEnv, T> x, Func<T, Effect<TEnv, TResult>> bind) => x.AndThen(bind);

        public static Effect<TEnv, TResult> operator >>>(Effect<TEnv, T> x, Effect<TEnv, TResult> y) => x.Then(y);
    }
}
