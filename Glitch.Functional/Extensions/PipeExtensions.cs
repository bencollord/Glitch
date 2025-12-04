using Glitch.Functional;
using System.Diagnostics;

namespace Glitch.Functional;

[DebuggerStepThrough]
public static class PipeExtensions
{
    extension<T, TResult>(T self)
    {
        public TResult PipeInto(Func<T, TResult> func) => func(self);

        // Pipe operator, similar to |> in F#
        public static TResult operator >>(T obj, Func<T, TResult> f) => f(obj);
    }

    extension<T, TResult>(Func<T, TResult> _)
    {
        // Pipe back operator, similar to <| in F#
        public static TResult operator <<(Func<T, TResult> f, T obj) => f(obj);
    }

    extension<T>(T self)
    {
        public static T operator >>(T obj, Func<T, Unit> f) => f(obj).Return(obj);
    }

    extension<T>(Func<T, Unit> _)
    {
        // Pipe back operator, similar to <| in F#
        public static T operator <<(Func<T, Unit> f, T obj) => f(obj).Return(obj);
    }
}
