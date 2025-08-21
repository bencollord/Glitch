using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public static class ResultQuerySyntax
    {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult> Select<T, TResult>(this Result<T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult> SelectMany<T, TResult>(this Result<T> source, Func<T, Result<TResult>> bind)
            => source.AndThen(bind);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult> SelectMany<T, TElement, TResult>(this Result<T> source, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Where<T>(this Result<T> source, Func<T, bool> predicate)
            => source.Filter(predicate);
    }
}
