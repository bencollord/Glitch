using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Sequence
    {
        public static Effect<Sequence<T>> Traverse<T>(this Sequence<Effect<T>> source)
            => source.Traverse(Identity);

        public static Effect<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Effect<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public static Effect<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Effect<T>> source, Func<T, int, TResult> traverse)
            => source.Map((s, i) => s.PartialSelect(traverse).Apply(i))
                     .Traverse();

        public static Effect<Sequence<TResult>> Traverse<T, TResult>(this Sequence<T> source, Func<T, int, Effect<TResult>> traverse)
            => source.Map((s, i) => traverse(s, i))
                     .Traverse();

        public static Result<Sequence<T>> Traverse<T>(this Sequence<Result<T>> source)
            => source.Traverse(Identity);

        public static Result<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Result<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public static Result<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Result<T>> source, Func<T, int, TResult> traverse)
            => source.Traverse((opt, idx) => opt.Select(o => traverse(o, idx)));

        public static Result<Sequence<TResult>> Traverse<T, TResult>(this Sequence<T> source, Func<T, int, Result<TResult>> traverse)
            => source.Map((s, i) => traverse(s, i))
                     .Traverse();

        public static Option<Sequence<T>> Traverse<T>(this Sequence<Option<T>> source)
            => source.Traverse(Identity);

        public static Option<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Option<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Map(traverse));

        public static Option<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Option<T>> source, Func<T, int, TResult> traverse)
            => source.Map((s, i) => s.PartialMap(traverse).Apply(i))
                     .Traverse();

        public static Option<Sequence<TResult>> Traverse<T, TResult>(this Sequence<T> source, Func<T, int, Option<TResult>> traverse)
            => source.Map((s, i) => traverse(s, i))
                     .Traverse();
    }
}
