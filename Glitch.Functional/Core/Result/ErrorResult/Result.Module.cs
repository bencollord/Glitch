using Glitch.Functional.Errors;

namespace Glitch.Functional;

public static partial class Result
{
    public static Result<Unit> Okay() => Okay(Unit.Value);

    public static Result<T> Okay<T>()
        where T : new()
        => Okay(new T());

    public static Result<T> Okay<T>(T value) => new Result<T>.Okay(value);

    public static Result<Unit> Fail(Error error) => Fail<Unit, Error>(error);

    public static Result<Unit> Fail(IEnumerable<Error> errors) => Fail(Error.New(errors));

    public static Result<T> Fail<T>(Error error) => Fail<T, Error>(error);

    public static Result<T> Fail<T>(IEnumerable<Error> errors) => Fail<T, Error>(Error.New(errors));

    public static Result<T> From<T, E>(Result<T, E> result) where E : Error => Result<T>.From(result);

    public static Result<Unit> Guard(bool condition, Error error)
        => Guard(condition, Unit.Value, error);

    public static Result<T> Guard<T>(bool condition, T value, Error error)
        => condition ? Okay(value) : Fail<T>(error);

    public static Result<T> Guard<T>(bool condition, T value, Func<T, Error> error)
        => condition ? Okay(value) : Fail<T>(error(value));

    public static Result<T> Guard<T>(Func<T, bool> predicate, T value, Error error)
        => predicate(value) ? Okay(value) : Fail<T>(error);

    public static Result<T> Guard<T>(Func<T, bool> predicate, T value, Func<T, Error> error)
        => predicate(value) ? Okay(value) : Fail<T>(error(value));
}