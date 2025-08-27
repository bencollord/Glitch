using Glitch.Functional.Results;

namespace Glitch.Functional.Results
{
    // TODO Incomplete
    public static partial class Expected
    {
        public static Expected<T, E> Okay<T, E>(T value) => new Success<T, E>(value);
        
        public static Expected<T, E> Fail<T, E>(E error) => new Failure<T, E>(error);

        public static bool IsOkay<T, E>(Expected<T, E> result) => result.IsOkay;

        public static bool IsFail<T, E>(Expected<T, E> result) => result.IsError;

        public static Expected<T, E> Guard<T, E>(bool condition, T value, E error)
            => condition ? new Success<T, E>(value) : new Failure<T, E>(error);

        public static Expected<T, E> Guard<T, E>(bool condition, T value, Func<T, E> error)
            => condition ? new Success<T, E>(value) : new Failure<T, E>(error(value));

        public static Expected<T, E> Guard<T, E>(Func<T, bool> predicate, T value, E error)
            => predicate(value) ? new Success<T, E>(value) : new Failure<T, E>(error);
    }
}