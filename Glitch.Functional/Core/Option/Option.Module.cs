using Glitch.Functional.Extensions;

namespace Glitch.Functional;

/// <summary>
/// Static methods for <see cref="Option{T}"/>, mostly to simplify
/// syntax when passing higher order functions.
/// </summary>
public partial struct Option : IMaybe<Unit>
{
    public static Option None => new();

    public bool HasValue => false;

    public static Option<T> Some<T>(T value) => Option<T>.Some(value);

    public static Option<T> Maybe<T>(T? value) => Option<T>.Maybe(value);

    public static Option<T> Maybe<T>(T? value) where T : struct
        => value.HasValue
         ? Some(value.Value)
         : None;

    // Function syntax for methods
    // UNDONE
    public static IEnumerable<T> Somes<T>(IEnumerable<Option<T>> options) => options.Somes();

    public static bool IsSome<T>(Option<T> option) => option.IsSome;
    public static bool IsNone<T>(Option<T> option) => option.IsNone;

    public static Option<T> Where<T>(T? value, Func<T, bool> predicate) => Maybe(value).Where(predicate);
    public static Option<T> Where<T>(T? value, Func<T, bool> predicate) where T : struct => Maybe(value).Where(predicate);

    public static (Option<TLeft> Left, Option<TRight> Right) Unzip<TLeft, TRight>(Option<(TLeft Left, TRight Right)> option) =>
        (option.Select(x => x.Left), option.Select(x => x.Right));

    public static T IfNone<T>(Option<T> option, T none) => option.IfNone(none);

    public TResult Match<TResult>(Func<Unit, TResult> some, Func<TResult> none) => none();
}
