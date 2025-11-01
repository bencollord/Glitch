using Glitch.Functional.Results;

namespace Glitch.Functional.Results
{
    // TODO Incomplete
    public static partial class Result
    {
        public static Okay<T> Okay<T>() 
            where T : new() 
            => Okay(new T());

        public static Okay<T> Okay<T>(T value) => new(value);

        public static Result<T, E> Okay<T, E>(T value) => new Okay<T, E>(value);
        
        public static Fail<E> Fail<E>(E error) => new(error);

        public static Result<T, E> Fail<T, E>(E error) => new Fail<T, E>(error);

        public static bool IsOkay<T, E>(Result<T, E> result) => result.IsOkay;

        public static bool IsFail<T, E>(Result<T, E> result) => result.IsError;

        public static Result<Unit, E> Guard<E>(bool condition, E error)
            => condition ? new Okay<Unit, E>(Unit.Value) : new Fail<Unit, E>(error);

        public static Result<Unit, E> Guard<E>(bool condition, Func<Unit, E> error)
            => condition ? new Okay<Unit, E>(Unit.Value) : new Fail<Unit, E>(error(Unit.Value));

        public static Result<Unit, E> Guard<E>(Func<Unit, bool> predicate, E error)
            => predicate(Unit.Value) ? new Okay<Unit, E>(Unit.Value) : new Fail<Unit, E>(error);

        public static Result<T, E> Guard<T, E>(bool condition, T value, E error)
            => condition ? new Okay<T, E>(value) : new Fail<T, E>(error);

        public static Result<T, E> Guard<T, E>(bool condition, T value, Func<T, E> error)
            => condition ? new Okay<T, E>(value) : new Fail<T, E>(error(value));

        public static Result<T, E> Guard<T, E>(Func<T, bool> predicate, T value, E error)
            => predicate(value) ? new Okay<T, E>(value) : new Fail<T, E>(error);
    }
}