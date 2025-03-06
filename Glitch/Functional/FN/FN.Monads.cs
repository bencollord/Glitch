using System.Numerics;

namespace Glitch.Functional
{
    public static partial class FN
    {
        public static readonly OptionNone None = new();

        public static readonly Terminal Terminal = new();

        public static Terminal Ignore<T>(T _) => default;

        public static Identity<T> Id<T>(T value) => value;

        public static Option<T> Some<T>(T value) => Option<T>.Some(value);

        public static Option<T> Maybe<T>(T? value) => Option<T>.Maybe(value);

        public static Result<T> Okay<T>(T value) => new Result.Okay<T>(value);

        public static Result<T> Fail<T>(Error error) => new Result.Fail<T>(error);

        public static Fallible<T> Try<T>(Func<Result<T>> function) => Fallible<T>.Lift(function);

        public static Fallible<T> Try<T>(Func<T> function) => Fallible<T>.Lift(function);

        public static Fallible<Terminal> Try(Action action) => Fallible<Terminal>.Lift(action.Return());

        public static Fallible<T> Try<T>(T value) => Fallible<T>.Okay(value);

        public static Fallible<T> Try<T>(Result<T> result) => Fallible<T>.Lift(result);

        public static Fallible<T> Try<T>(Error error) => Fallible<T>.Fail(error);

        public static Fallible<T> TryCast<T>(object obj) => Try(() => (T)(dynamic)obj);

        public static OneOf.Left<TLeft> Left<TLeft>(TLeft left) => new(left);

        public static OneOf<TLeft, TRight> Left<TLeft, TRight>(TLeft left) => new OneOf.Left<TLeft, TRight>(left);

        public static OneOf.Right<TRight> Right<TRight>(TRight right) => new(right);
        
        public static OneOf<TLeft, TRight> Right<TLeft, TRight>(TRight right) => new OneOf.Right<TLeft, TRight>(right);
        
        public static OneOf<TLeft, TRight> OneOf<TLeft, TRight>(TLeft left) => new OneOf.Left<TLeft, TRight>(left);
        
        public static OneOf<TLeft, TRight> OneOf<TLeft, TRight>(TRight right) => new OneOf.Right<TLeft, TRight>(right);

        public static Sequence<T> Sequence<T>(IEnumerable<T> items) => items.AsSequence();

        public static Sequence<T> Sequence<T>(params T[] items) => items.AsSequence();

        public static Sequence<T> Range<T>(T start, T end)
            where T : IComparisonOperators<T, T, bool>, IIncrementOperators<T>
            => Functional.Sequence.Range(start, end);

        public static Sequence<T> Repeat<T>(T item, int times) => Functional.Sequence.Repeat(item, times);

        public static Sequence<T> Repeat<T>(Func<T> func, int times)
            => Functional.Sequence.Repeat(func, times);
    }
}
