using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        public static Expected<T> Try<T>(this IEffect<Unit, T> source) => source.Try(Unit.Value);

        public static Expected<T> Try<TInput, T>(this IEffect<TInput, T> source, TInput input)
        {
            try
            {
                return source.Run(input);
            }
            catch (Exception ex)
            {
                return Error.New(ex);
            }
        }
    }
}
