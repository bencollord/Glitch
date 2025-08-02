namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<TInput, TInput> Ask<TInput>() => Effect<TInput, TInput>.Lift(Identity);

        public static Effect<TInput, T> Okay<TInput, T>(T value) => Effect<T>.Okay(value);

        public static Effect<TInput, T> Fail<TInput, T>(Error error) => Effect<T>.Fail(error);

        public static Effect<TInput, T> FromResult<TInput, T>(Result<T> result) => Effect<TInput, T>.Lift(result);

        public static Effect<TInput, T> Lift<TInput, T>(Func<TInput, Result<T>> function) => Effect<TInput, T>.Lift(function);

        public static Effect<TInput, Unit> Lift<TInput>(Action<TInput> action) => Effect<TInput, Unit>.Lift(action.Return());

        public static Effect<TInput, T> Lift<TInput, T>(Func<TInput, T> function) => Effect<TInput, T>.Lift(function);
    }
}
