using Glitch.Functional;

namespace Glitch.Functional.Errors;

public partial record Expected<T>
{
    public static implicit operator Expected<T>(T value) => new Okay(value);

    public static implicit operator Expected<T>(Okay<T> success) => new Okay(success.Value);

    public static implicit operator Expected<T>(Error error) => new Fail(error);

    public static implicit operator Expected<T>(Fail<Error> failure) => new Fail(failure.Error);

    public static implicit operator Expected<T>(Result<T, Error> result) => new(result);

    public static implicit operator Result<T, Error>(Expected<T> result) => result.Match(Result.Okay<T, Error>, Result.Fail<T, Error>);

    public static explicit operator T(Expected<T> result)
        => result.Match(Identity, err => throw new InvalidCastException($"Cannot cast a faulted result to a value", err.AsException()));

    public static explicit operator Error(Expected<T> result)
        => result.Match(_ => throw new InvalidCastException("Cannot cast a successful result to an error"), FN.Identity);
}
