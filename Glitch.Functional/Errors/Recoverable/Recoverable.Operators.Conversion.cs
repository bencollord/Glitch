using Glitch.Functional.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Validation;

public partial record Recoverable<T, E>
{
    public static implicit operator Recoverable<T, E>(T value) => new Okay(value);

    public static implicit operator Recoverable<T, E>(Okay<T> success) => new Okay(success.Value);

    public static implicit operator Recoverable<T, E>(E error) => new Fatal(error);

    public static implicit operator Recoverable<T, E>(Fail<E> failure) => new Fatal(failure.Error);

    public static implicit operator Recoverable<T, E>(Result<T, E> result) => result.Match(Recoverable.Okay<T, E>, Recoverable.Fatal<T, E>);

    public static implicit operator Recoverable<T, E>((T Value, E Error) tuple) => Recoverable.Warning(tuple.Value, tuple.Error);
}