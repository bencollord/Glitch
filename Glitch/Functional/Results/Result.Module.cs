namespace Glitch.Functional.Results
{
    // TODO Incomplete
    public static partial class Result
    {
        public static Result<T> Okay<T>() where T : new() => Okay(new T());

        public static Result<T> Okay<T>(T value) => new Success<T>(value);

        public static Functional.Failure<Error> Fail(Error error) => new Functional.Failure<Error>(error);

        public static Result<T> Fail<T>(Error error) => new Failure<T>(error);

        public static bool IsOkay<T>(Result<T> result) => result.IsOkay;

        public static bool IsFail<T>(Result<T> result) => result.IsError;

        public static Result<Unit> Guard(bool condition, Error error)
            => Guard(condition, Unit.Value, error);

        public static Result<T> Guard<T>(bool condition, T value, Error error)
            => condition ? Okay(value) : Fail<T>(error);

        public static Result<T> Guard<T>(bool condition, T value, Func<T, Error> error)
            => condition ? Okay(value) : Fail<T>(error(value));

        public static Result<T> Guard<T>(Func<T, bool> predicate, T value, Error error)
            => predicate(value) ? Okay(value) : Fail<T>(error);

        public static Result<T> Guard<T>(Func<T, bool> predicate, T value, Func<T, Error> error)
            => predicate(value) ? Okay(value) : Fail<T>(error(value));
    }
}
