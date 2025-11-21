using Glitch.Functional.Errors;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Core
{
    // Instance operators
    public readonly partial struct Option<T>
    {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(Option<T> option) => option.IsSome;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator false(Option<T> option) => option.IsNone;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> operator &(Option<T> x, Option<T> y) => x.And(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> operator ^(Option<T> x, Option<T> y) => x.Xor(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> operator |(Option<T> x, Option<T> y) => x.Or(y);
        
        // Coalescing operators
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T operator |(Option<T> x, T y) => x.IfNone(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T operator |(Option<T> x, Okay<T> y) => x.IfNone(y.Value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(Option<T> option) => option.IsSome;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Option<T>(T? value) => Maybe(value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Option<T>(OptionNone _) => new();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Option<T> x, Option<T> y) => x.Equals(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(Option<T> x, Option<T> y) => x.CompareTo(y) >= 0;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(Option<T> x, Option<T> y) => x.CompareTo(y) <= 0;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(Option<T> x, Option<T> y) => x.CompareTo(y) > 0;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(Option<T> x, Option<T> y) => x.CompareTo(y) < 0;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Option<T> x, Option<T> y) => !(x == y);
    }

    // Extensions
    public static partial class OptionExtensions
    {
        extension<T>(Option<T> self)
        {
            public static Option<T> operator >>(Option<T> x, Func<T, Option<Unit>> bind) => x.AndThen(bind, (x, _) => x);
        }

        extension<T, TResult>(Option<T> self)
        {
            // Map
            public static Option<TResult> operator *(Option<T> x, Func<T, TResult> map) => x.Select(map);
            public static Option<TResult> operator *(Func<T, TResult> map, Option<T> x) => x.Select(map);

            // Apply
            public static Option<TResult> operator *(Option<T> x, Option<Func<T, TResult>> apply) => x.Apply(apply);
            public static Option<TResult> operator *(Option<Func<T, TResult>> apply, Option<T> x) => x.Apply(apply);

            // Bind
            public static Option<TResult> operator >>(Option<T> x, Func<T, Option<TResult>> bind) => x.AndThen(bind);

            // And
            public static Option<TResult> operator &(Option<T> x, Option<TResult> y) => x.And(y);
        }
    }
}
