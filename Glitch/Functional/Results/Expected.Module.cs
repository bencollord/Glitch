namespace Glitch.Functional.Results
{
    // TODO Incomplete
    public static partial class Expected
    {
        public static Expected<T> Okay<T>() where T : new() => Okay(new T());

        public static Expected<T> Okay<T>(T value) => new Success<T>(value);

        public static Results.Failure<Error> Fail(Error error) => new(error);

        public static Results.Failure<Error> Fail(IEnumerable<Error> errors) => Fail(Error.New(errors));

        public static Expected<T> Fail<T>(Error error) => new Failure<T>(error);

        public static Expected<T> Fail<T>(IEnumerable<Error> errors) => new Failure<T>(Error.New(errors));

        public static bool IsOkay<T>(Expected<T> result) => result.IsOkay;

        public static bool IsFail<T>(Expected<T> result) => result.IsError;

        public static Expected<Unit> Guard(bool condition, Error error)
            => Guard(condition, Unit.Value, error);

        public static Expected<T> Guard<T>(bool condition, T value, Error error)
            => condition ? Okay(value) : Fail<T>(error);

        public static Expected<T> Guard<T>(bool condition, T value, Func<T, Error> error)
            => condition ? Okay(value) : Fail<T>(error(value));

        public static Expected<T> Guard<T>(Func<T, bool> predicate, T value, Error error)
            => predicate(value) ? Okay(value) : Fail<T>(error);

        public static Expected<T> Guard<T>(Func<T, bool> predicate, T value, Func<T, Error> error)
            => predicate(value) ? Okay(value) : Fail<T>(error(value));
    }
}
