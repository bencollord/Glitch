namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<TInput, TOutput> Okay<TInput, TOutput>(TOutput value) => Effect<TInput>.Okay(value);

        public static Effect<TInput, TOutput> Fail<TInput, TOutput>(Error error) => Effect<TInput>.Fail<TOutput>(error);

        public static Effect<TInput, Terminal> Fail<TInput>(Error error) => Effect<TInput, Terminal>.Fail(error);

        public static Effect<TInput, TOutput> Lift<TInput, TOutput>(Result<TOutput> result) => Effect<TInput>.Lift(result);

        public static Effect<TInput, TOutput> Lift<TInput, TOutput>(Func<TInput, Result<TOutput>> function) => Effect<TInput>.Lift(function);

        public static Effect<TInput, Terminal> Lift<TInput>(Action<TInput> action) => Effect<TInput>.Lift(action);

        public static Effect<TInput, TOutput> Lift<TInput, TOutput>(Func<TInput, TOutput> function) => Effect<TInput>.Lift(function);
    }

    public static class Effect<T>
    {
        public static Effect<T, TOutput> Okay<TOutput>(TOutput value) => new(_ => value);

        public static Effect<T, TOutput> Fail<TOutput>(Error error) => new(_ => error);

        public static Effect<T, TOutput> Lift<TOutput>(Result<TOutput> result) => new(_ => result);

        public static Effect<T, TOutput> Lift<TOutput>(Func<T, Result<TOutput>> function) => new(i => function(i));

        public static Effect<T, Terminal> Lift(Action<T> action) => Lift(action.Return());

        public static Effect<T, TOutput> Lift<TOutput>(Func<T, TOutput> function) => new(i => function(i));
    }
}
