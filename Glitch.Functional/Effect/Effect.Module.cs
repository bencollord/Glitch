namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<T> Okay<T>(T value) => Okay<Unit, T>(value);

        public static Effect<T> Fail<T>(Error error) => Fail<Unit, T>(error);

        public static Effect<Unit> Fail(Error value) => Fail<Unit, Unit>(value);

        public static Effect<T> FromResult<T>(Result<T> result) => FromResult<Unit, T>(result);

        public static Effect<T> Lift<T>(Func<Result<T>> function) => Lift<Unit, T>(_ => function());

        public static Effect<T> Lift<T>(Func<T> function) => Lift(() => function());
    }
}
