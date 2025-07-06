namespace Glitch.Functional
{
    public static partial class Sequence
    {
        public static Fallible<Sequence<T>> Traverse<T>(this Sequence<Fallible<T>> source)
            => source.Traverse(Identity);

        public static Fallible<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Fallible<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Map(traverse));

        public static Fallible<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Fallible<T>> source, Func<T, int, TResult> traverse)
            => source.Map((s, i) => s.PartialMap(traverse).Apply(i))
                     .Traverse();

        public static Fallible<Sequence<TResult>> Traverse<T, TResult>(this Sequence<T> source, Func<T, int, Fallible<TResult>> traverse)
            => source.Map((s, i) => traverse(s, i))
                     .Traverse();

        public static Result<Sequence<T>> Traverse<T>(this Sequence<Result<T>> source)
            => source.Traverse(Identity);

        public static Result<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Result<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Map(traverse));

        public static Result<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Result<T>> source, Func<T, int, TResult> traverse)
            => source.Traverse((opt, idx) => opt.Map(o => traverse(o, idx)));

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
