using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    [DebuggerStepThrough]
    public static class ObjectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult PipeInto<T, TResult>(this T obj, Func<T, TResult> func) => func(obj);
    }
}
