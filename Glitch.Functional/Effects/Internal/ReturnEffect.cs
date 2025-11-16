namespace Glitch.Functional.Effects.Internal
{
    internal class ReturnEffect<TInput, T> : IEffect<TInput, T>
    {
        private readonly T value;

        internal ReturnEffect(T value)
        {
            this.value = value;
        }

        public T Run(TInput _)
        {
            return value;
        }
    }
}
