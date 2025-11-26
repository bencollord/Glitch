using Glitch.Functional;

namespace Glitch.Functional.Extensions;

// TODO Some of these methods are fundamental enough to Option that they should probably be pulled into core.
// Specifically methods where it could be on the option itself, but only applies to certain type parameters
public static class OptionExtensions
{
    public static T? AsNullable<T>(this Option<T> option)
        where T : struct
        => option.Match(v => v, () => new T?());

    public static Option<TResult> Apply<T, TResult>(this Option<Func<T, TResult>> function, Option<T> value)
        => value.Apply(function);

    /// <summary>
    /// Returns a the unwrapped values of all the non-empty options.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IEnumerable<T> Somes<T>(this IEnumerable<Option<T>> options)
        => options.Where(o => o.IsSome).Select(o => o.Unwrap());

    public static Option<T> Select<T>(this Option<bool> result, Func<Unit, T> ifTrue, Func<Unit, T> ifFalse)
        => result.Select(flag => flag ? ifTrue(default) : ifFalse(default));

    /// <summary>
    /// Allows three valued logic to be applied to an optional boolean.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="booleanOption"></param>
    /// <param name="ifTrue"></param>
    /// <param name="ifFalse"></param>
    /// <param name="ifNone"></param>
    /// <returns></returns>
    public static T Match<T>(this Option<bool> booleanOption, Func<T> ifTrue, Func<T> ifFalse, Func<T> ifNone)
        => booleanOption.Match(v => v ? ifTrue() : ifFalse(), ifNone);


    public static Unit Match(this Option<bool> result, Action ifTrue, Action ifFalse, Action ifNone)
        => result.Match(flag => flag ? ifTrue.Return()() : ifFalse.Return()(), ifNone.Return());

    /// <summary>
    /// Wraps the error in a <see cref="Expected{T}" /> if it exists,
    /// otherwise returns an okay <see cref="Expected{T}" /> containing 
    /// the provided value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result<T, E> FailOr<E, T>(this Option<E> opt, T value)
        => opt.Match(Result.Fail<T, E>, () => Result.Okay<T, E>(value));

    /// <summary>
    /// Wraps the error in a failed <see cref="Expected{T}" /> if it exists,
    /// otherwise returns an okay <see cref="Expected{T}" /> containing 
    /// the result of the provided function.
    /// </summary>
    /// <param name="function"></param>
    public static Result<T, E> FailOrElse<E, T>(this Option<E> opt, Func<Unit, T> function)
        => opt.Match(Result.Fail<T, E>, function.Then(Result.Okay<T, E>));

    /// <summary>
    /// Unzips an option of a tuple into a tuple of two options.
    /// </summary>
    /// <remarks>
    /// This method is intended to be used inline with tuple deconstruction.
    /// </remarks>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="option"></param>
    /// <returns></returns>
    public static (Option<T1>, Option<T2>) Unzip<T1, T2>(this Option<(T1, T2)> option)
        => (option.Select(o => o.Item1), option.Select(o => o.Item2));

    /// <summary>
    /// Flattens a nested option to a single level.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="nested"></param>
    /// <returns></returns>
    public static Option<T> Flatten<T>(this Option<Option<T>> nested)
        => nested.AndThen(o => o);
}
