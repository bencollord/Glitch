namespace Glitch.Functional
{
    public static partial class Fallible
    {
        public static Fallible<T> Okay<T>(T value) => new(() => value);

        public static Fallible<T> Fail<T>(Error error) => new(() => error);

        public static Fallible<Terminal> Fail(Error value) => Fail<Terminal>(value);

        public static Fallible<T> Lift<T>(Result<T> result) => new(() => result);

        public static Fallible<T> Lift<T>(Func<Result<T>> function) => new(function);

        public static Fallible<T> Try<T>(Func<T> function) => new(() => function());
    }
}
