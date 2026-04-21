namespace Glitch.Functional.Effects;

public readonly record struct Guard<T, E>(bool IsOkay, Func<E> Fail);
