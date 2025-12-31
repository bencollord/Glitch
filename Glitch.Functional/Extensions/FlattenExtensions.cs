using System.Diagnostics;

namespace Glitch.Functional;

[DebuggerStepThrough]
public static class FlattenExtensions
{
    public static Option<T> Flatten<T>(this Option<Option<T>> source) => source.AndThen(Identity);

    public static Result<T> Flatten<T>(this Result<Result<T>> source) => source.AndThen(Identity);

}
