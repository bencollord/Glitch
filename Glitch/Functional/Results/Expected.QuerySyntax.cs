using Glitch.Functional.Attributes;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    [MonadExtension(typeof(Expected<>))]
    public static class ResultQuerySyntax
    {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<TResult> SelectMany<T, TResult>(this Expected<T> source, Func<T, Expected<TResult>> bind)
            => source.AndThen(bind);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<TResult> SelectMany<T, TElement, TResult>(this Expected<T> source, Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));
    }
}
