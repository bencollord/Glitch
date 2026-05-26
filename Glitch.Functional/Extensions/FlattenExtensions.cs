using Glitch.Functional.Errors;
using System.Diagnostics;

namespace Glitch.Functional;

[DebuggerStepThrough]
public static class FlattenExtensions
{
    public static Option<T> Flatten<T>(this Option<Option<T>> source) => source.AndThen(Identity);

    public static Result<T, E> Flatten<T, E>(this Result<Result<T, E>, E> source) => source.AndThen(Identity);

}
