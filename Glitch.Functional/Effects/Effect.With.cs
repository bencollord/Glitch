namespace Glitch.Functional.Effects
{
    internal class WithEffect<TNewInput, TInput, T> : IEffect<TNewInput, T>
    {
        private readonly IEffect<TInput, T> source;
        private readonly Func<TNewInput, TInput> selector;

        internal WithEffect(IEffect<TInput, T> source, Func<TNewInput, TInput> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public T Run(TNewInput input)
        {
            return source.Run(selector(input));
        }
    }
}
