namespace Glitch.Functional.Extensions.Impure;

using Glitch.Functional.Effects;

public static partial class ImpureExtensions
{
    extension<TInput, T>(Reader<TInput, T> self)
    {
        public Reader<TInput, T> Do(Action<T> action) =>
            self.Do(action.Return());

        public Reader<TInput, T> Do(Func<T, Unit> action) =>
            self.Select(val => action(val).Return(val));
    }
}
