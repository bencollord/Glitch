namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<TInput, TInput> Ask<TInput>() => Effect<TInput>.Ask();

        public static Effect<TInput, TOutput> Okay<TInput, TOutput>(TOutput value) => Effect<TInput>.Okay(value);

        public static Effect<TInput, TOutput> Fail<TInput, TOutput>(Error error) => Effect<TInput>.Fail<TOutput>(error);

        public static Effect<TInput, Unit> Fail<TInput>(Error error) => Effect<TInput, Unit>.Fail(error);

        public static Effect<TInput, TOutput> New<TInput, TOutput>(Result<TOutput> result) => Effect<TInput>.Lift(result);

        public static Effect<TInput, TOutput> TryWith<TInput, TOutput>(Func<TInput, Result<TOutput>> function) => Effect<TInput>.New(function);

        public static Effect<TInput, Unit> New<TInput>(Action<TInput> action) => Effect<TInput>.New(action);

        public static Effect<TInput, TOutput> TryWith<TInput, TOutput>(Func<TInput, TOutput> function) => Effect<TInput>.New(function);
    }

    public static class Effect<T>
    {
        public static Effect<T, T> Ask() => Effect<T, T>.New(t => t);

        public static Effect<T, TOutput> Okay<TOutput>(TOutput value) => new(_ => value);

        public static Effect<T, TOutput> Fail<TOutput>(Error error) => new(_ => error);

        public static Effect<T, TOutput> Lift<TOutput>(Result<TOutput> result) => new(_ => result);

        public static Effect<T, TOutput> New<TOutput>(Func<T, Result<TOutput>> function) => new(i => function(i));

        public static Effect<T, Unit> New(Action<T> action) => New(action.Return());

        public static Effect<T, TOutput> New<TOutput>(Func<T, TOutput> function) => new(i => function(i));
    }
}
