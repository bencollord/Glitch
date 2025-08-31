using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static class IgnoreExtensions
    {
        public static Unit Ignore<T>(this T _) => default;
        public static Option<Unit> IgnoreValue<T>(this Option<T> option) => option.Select(v => v.Ignore());
        public static Result<Unit> IgnoreValue<T>(this Result<T> result) => result.Select(v => v.Ignore());
        public static Expected<Unit, E> IgnoreValue<T, E>(this Expected<T, E> result) => result.Select(v => v.Ignore());
    }
}
