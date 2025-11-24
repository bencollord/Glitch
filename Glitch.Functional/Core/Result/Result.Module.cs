namespace Glitch.Functional
{
    public static partial class Result
    {
        public static Okay<T> Okay<T>() 
            where T : new() 
            => Okay(new T());

        public static Okay<T> Okay<T>(T value) => new(value);

        public static Result<T, E> Okay<T, E>(T value) => new Result<T, E>.Okay(value);
        
        public static Fail<E> Fail<E>(E error) => new(error);

        public static Result<T, E> Fail<T, E>(E error) => new Result<T, E>.Fail(error);

        public static bool IsOkay<T, E>(Result<T, E> result) => result.IsOkay;

        public static bool IsFail<T, E>(Result<T, E> result) => result.IsFail;

        public static Result<Unit, E> Guard<E>(bool condition, E error)
            => condition ? new Result<Unit, E>.Okay(Unit.Value) : new Result<Unit, E>.Fail(error);

        public static Result<Unit, E> Guard<E>(bool condition, Func<Unit, E> error)
            => condition ? new Result<Unit, E>.Okay(Unit.Value) : new Result<Unit, E>.Fail(error(Unit.Value));

        public static Result<Unit, E> Guard<E>(Func<Unit, bool> predicate, E error)
            => predicate(Unit.Value) ? new Result<Unit, E>.Okay(Unit.Value) : new Result<Unit, E>.Fail(error);

        public static Result<T, E> Guard<T, E>(bool condition, T value, E error)
            => condition ? new Result<T, E>.Okay(value) : new Result<T, E>.Fail(error);

        public static Result<T, E> Guard<T, E>(bool condition, T value, Func<T, E> error)
            => condition ? new Result<T, E>.Okay(value) : new Result<T, E>.Fail(error(value));

        public static Result<T, E> Guard<T, E>(Func<T, bool> predicate, T value, E error)
            => predicate(value) ? new Result<T, E>.Okay(value) : new Result<T, E>.Fail(error);
    }
}