using System.Diagnostics;
using System.Numerics;

namespace Glitch.Functional
{
    public static partial class FN
    {
        public static readonly OptionNone None = new();

        public static readonly Terminal End = new();

        public static NotSupportedException BadMatch()
        {
            var message = "If you've reached this message, a pattern match against a discriminated union reached the base case. " +
                          "This error is only intended to handle the C# compiler wanting switch expression matches to be exhaustive, " +
                          "so if you're seeing this message, you dun goofed";

            Debug.Fail(message);

            return new NotSupportedException(message);
        }

        public static Terminal Ignore<T>(T _) => default;

        public static Identity<T> Id<T>() where T : new() => Id<T>(new());

        public static Identity<T> Id<T>(T value) => value;

        public static Option<T> Some<T>() where T : new() => Some<T>(new());

        public static Option<T> Some<T>(T value) => Option<T>.Some(value);

        public static Option<T> Maybe<T>(T? value) => Option<T>.Maybe(value);

        public static Result<T> Okay<T>() where T : new() => Okay<T>(new());

        public static Result<T> Okay<T>(T value) => new Result.Okay<T>(value);

        public static Result<T> Fail<T>(Error error) => new Result.Fail<T>(error);

        public static Fallible<T> Try<T>(Func<Result<T>> function) => Fallible<T>.New(function);

        public static Fallible<T> Try<T>(Func<Terminal, Result<T>> function) => Fallible<T>.New(() => function(End));

        public static Fallible<T> Try<T>(Func<T> function) => Fallible<T>.New(function);

        public static Fallible<T> Try<T>(Func<Terminal, T> function) => Fallible<T>.New(() => function(End));

        public static Fallible<Terminal> Try(Action action) => Fallible<Terminal>.New(action.Return());

        public static Fallible<Terminal> Try(Action<Terminal> action) => Try(action.Return());

        public static Fallible<T> Try<T>(T value) => Fallible<T>.Okay(value);

        public static Fallible<T> Try<T>(Result<T> result) => Fallible<T>.New(result);

        public static Effect<TInput, Terminal> TryWith<TInput>(Action<TInput> function)
            => TryWith(function.Return());

        public static Effect<TInput, Terminal> TryWith<TInput>(Func<TInput, Terminal> function)
            => TryWith<TInput, Terminal>(function);

        public static Effect<TInput, TOutput> TryWith<TInput, TOutput>(Func<TInput, TOutput> function)
            => Effect.TryWith(function);

        public static Effect<TInput, TOutput> TryWith<TInput, TOutput>(Func<TInput, Result<TOutput>> function)
            => Effect.TryWith(function);

        public static Effect<T, T> Ask<T>() => Effect.Ask<T>();

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
