using Glitch.Functional;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Errors;

// Instance
public partial record Expected<T>
{
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator true(Expected<T> result) => result.IsOkay;

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator false(Expected<T> result) => result.IsFail;

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expected<T> operator &(Expected<T> x, Expected<T> y) => x.And(y);

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expected<T> operator |(Expected<T> x, Expected<T> y) => x.Or(y);

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator bool(Expected<T> result) => result.IsOkay;

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Expected<T>(T value) => Okay(value);

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Expected<T>(Okay<T> success) => Okay(success.Value);

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Expected<T>(Error error) => Fail(error);

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Expected<T>(Fail<Error> failure) => Fail(failure.Error);

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Expected<T>(Result<T, Error> result) => new(result);

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Result<T, Error>(Expected<T> result) => result.Match(Result.Okay<T, Error>, Result.Fail<T, Error>);

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator T(Expected<T> result)
        => result.Match(Identity, err => throw new InvalidCastException($"Cannot cast a faulted result to a value", err.AsException()));

    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Error(Expected<T> result)
        => result.Match(_ => throw new InvalidCastException("Cannot cast a successful result to an error"), FN.Identity);
}

// Extensions
public static partial class ExpectedExtensions
{
    extension<T>(Expected<T> self)
    {
        public static Expected<T> operator >>(Expected<T> x, Func<T, Expected<Unit>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<T, TResult>(Expected<T> self)
    {
        // Map
        public static Expected<TResult> operator *(Expected<T> x, Func<T, TResult> map) => x.Select(map);
        public static Expected<TResult> operator *(Func<T, TResult> map, Expected<T> x) => x.Select(map);

        // Apply
        public static Expected<TResult> operator *(Expected<T> x, Expected<Func<T, TResult>> apply) => x.Apply(apply);
        public static Expected<TResult> operator *(Expected<Func<T, TResult>> apply, Expected<T> x) => x.Apply(apply);

        // Bind
        public static Expected<TResult> operator >>(Expected<T> x, Func<T, Expected<TResult>> bind) => x.AndThen(bind);

        // And
        public static Expected<TResult> operator &(Expected<T> x, Expected<TResult> y) => x.And(y);
    }

    extension<T1, T2, TResult>(Expected<T1> self)
    {
        // Map
        public static Expected<Func<T2, TResult>> operator *(Expected<T1> x, Func<T1, T2, TResult> map) => x * map.Curry();
        public static Expected<Func<T2, TResult>> operator *(Func<T1, T2, TResult> map, Expected<T1> x) => x * map.Curry();

        // Apply
        public static Expected<Func<T2, TResult>> operator *(Expected<T1> x, Expected<Func<T1, T2, TResult>> apply) => x.Apply(apply * Curry);
        public static Expected<Func<T2, TResult>> operator *(Expected<Func<T1, T2, TResult>> apply, Expected<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, TResult>(Expected<T1> self)
    {
        // Map
        public static Expected<Func<T2, Func<T3, TResult>>> operator *(Expected<T1> x, Func<T1, T2, T3, TResult> map) => x * map.Curry();
        public static Expected<Func<T2, Func<T3, TResult>>> operator *(Func<T1, T2, T3, TResult> map, Expected<T1> x) => x * map.Curry();

        // Apply
        public static Expected<Func<T2, Func<T3, TResult>>> operator *(Expected<T1> x, Expected<Func<T1, T2, T3, TResult>> apply) => x.Apply(apply * Curry);
        public static Expected<Func<T2, Func<T3, TResult>>> operator *(Expected<Func<T1, T2, T3, TResult>> apply, Expected<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, TResult>(Expected<T1> self)
    {
        // Map
        public static Expected<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Expected<T1> x, Func<T1, T2, T3, T4, TResult> map) => x * map.Curry();
        public static Expected<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Func<T1, T2, T3, T4, TResult> map, Expected<T1> x) => x * map.Curry();

        // Apply
        public static Expected<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Expected<T1> x, Expected<Func<T1, T2, T3, T4, TResult>> apply) => x.Apply(apply * Curry);
        public static Expected<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Expected<Func<T1, T2, T3, T4, TResult>> apply, Expected<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, T5, TResult>(Expected<T1> self)
    {
        // Map
        public static Expected<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Expected<T1> x, Func<T1, T2, T3, T4, T5, TResult> map) => x * map.Curry();
        public static Expected<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Func<T1, T2, T3, T4, T5, TResult> map, Expected<T1> x) => x * map.Curry();

        // Apply
        public static Expected<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Expected<T1> x, Expected<Func<T1, T2, T3, T4, T5, TResult>> apply) => x.Apply(apply * Curry);
        public static Expected<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Expected<Func<T1, T2, T3, T4, T5, TResult>> apply, Expected<T1> x) => x.Apply(apply * Curry);
    }
}
