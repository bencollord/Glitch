using Glitch.Functional.Extensions;

namespace Glitch.Functional;

/// <summary>
/// Static methods for <see cref="Option{T}"/>, mostly to simplify
/// syntax when passing higher order functions.
/// </summary>
public static partial class Option
{
    public static OptionNone None => OptionNone.Value;

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

    public static Option<TResult> Map<T, TResult>(Option<T> value, Func<T, TResult> map) => value.Select(map);
    public static Option<TResult> Map<T, TResult>(T? value, Func<T, TResult> map) => Maybe(value).Select(map);
    public static Option<TResult> Map<T, TResult>(T? value, Func<T, TResult> map) where T : struct => Maybe(value).Select(map);

    public static Option<TResult> Apply<T, TResult>(Option<Func<T, TResult>> func, Option<T> value) => func.AndThen(fn => value * fn);
    public static Option<TResult> Apply<T, TResult>(Option<Func<T, TResult>> func, T? value) => func % Maybe(value);
    public static Option<TResult> Apply<T, TResult>(Option<Func<T, TResult>> func, T? value) where T : struct => func % Maybe(value);

    public static Option<TResult> Bind<T, TResult>(Option<T> value, Func<T, Option<TResult>> bind) => value.AndThen(bind);
    public static Option<TResult> Bind<T, TResult>(T? value, Func<T, Option<TResult>> bind) => Maybe(value).AndThen(bind);
    public static Option<TResult> Bind<T, TResult>(T? value, Func<T, Option<TResult>> bind) where T : struct => Maybe(value).AndThen(bind);

    public static Option<T> Filter<T>(Option<T> value, Func<T, bool> predicate) => value.Where(predicate);
    public static Option<T> Filter<T>(T? value, Func<T, bool> predicate) => Maybe(value).Where(predicate);
    public static Option<T> Filter<T>(T? value, Func<T, bool> predicate) where T : struct => Maybe(value).Where(predicate);

    public static TResult Match<T, TResult>(Option<T> value, Func<T, TResult> some, Func<Unit, TResult> none) => value.Match(some, none);
    public static TResult Match<T, TResult>(T? value, Func<T, TResult> some, Func<Unit, TResult> none) => Maybe(value).Match(some, none);
    public static TResult Match<T, TResult>(T? value, Func<T, TResult> some, Func<Unit, TResult> none) where T : struct => Maybe(value).Match(some, none);

    public static TResult Match<T, TResult>(Option<T> value, Func<T, TResult> some, Func<TResult> none) => value.Match(some, none);
    public static TResult Match<T, TResult>(T? value, Func<T, TResult> some, Func<TResult> none) => Maybe(value).Match(some, none);
    public static TResult Match<T, TResult>(T? value, Func<T, TResult> some, Func<TResult> none) where T : struct => Maybe(value).Match(some, none);

    public static TResult Match<T, TResult>(Option<T> value, Func<T, TResult> some, TResult none) => value.Match(some, none);
    public static TResult Match<T, TResult>(T? value, Func<T, TResult> some, TResult none) => Maybe(value).Match(some, none);
    public static TResult Match<T, TResult>(T? value, Func<T, TResult> some, TResult none) where T : struct => Maybe(value).Match(some, none);

    public static (Option<TLeft> Left, Option<TRight> Right) Unzip<TLeft, TRight>(Option<(TLeft Left, TRight Right)> option) =>
        (option.Select(x => x.Left), option.Select(x => x.Right));

    public static T IfNone<T>(Option<T> option, T none) => option.IfNone(none);

    // ========================================================================
    // Curried for use in Linq expressions and operators
    // ========================================================================
    // TODO If this works out, add to Result as well, then possibly other monads
    public static Func<Option<T>, Option<TResult>> Map<T, TResult>(Func<T, TResult> map) => (Option<T> value) => value.Select(map);

    public static Func<Option<Func<T, TResult>>, Option<TResult>> Apply<T, TResult>(Option<T> value) => (Option<Func<T, TResult>> func) => func.AndThen(fn => value * fn);

    public static Func<Option<T>, Option<TResult>> Bind<T, TResult>(Func<T, Option<TResult>> bind) => (Option<T> value) => value.AndThen(bind);

    public static Func<Option<T>, Option<T>> Filter<T>(Func<T, bool> predicate) => (Option<T> value) => value.Where(predicate);
    
    public static Func<Option<T>, TResult> Match<T, TResult>(Func<T, TResult> some, Func<Unit, TResult> none) => (Option<T> value) => value.Match(some, none);
    
    public static Func<Option<T>, TResult> Match<T, TResult>(Func<T, TResult> some, Func<TResult> none) => (Option<T> value) => value.Match(some, none);
    
    public static Func<Option<T>, TResult> Match<T, TResult>(Func<T, TResult> some, TResult none) => (Option<T> value) => value.Match(some, none);
    
    public static Func<Option<T>, T> IfNone<T>(T none) => (Option<T> value) => value.IfNone(none);
}
