using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects;

public static partial class Effect
{
    public static Effect<TEnv, TEnv> Ask<TEnv>() => Effect<TEnv, TEnv>.Lift(Identity);

    public static Effect<TEnv, T> Asks<TEnv, T>(Func<TEnv, T> function) => Effect<TEnv, T>.Lift(function);

    public static Effect<TEnv, T> Return<TEnv, T>(T value) => Effect<T>.Return(value);

    public static Effect<TEnv, T> Fail<TEnv, T>(Error error) => Effect<T>.Fail(error);

    public static Effect<TEnv, T> Return<TEnv, T>(Result<T, Error> result) => Effect<TEnv, T>.Return(result);
    public static Effect<TEnv, T> Return<TEnv, T, E>(Result<T, E> result) where E : Error => Effect<TEnv, T>.Return(result);

    public static Effect<TEnv, T> Lift<TEnv, T>(Func<TEnv, Result<T, Error>> function) => Lift<TEnv, T, Error>(function);
    public static Effect<TEnv, T> Lift<TEnv, T, E>(Func<TEnv, Result<T, E>> function) where E : Error => Lift<TEnv, T>(x => function(x).Match(Result.Okay<T, Error>, Result.Fail<T, Error>));

    public static Effect<TEnv, Unit> Lift<TEnv>(Action<TEnv> action) => Effect<TEnv, Unit>.Lift(action.Return());
    public static Effect<TEnv, Unit> Lift<TEnv>(Func<TEnv, Unit> action) => Effect<TEnv, Unit>.Lift(action);
    public static Effect<TEnv, T> Lift<TEnv, T>(Func<TEnv, T> function) => Effect<TEnv, T>.Lift(function);

    public static Guard<Unit, Error> Guard(bool condition, Error error) => Guard<Error>(condition, () => error);
    public static Guard<Unit, Error> Guard(bool condition, Func<Error> error) => new(condition, error);
    public static Guard<Unit, E> Guard<E>(bool condition, E error) where E : Error => Guard(condition, () => error);
    public static Guard<Unit, E> Guard<E>(bool condition, Func<E> error) where E : Error => new(condition, error);
    public static Guard<T, E> Guard<T, E>(bool condition, E error) where E : Error => Guard<T, E>(condition, () => error);
    public static Guard<T, E> Guard<T, E>(bool condition, Func<E> error) where E : Error => new(condition, error);

    // TODO These are the same as Lift, but I'm experimenting with a new naming convention.
    // Pick one and stick with it.
    public static Effect<TEnv, T> TryWith<TEnv, T>(Func<TEnv, Result<T, Error>> function) => Effect<TEnv, T>.Lift(function);

    public static Effect<TEnv, Unit> TryWith<TEnv>(Action<TEnv> action) => Effect<TEnv, Unit>.Lift(action.Return());
    public static Effect<TEnv, Unit> TryWith<TEnv>(Func<TEnv, Unit> action) => Effect<TEnv, Unit>.Lift(action);
    public static Effect<TEnv, T> TryWith<TEnv, T>(Func<TEnv, T> function) => Effect<TEnv, T>.Lift(function);
}
