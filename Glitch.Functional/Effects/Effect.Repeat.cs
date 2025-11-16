namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        public static IEffect<TInput, IEnumerable<T>> Repeat<TInput, T>(this IEffect<TInput, T> source, int count)
            => new RepeatEffect<TInput, T>(source, count);

        private class RepeatEffect<TInput, T> : IEffect<TInput, IEnumerable<T>>
        {
            private readonly IEffect<TInput, T> source;
            private readonly int count;

            internal RepeatEffect(IEffect<TInput, T> source, int count)
            {
                this.source = source;
                this.count = count;
            }

            public IEnumerable<T> Run(TInput input)
            {
                for (int i = 0; i < count; i++)
                    yield return source.Run(input);
            }
        }
    }
}
