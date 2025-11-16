namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        public static IEffect<TInput, TResult> Then<TInput, T, TResult>(this IEffect<TInput, T> source, IEffect<TInput, TResult> other)
            => source.AndThen(_ => other);

        public static IEffect<TInput, TResult> Then<TInput, T, TNext, TResult>(this IEffect<TInput, T> source, IEffect<TInput, TNext> other, Func<T, TNext, TResult> project)
            => source.AndThen(_ => other, project);

        public static IEffect<TInput, TResult> AndThen<TInput, T, TResult>(this IEffect<TInput, T> source, Func<T, IEffect<TInput, TResult>> bind)
            => new BindEffect<TInput, T, TResult>(source, bind);

        public static IEffect<TInput, TResult> AndThen<TInput, T, TNext, TResult>(this IEffect<TInput, T> source, Func<T, IEffect<TInput, TNext>> bind, Func<T, TNext, TResult> project)
            => new BindEffect<TInput, T, TNext, TResult>(source, bind, project);

        internal class BindEffect<TInput, T, TNext> : IEffect<TInput, TNext>
        {
            private readonly IEffect<TInput, T> source;
            private readonly Func<T, IEffect<TInput, TNext>> next;

            internal BindEffect(IEffect<TInput, T> source, Func<T, IEffect<TInput, TNext>> next)
            {
                this.source = source;
                this.next = next;
            }

            public TNext Run(TInput input)
            {
                var src = source.Run(input);
                var nxt = next(src);

                return nxt.Run(input);
            }
        }

        private class BindEffect<TInput, T, TNext, TResult> : IEffect<TInput, TResult>
        {
            private readonly IEffect<TInput, T> source;
            private readonly Func<T, IEffect<TInput, TNext>> next;
            private readonly Func<T, TNext, TResult> project;

            internal BindEffect(IEffect<TInput, T> source, Func<T, IEffect<TInput, TNext>> next, Func<T, TNext, TResult> project)
            {
                this.source = source;
                this.next = next;
                this.project = project;
            }

            public TResult Run(TInput input)
            {
                var src = source.Run(input);
                var nxt = next(src).Run(input);

                return project(src, nxt);
            }
        }
    }
}
