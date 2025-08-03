namespace Glitch.Functional
{
    public static class NothingExtensions
    {
        public static Option<Nothing> IgnoreValue<T>(this Option<T> option) => option.Map(v => v.Ignore());
        public static Result<Nothing> IgnoreValue<T>(this Result<T> result) => result.Map(v => v.Ignore());
        public static Result<Nothing, E> IgnoreValue<T, E>(this Result<T, E> result) => result.Map(v => v.Ignore());
        public static IResult<Nothing, E> IgnoreValue<T, E>(this IResult<T, E> result) => result.Map(v => v.Ignore());
        public static Validation<Nothing> IgnoreValue<T>(this Validation<T> validation) => validation.Map(v => v.Ignore());
        public static Effect<Nothing> IgnoreResult<T>(this Effect<T> effect) => effect.Map(v => v.Ignore());
        public static Effect<TInput, Nothing> IgnoreResult<TInput, T>(this Effect<TInput, T> effect) => effect.Map(v => v.Ignore());
        public static IEffect<TInput, Nothing> IgnoreResult<TInput, T>(this IEffect<TInput, T> effect) => effect.Map(v => v.Ignore());
    }
}
