using Glitch.Functional.Core;

namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        public static IEffect<Unit, T> Return<T>(T value) => Effect<Unit>.Return(value);
    }

    /// <summary>
    /// Provides factory methods which can infer return types after only
    /// being provided the type of <typeparamref name="TInput"/>.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public static class Effect<TInput>
    {
        public static IEffect<TInput, T> Return<T>(T value) => new ReturnEffect<T>(value);

        private class ReturnEffect<T> : IEffect<TInput, T>
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
}
