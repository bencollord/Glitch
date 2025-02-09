namespace Glitch.Functional
{
    public static partial class FN
    {
        public static readonly OptionNone None = Option.None;

        public static Option<T> Some<T>(T value) => Option.Some(value);

        public static Option<T> Optional<T>(T? value) => Option.Optional(value);

        public static Result<T> Ok<T>(T value) => Result.Okay(value);

        public static Result<T> Fail<T>(Error error) => Result.Fail<T>(error);

        public static Try<T> Try<T>(Func<Result<T>> function) => Functional.Try.Lift(function);

        public static Try<T> Try<T>(Func<T> function) => Functional.Try.Lift(function);
    }
}
