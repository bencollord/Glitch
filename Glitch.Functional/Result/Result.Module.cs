namespace Glitch.Functional
{
    // TODO Incomplete
    public static partial class Result
    {
        public static bool IsOkay<T>(Result<T> result) => result.IsOkay;

        public static bool IsFail<T>(Result<T> result) => result.IsFail;

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
