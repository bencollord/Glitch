namespace Glitch.Functional
{
    public static class IgnoreExtensions
    {
        public static Unit Ignore<T>(this T _) => default;
        public static Option<Unit> IgnoreValue<T>(this Option<T> option) => option.Map(v => v.Ignore());
        public static Result<Unit> IgnoreValue<T>(this Result<T> result) => result.Select(v => v.Ignore());
        public static Result<Unit, E> IgnoreValue<T, E>(this Result<T, E> result) => result.Map(v => v.Ignore());
        public static Effect<Unit> IgnoreResult<T>(this Effect<T> effect) => effect.Select(v => v.Ignore());
        public static Effect<TInput, Unit> IgnoreResult<TInput, T>(this Effect<TInput, T> effect) => effect.Map(v => v.Ignore());
        public static IEffect<TInput, Unit> IgnoreResult<TInput, T>(this IEffect<TInput, T> effect) => effect.Map(v => v.Ignore());
    }
}
