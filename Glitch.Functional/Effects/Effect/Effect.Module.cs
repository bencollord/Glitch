using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        public static Effect<T> Return<T>(T value) => Return<Unit, T>(value);

        public static Effect<T> Fail<T>(Error error) => Fail<Unit, T>(error);

        public static Effect<Unit> Fail(Error value) => Fail<Unit, Unit>(value);

        public static Effect<T> Return<T>(Expected<T> result) => Return<Unit, T>(result);
        
        public static Effect<T> Return<T>(Result<T, Error> result) => Return<Unit, T>(result);

        public static Effect<T> Return<T, E>(Result<T, E> result) where E : Error => Effect<Unit, T>.Return(result);

        public static Effect<T> Lift<T>(Func<Expected<T>> function) => Lift<Unit, T>(_ => function());

        public static Effect<T> Lift<T>(Func<T> function) => Effect<T>.Lift(function);

        public static Effect<T> Lift<T>(Func<Result<T, Error>> function) => Lift(() => function().Match(Expected.Okay, Expected.Fail<T>));

        public static Effect<Unit> Lift(Action action) => Effect<Unit>.Lift(action.Return());

        // TODO These are the same as Lift and/or Return, but I'm experimenting with a new naming convention.
        // Pick one and stick with it.
        public static Effect<T> Try<T>(T value) => Return(value);
        public static Effect<T> Try<T>(Func<Expected<T>> function) => Lift<Unit, T>(_ => function());

        public static Effect<T> Try<T>(Func<T> function) => Effect<T>.Lift(function);

        public static Effect<T> Try<T>(Func<Result<T, Error>> function) => Lift(() => function().Match(Expected.Okay, Expected.Fail<T>));

        public static Effect<Unit> Try(Action action) => Effect<Unit>.Lift(action.Return());
    }
}
