using Glitch.Functional.Effects.Internal;

namespace Glitch.Functional.Effects
{
    /// <summary>
    /// Provides factory methods which can infer return types after only
    /// being provided the type of <typeparamref name="TInput"/>.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public static class Effect<TInput>
    {
        public static IEffect<TInput, T> Return<T>(T value) => new ReturnEffect<TInput, T>(value);
    }
}
