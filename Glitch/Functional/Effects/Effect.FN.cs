using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class FN
    {
        public static Effect<T> Try<T>(Func<Expected<T>> function) => Effect<T>.Lift(function);

        public static Effect<T> Try<T>(Func<Unit, Expected<T>> function) => Effect<T>.Lift(() => function(Nothing));

        public static Effect<T> Try<T>(Func<T> function) => Effect<T>.Lift(function);

        public static Effect<T> Try<T>(Func<Unit, T> function) => Effect<T>.Lift(() => function(Nothing));

        public static Effect<Unit> Try(Action action) => Effect<Unit>.Lift(action.Return());

        public static Effect<Unit> Try(Action<Unit> action) => Try(action.Return());

        public static Effect<T> Try<T>(T value) => Effect<T>.Return(value);

        public static Effect<T> Try<T>(Expected<T> result) => Effect<T>.Return(result);

        public static Effect<TInput, Unit> TryWith<TInput>(Action<TInput> function)
            => TryWith(function.Return());

        public static Effect<TInput, Unit> TryWith<TInput>(Func<TInput, Unit> function)
            => TryWith<TInput, Unit>(function);

        public static Effect<TInput, TOutput> TryWith<TInput, TOutput>(Func<TInput, TOutput> function)
            => Effect.Lift(function);

        public static Effect<TInput, TOutput> TryWith<TInput, TOutput>(Func<TInput, Expected<TOutput>> function)
            => Effect.Lift(function);

        public static Effect<T, T> Ask<T>() => Effect.Ask<T>();
    }
}
