using Glitch.Functional.Errors;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Core
{
    // Instance
    public partial record Result<T, E>
    {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(Result<T, E> result) => result.IsOkay;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator false(Result<T, E> result) => result.IsError;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, E> operator &(Result<T, E> x, Result<T, E> y) => x.And(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, E> operator |(Result<T, E> x, Result<T, E> y) => x.Or(y);
        
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, E> operator |(Option<T> x, Result<T, E> y) => x.Or(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(Result<T, E> result) => result.IsOkay;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<T, E>(T value) => Okay(value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<T, E>(Okay<T> success) => Okay(success.Value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<T, E>(E error) => Fail(error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<T, E>(Fail<E> failure) => Fail(failure.Error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator T(Result<T, E> result)
            => result.Match(Identity, e => throw new InvalidCastException(ErrorMessages.InvalidCast<T>(e)));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator E(Result<T, E> result)
            => result.Match(v => throw new InvalidCastException(ErrorMessages.InvalidCast<E>(result)), Identity);
    }

    // Extensions
    public static partial class ResultExtensions
    {
        extension<T, E>(Result<T, E> self)
        {
            public static Result<T, E> operator >>(Result<T, E> x, Func<T, Result<Unit, E>> bind) => x.AndThen(bind, (x, _) => x);
        }

        extension<T, E, TResult>(Result<T, E> self)
        {
            // Map
            public static Result<TResult, E> operator *(Result<T, E> x, Func<T, TResult> map) => x.Select(map);
            public static Result<TResult, E> operator *(Func<T, TResult> map, Result<T, E> x) => x.Select(map);

            // Apply
            public static Result<TResult, E> operator *(Result<T, E> x, Result<Func<T, TResult>, E> apply) => x.Apply(apply);
            public static Result<TResult, E> operator *(Result<Func<T, TResult>, E> apply, Result<T, E> x) => x.Apply(apply);

            // Bind
            public static Result<TResult, E> operator >>(Result<T, E> x, Func<T, Result<TResult, E>> bind) => x.AndThen(bind);

            // And
            public static Result<TResult, E> operator &(Result<T, E> x, Result<TResult, E> y) => x.And(y);
        }
    }
}