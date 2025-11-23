using Glitch.Functional;
using System.Diagnostics;

namespace Glitch.Functional
{
    [DebuggerStepThrough]
    public static class PipeExtensions
    {
        extension<T, TResult>(T self)
        {
            public TResult PipeInto(Func<T, TResult> func) => func(self);

            // Pipe operator, similar to |> in F#
            public static TResult operator >>(T obj, Func<T, TResult> f) => f(obj);
        }
    }
}
