using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional;

public static class DiscardExtensions
{
    public static Unit Ignore<T>(this T _) => default;
    public static Option<Unit> IgnoreValue<T>(this Option<T> option) => option.Select(Unit.Ignore);
    public static Result<Unit> IgnoreValue<T>(this Result<T> result) => result.Select(Unit.Ignore);
    public static Result<Unit, E> IgnoreValue<T, E>(this Result<T, E> result) => result.Select(Unit.Ignore);
}
