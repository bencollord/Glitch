using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static class IgnoreExtensions
    {
        public static Unit Ignore<T>(this T _) => default;
        public static Option<Unit> IgnoreValue<T>(this Option<T> option) => option.Select(v => v.Ignore());
        public static Expected<Unit> IgnoreValue<T>(this Expected<T> result) => result.Select(v => v.Ignore());
        public static Result<Unit, E> IgnoreValue<T, E>(this Result<T, E> result) => result.Select(v => v.Ignore());
    }
}
