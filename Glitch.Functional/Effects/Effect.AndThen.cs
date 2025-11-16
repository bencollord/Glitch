using Glitch.Functional.Effects.Internal;

namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        public static IEffect<TInput, TResult> AndThen<TInput, T, TResult>(this IEffect<TInput, T> source, Func<T, IEffect<TInput, TResult>> bind)
            => new BindEffect<TInput, T, TResult>(source, bind);

        public static IEffect<TInput, TResult> AndThen<TInput, T, TNext, TResult>(this IEffect<TInput, T> source, Func<T, IEffect<TInput, TNext>> bind, Func<T, TNext, TResult> project)
            => new BindEffect<TInput, T, TNext, TResult>(source, bind, project);
    }
}
