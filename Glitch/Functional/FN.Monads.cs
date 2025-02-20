using System.Numerics;

namespace Glitch.Functional
{
    public static partial class FN
    {
        public static readonly OptionNone None = Option.None;

        public static Option<T> Some<T>(T value) => Option.Some(value);

        public static Option<T> Maybe<T>(T? value) => Option.Maybe(value);

        public static Result<T> Okay<T>(T value) => Result<T>.Okay(value);

        public static Result<T> Fail<T>(Error error) => Result<T>.Fail(error);

        public static Try<T> Try<T>(Func<Result<T>> function) => Functional.Try<T>.Lift(function);

        public static Try<T> Try<T>(Func<T> function) => Functional.Try<T>.Lift(function);

        public static Try<Unit> Try(Action action) => Functional.Try<Unit>.Lift(action.Return());

        public static Try<T> Try<T>(T value) => Functional.Try<T>.Okay(value);

        public static Try<T> Try<T>(Result<T> result) => Functional.Try<T>.Lift(result);

        public static Try<T> Try<T>(Error error) => Functional.Try<T>.Fail(error);

        public static OneOf.Left<TLeft> Left<TLeft>(TLeft left) => new(left);

        public static OneOf<TLeft, TRight> Left<TLeft, TRight>(TLeft left) => new OneOf.Left<TLeft, TRight>(left);

        public static OneOf.Right<TRight> Right<TRight>(TRight right) => new(right);
        
        public static OneOf<TLeft, TRight> Right<TLeft, TRight>(TRight right) => new OneOf.Right<TLeft, TRight>(right);
        
        public static OneOf<TLeft, TRight> OneOf<TLeft, TRight>(TLeft left) => new OneOf.Left<TLeft, TRight>(left);
        
        public static OneOf<TLeft, TRight> OneOf<TLeft, TRight>(TRight right) => new OneOf.Right<TLeft, TRight>(right);

        public static IEnumerable<T> Sequence<T>(IEnumerable<T> items) => items;

        public static IEnumerable<T> Sequence<T>(params T[] items) => items.AsEnumerable();

        public static IEnumerable<T> Range<T>(T start, T end)
            where T : IComparable<T>, IIncrementOperators<T>
        {
            for (T i = start; i.CompareTo(end) < 0; ++i)
            {
                yield return i;
            }
        }

        public static IEnumerable<T> Repeat<T>(T item, int times) => Enumerable.Repeat(item, times);

        public static IEnumerable<T> Repeat<T>(Func<T> func, int times)
            => Infinite(func).Take(times);

        public static IEnumerable<T> Infinite<T>(Func<T> func)
        {
            while (true)
            {
                yield return func();
            }
        }
    }
}
