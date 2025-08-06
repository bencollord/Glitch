namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<TInput, TInput> Ask<TInput>() => Effect<TInput, TInput>.Lift(Identity);

        public static Effect<TInput, T> Okay<TInput, T>(T value) => Effect<T>.Okay(value);

        public static Effect<TInput, T> Fail<TInput, T>(Error error) => Effect<T>.Fail(error);

        public static Effect<TInput, T> Return<TInput, T>(Result<T> result) => Effect<TInput, T>.Return(result);
        public static Effect<TInput, T> Return<TInput, T>(Result<T, Error> result) => Effect<TInput, T>.Return(result);
        public static Effect<TInput, T> Return<TInput, T>(IResult<T, Error> result) => Effect<TInput, T>.Return(result);

        public static Effect<TInput, T> Lift<TInput, T>(Func<TInput, Result<T>> function) => Effect<TInput, T>.Lift(function);

        public static Effect<TInput, Nothing> Lift<TInput>(Action<TInput> action) => Effect<TInput, Nothing>.Lift(action.Return());

        public static Effect<TInput, T> Lift<TInput, T>(Func<TInput, T> function) => Effect<TInput, T>.Lift(function);
    }
}
