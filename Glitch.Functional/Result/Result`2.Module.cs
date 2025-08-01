namespace Glitch.Functional
{
    // TODO Incomplete
    public static partial class Result
    {
        public static Result<T, E> Okay<T, E>(T value) => new Success<T, E>(value);
        
        public static Result<T, E> Fail<T, E>(E error) => new Failure<T, E>(error);

        public static bool IsOkay<T, E>(Result<T, E> result) => result.IsOkay;

        public static bool IsFail<T, E>(Result<T, E> result) => result.IsFail;

        public static Result<T, E> Guard<T, E>(bool condition, T value, E error)
            => condition ? new Success<T, E>(value) : new Failure<T, E>(error);

        public static Result<T, E> Guard<T, E>(bool condition, T value, Func<T, E> error)
            => condition ? new Success<T, E>(value) : new Failure<T, E>(error(value));

        public static Result<T, E> Guard<T, E>(Func<T, bool> predicate, T value, E error)
            => predicate(value) ? new Success<T, E>(value) : new Failure<T, E>(error);
    }
}