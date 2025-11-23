using Glitch.Functional;

namespace Glitch.Functional.Errors
{
    // TODO Incomplete
    public static partial class Expected
    {
        public static Expected<T> Okay<T>() where T : new() => Okay(new T());

        public static Expected<T> Okay<T>(T value) => new Okay<T>(value);

        public static Fail<Error> Fail(Error error) => new(error);

        public static Fail<Error> Fail(IEnumerable<Error> errors) => Fail(Error.New(errors));

        public static Expected<T> Fail<T>(Error error) => new Fail<T, Error>(error);

        public static Expected<T> Fail<T>(IEnumerable<Error> errors) => new Fail<T, Error>(Error.New(errors));

        public static Expected<T> From<T, E>(Result<T, E> result) where E : Error => Expected<T>.From(result);

        public static bool IsOkay<T>(Expected<T> result) => result.IsOkay;

        public static bool IsFail<T>(Expected<T> result) => result.IsFail;

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
