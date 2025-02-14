namespace Glitch.Functional
{
    public static partial class FN
    {
        public static readonly OptionNone None = Option.None;

        public static Option<T> Some<T>(T value) => Option.Some(value);

        public static Option<T> Maybe<T>(T? value) => Option.Maybe(value);

        public static Result<T> Okay<T>(T value) => Result.Okay(value);

        public static Result<T> Fail<T>(Error error) => Result.Fail<T>(error);

        public static Try<T> Try<T>(Func<Result<T>> function) => Functional.Try.Lift(function);

        public static Try<T> Try<T>(Func<T> function) => Functional.Try.Lift(function);

        public static Try<Unit> Try(Action action) => Functional.Try.Lift(action.Return());

        public static Try<T> Try<T>(T value) => Functional.Try.Okay(value);

        public static Try<T> Try<T>(Result<T> result) => Functional.Try.Lift(result);

        public static Try<T> Try<T>(Error error) => Functional.Try.Fail<T>(error);

        public static OneOf<TLeft, TRight> Left<TLeft, TRight>(TLeft left) => new OneOf<TLeft, TRight>.Left(left);

        public static OneOf<TLeft, TRight> Right<TLeft, TRight>(TRight left) => new OneOf<TLeft, TRight>.Right(left);
    }
}
