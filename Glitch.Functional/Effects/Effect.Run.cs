using Glitch.Functional.Core;

namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        /// <summary>
        /// Convenience method for running an effect that doesn't take meaningful input.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Run<T>(this IEffect<Unit, T> source) => source.Run(Unit.Value);
    }
}
