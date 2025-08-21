namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<T> Okay<T>(T value) => Okay<Nothing, T>(value);

        public static Effect<T> Fail<T>(Error error) => Fail<Nothing, T>(error);

        public static Effect<Nothing> Fail(Error value) => Fail<Nothing, Nothing>(value);

        public static Effect<T> Return<T>(Result<T> result) => Return<Nothing, T>(result);
        
        public static Effect<T> Return<T>(Result<T, Error> result) => Return<Nothing, T>(result);

        public static Effect<T> Lift<T>(Func<Result<T>> function) => Lift<Nothing, T>(_ => function());

        public static Effect<T> Lift<T>(Func<T> function) => Lift(() => function());
    }
}
