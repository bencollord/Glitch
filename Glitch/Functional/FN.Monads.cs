using System.Numerics;

namespace Glitch.Functional
{
    public static partial class FN
    {
        public static readonly Unit Unit = Unit.Value;

        public static readonly OptionNone None = OptionNone.Value;

        public static Option<T> Some<T>(T value) => Option<T>.Some(value);

        public static Option<T> Maybe<T>(T? value) => Option<T>.Maybe(value);

        public static Result<T> Okay<T>(T value) => Result<T>.Okay(value);

        public static Result<T> Fail<T>(Error error) => Result<T>.Fail(error);

        public static Fallible<T> Try<T>(Func<Result<T>> function) => Functional.Fallible<T>.Lift(function);

        public static Fallible<T> Try<T>(Func<T> function) => Functional.Fallible<T>.Lift(function);

        public static Fallible<Unit> Try(Action action) => Functional.Fallible<Unit>.Lift(action.Return());

        public static Fallible<T> Try<T>(T value) => Functional.Fallible<T>.Okay(value);

        public static Fallible<T> Try<T>(Result<T> result) => Functional.Fallible<T>.Lift(result);

        public static Fallible<T> Try<T>(Error error) => Functional.Fallible<T>.Fail(error);

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
