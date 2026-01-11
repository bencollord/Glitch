using System;
using System.Collections.Generic;
using System.Text;

namespace Glitch.Functional.Effects;

public static class Lazy
{
    public static Lazy<T> Return<T>(T value) => new(value);

    public static Lazy<T> Lift<T>(Func<T> func) => new(func);

    public static Lazy<T> Lift<T>(Func<Unit, T> func) => Lift(func.Apply(Unit.Value));

    public static bool IsValueCreated<T>(Lazy<T> value) => value.IsValueCreated;

    public static Lazy<TResult> Map<T, TResult>(Lazy<T> value, Func<T, TResult> map) => value.Select(map);

    public static Lazy<TResult> Apply<T, TResult>(Lazy<Func<T, TResult>> func, Lazy<T> value) => func.AndThen(fn => value.Select(fn));

    public static Lazy<TResult> Bind<T, TResult>(Lazy<T> value, Func<T, Lazy<TResult>> bind) => value.AndThen(bind);
}
