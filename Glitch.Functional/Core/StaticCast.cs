using Glitch.Functional.Errors;

namespace Glitch.Functional;

public static class StaticCast<T>
{
    public static T UpFrom<TDerived>(TDerived obj)
        where TDerived : T => obj;

    public static T DownFrom<TFrom>(TFrom obj) => TryDownFrom(obj).IfNone(_ => Error.InvalidCast<T>(obj).Throw<T>());

    public static Option<T> TryDownFrom<TFrom>(TFrom obj) => obj is T ok ? Option.Some(ok) : Option.None;
}

// EXPERIMENTAL May remove. Trying to decide on what's more clear syntactically when scanning code.
public static class UpCast<T>
{
    public static T From<TDerived>(TDerived obj)
        where TDerived : T => obj;

}

public static class DownCast<T>
{
    public static T From<TFrom>(TFrom obj) => Try(obj).IfNone(_ => Error.InvalidCast<T>(obj).Throw<T>());
    public static Option<T> Try<TFrom>(TFrom obj) => obj is T ok ? Option.Some(ok) : Option.None;
}
