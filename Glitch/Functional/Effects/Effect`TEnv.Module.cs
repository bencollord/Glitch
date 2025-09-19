using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<TEnv, TEnv> Ask<TEnv>() => Effect<TEnv, TEnv>.Lift(Identity);

        public static Effect<TEnv, T> Return<TEnv, T>(T value) => Effect<T>.Return(value);

        public static Effect<TEnv, T> Fail<TEnv, T>(Error error) => Effect<T>.Fail(error);

        public static Effect<TEnv, T> Return<TEnv, T>(Result<T> result) => Effect<TEnv, T>.Return(result);
        public static Effect<TEnv, T> Return<TEnv, T>(Expected<T, Error> result) => Effect<TEnv, T>.Return(result);
        public static Effect<TEnv, T> Return<TEnv, T, E>(Expected<T, E> result) where E : Error => Effect<TEnv, T>.Return(result);

        public static Effect<TEnv, T> Lift<TEnv, T>(Func<TEnv, Expected<T, Error>> function) => Lift<TEnv, T, Error>(function);
        public static Effect<TEnv, T> Lift<TEnv, T, E>(Func<TEnv, Expected<T, E>> function) where E : Error => Lift<TEnv, T>(x => function(x).Match(Expected.Okay<T, Error>, Expected.Fail<T, Error>));
        public static Effect<TEnv, T> Lift<TEnv, T>(Func<TEnv, Result<T>> function) => Effect<TEnv, T>.Lift(function);

        public static Effect<TEnv, Unit> Lift<TEnv>(Action<TEnv> action) => Effect<TEnv, Unit>.Lift(action.Return());
        public static Effect<TEnv, Unit> Lift<TEnv>(Func<TEnv, Unit> action) => Effect<TEnv, Unit>.Lift(action);
        public static Effect<TEnv, T> Lift<TEnv, T>(Func<TEnv, T> function) => Effect<TEnv, T>.Lift(function);

        // TODO These are the same as Lift, but I'm experimenting with a new naming convention.
        // Pick one and stick with it.
        public static Effect<TEnv, T> TryWith<TEnv, T, E>(Func<TEnv, Expected<T, E>> function) where E : Error => TryWith<TEnv, T>(x => function(x).Match(Expected.Okay<T, Error>, Expected.Fail<T, Error>));
        public static Effect<TEnv, T> TryWith<TEnv, T>(Func<TEnv, Result<T>> function) => Effect<TEnv, T>.Lift(function);

        public static Effect<TEnv, Unit> TryWith<TEnv>(Action<TEnv> action) => Effect<TEnv, Unit>.Lift(action.Return());
        public static Effect<TEnv, Unit> TryWith<TEnv>(Func<TEnv, Unit> action) => Effect<TEnv, Unit>.Lift(action);
        public static Effect<TEnv, T> TryWith<TEnv, T>(Func<TEnv, T> function) => Effect<TEnv, T>.Lift(function);
    }
}
