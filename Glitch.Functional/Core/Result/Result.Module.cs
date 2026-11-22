using Glitch.Functional.Errors;
using Glitch.Functional.Extensions;

namespace Glitch.Functional;

public static partial class Result
{
    public static Okay<T> Okay<T>() 
        where T : new() 
        => Okay(new T());

    public static Okay<T> Okay<T>(T value) => new(value);

    public static Result<T, E> Okay<T, E>(T value) => Okay(value);
    
    public static Fail<E> Fail<E>(E error) => new(error);

    public static Result<T, E> Fail<T, E>(E error) => Fail(error);

    public static bool IsOkay<T, E>(Result<T, E> result) => result.IsOkay;

    public static bool IsFail<T, E>(Result<T, E> result) => result.IsFail;

    public static Result<Unit, E> Guard<E>(bool condition, E error)
        => condition ? Okay(Unit.Value) : Fail(error);

    public static Result<Unit, E> Guard<E>(bool condition, Func<Unit, E> error)
        => condition ? Okay(Unit.Value) : Fail(error(Unit.Value));

    public static Result<Unit, E> Guard<E>(Func<Unit, bool> predicate, E error)
        => predicate(Unit.Value) ? Okay(Unit.Value) : Fail(error);

    public static Result<T, E> Guard<T, E>(bool condition, T value, E error)
        => condition ? Okay(value) : Fail(error);

    public static Result<T, E> Guard<T, E>(bool condition, T value, Func<T, E> error)
        => condition ? Okay(value) : Fail(error(value));

    public static Result<T, E> Guard<T, E>(Func<T, bool> predicate, T value, E error)
        => predicate(value) ? Okay(value) : Fail(error);

    public static Result<T, E> Guard<T, E>(Func<T, bool> predicate, T value, Func<T, E> error)
        => predicate(value) ? Okay(value) : Fail(error(value));

    public static Option<T> OkayOrNone<T, E>(Result<T, E> result) => result.OkayOrNone();

    public static Option<E> ErrorOrNone<T, E>(Result<T, E> result) => result.ErrorOrNone();

    public static T Unwrap<T, E>(Result<T, E> result) => result.Unwrap();

    public static E UnwrapError<T, E>(Result<T, E> result) => result.UnwrapError();

    public static (IEnumerable<T> Successes, IEnumerable<E> Errors) Partition<T, E>(IEnumerable<Result<T, E>> results) => results.Partition();

    public static Result<TResult, E> Map<T, E, TResult>(Result<T, E> value, Func<T, TResult> map) => value.Select(map);
    public static Result<TResult, E> Apply<T, E, TResult>(Result<Func<T, TResult>, E> func, Result<T, E> value) => func.AndThen(fn => value * fn);
    public static Result<TResult, E> Bind<T, E, TResult>(Result<T, E> value, Func<T, Result<TResult, E>> bind) => value.AndThen(bind);

    public static TResult Match<T, E, TResult>(Result<T, E> value, Func<T, TResult> okay, Func<E, TResult> fail) => value.Match(okay, fail);
    public static TResult Match<T, E, TResult>(Result<T, E> value, Func<T, TResult> okay, TResult fail) => value.Match(okay, fail);

    public static (Result<TLeft, E> Left, Result<TRight, E> Right) Unzip<TLeft, TRight, E>(Result<(TLeft Left, TRight Right), E> option) =>
        (option.Select(x => x.Left), option.Select(x => x.Right));

    public static T IfFail<T, E>(Result<T, E> value, T fail) => value.IfFail(fail);
    public static T IfFail<T, E>(Result<T, E> value, Func<E, T> fail) => value.IfFail(fail);
}