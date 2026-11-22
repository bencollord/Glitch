using Glitch.Functional.Validation;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional;

public readonly record struct Just<T>([NotNull] T Value)
{
    public Option<T> OrNone() => Option.Some(Value);

    // Linq
    // ============================================================================================

    // Map
    // --------------------------------------------------------------------------------------------
    public Just<TResult> Cast<TResult>() => DynamicCast<TResult>.From(Value); // TODO Should this return a result?
    public Just<TResult> Select<TResult>(Func<T, TResult> selector) => new(selector(Value));

    public Option<TResult> AndThen<TResult>(Func<T, Option<TResult>> bind) => bind(Value);
    public Option<TResult> AndThen<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
       => bind(Value).Select(project.Curry(Value));
    public Option<TResult> SelectMany<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
       => bind(Value).Select(project.Curry(Value));

    public override string ToString() => $"Just({Value})";

    public static implicit operator Just<T>(T value) => new(value);

    public static implicit operator T(Just<T> success) => success.Value;
}
