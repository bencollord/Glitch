using System.Diagnostics;
using System.Numerics;

namespace Glitch.Functional
{
    public static partial class FN
    {
        public static readonly OptionNone None = new();

        public static readonly Unit End = new();

        public static NotSupportedException BadMatchException()
        {
            var message = "If you've reached this message, a pattern match against a discriminated union reached the base case. " +
                          "This error is only intended to handle the C# compiler wanting switch expression matches to be exhaustive, " +
                          "so if you're seeing this message, you dun goofed";

            Debug.Fail(message);

            return new NotSupportedException(message);
        }

        public static Unit Ignore<T>(T _) => default;

        public static Option<T> Some<T>() where T : new() => Some<T>(new());

        public static Option<T> Some<T>(T value) => Option<T>.Some(value);

        public static Option<T> Maybe<T>(T? value) => Option<T>.Maybe(value);

        public static Success<T> Okay<T>() where T : new() => Okay<T>(new());

        public static Success<T> Okay<T>(T value) => new(value);

        public static Failure<T> Fail<T>(T error) => new(error);

        public static Fallible<T> Try<T>(Func<Result<T>> function) => Fallible<T>.New(function);

        public static Fallible<T> Try<T>(Func<Unit, Result<T>> function) => Fallible<T>.New(() => function(End));

        public static Fallible<T> Try<T>(Func<T> function) => Fallible<T>.New(function);

        public static Fallible<T> Try<T>(Func<Unit, T> function) => Fallible<T>.New(() => function(End));

        public static Fallible<Unit> Try(Action action) => Fallible<Unit>.New(action.Return());

        public static Fallible<Unit> Try(Action<Unit> action) => Try(action.Return());

        public static Fallible<T> Try<T>(T value) => Fallible<T>.Okay(value);

        public static Fallible<T> Try<T>(Result<T> result) => Fallible<T>.New(result);

        public static Effect<TInput, Unit> TryWith<TInput>(Action<TInput> function)
            => TryWith(function.Return());

        public static Effect<TInput, Unit> TryWith<TInput>(Func<TInput, Unit> function)
            => TryWith<TInput, Unit>(function);

        public static Effect<TInput, TOutput> TryWith<TInput, TOutput>(Func<TInput, TOutput> function)
            => Effect.TryWith(function);

        public static Effect<TInput, TOutput> TryWith<TInput, TOutput>(Func<TInput, Result<TOutput>> function)
            => Effect.TryWith(function);

        public static Effect<T, T> Ask<T>() => Effect.Ask<T>();

        public static Fallible<T> TryCast<T>(object obj) => Try(() => (T)(dynamic)obj);

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
