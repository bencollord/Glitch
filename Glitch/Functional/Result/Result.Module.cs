namespace Glitch.Functional
{
    // TODO Incomplete
    public static partial class Result
    {
        public static bool IsOkay<T>(Result<T> result) => result.IsOkay;

        public static bool IsFail<T>(Result<T> result) => result.IsFail;

        public static Result<Terminal> Guard(bool condition, Error error)
            => Guard(Terminal.Value, condition, error);

        public static Result<T> Guard<T>(T value, bool condition, Error error)
            => condition ? Okay(value) : Fail<T>(error);

        public static Result<T> Guard<T>(T value, bool condition, Func<T, Error> error)
            => condition ? Okay(value) : Fail<T>(error(value));

        public static Result<T> Guard<T>(T value, Func<T, bool> predicate, Error error)
            => predicate(value) ? Okay(value) : Fail<T>(error);

        public static Result<T> Guard<T>(T value, Func<T, bool> predicate, Func<T, Error> error)
            => predicate(value) ? Okay(value) : Fail<T>(error(value));
    }
}
