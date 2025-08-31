using Glitch.Functional.Attributes;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    [MonadExtension(typeof(Result<>))]
    public static class ResultQuerySyntax
    {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult> SelectMany<T, TResult>(this Result<T> source, Func<T, Result<TResult>> bind)
            => source.AndThen(bind);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult> SelectMany<T, TElement, TResult>(this Result<T> source, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));
    }
}
