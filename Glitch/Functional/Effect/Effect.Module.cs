using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<T> Return<T>(T value) => Return<Unit, T>(value);

        public static Effect<T> Fail<T>(Error error) => Fail<Unit, T>(error);

        public static Effect<Unit> Fail(Error value) => Fail<Unit, Unit>(value);

        public static Effect<T> Return<T>(Result<T> result) => Return<Unit, T>(result);
        
        public static Effect<T> Return<T>(Expected<T, Error> result) => Return<Unit, T>(result);

        public static Effect<T> Lift<T>(Func<Result<T>> function) => Lift<Unit, T>(_ => function());

        public static Effect<T> Lift<T>(Func<T> function) => Lift(() => function());
    }
}
