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
            => source.Select((s, i) => s.PartialSelect(traverse).Apply(i))
                     .Traverse();

        
        public static Expected<Sequence<T>> Traverse<T>(this Sequence<Expected<T>> source)
            => source.Traverse(Identity);

        public static Expected<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Expected<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public static Expected<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Expected<T>> source, Func<T, int, TResult> traverse)
            => source.Traverse((opt, idx) => opt.Select(o => traverse(o, idx)));

        public static Option<Sequence<T>> Traverse<T>(this Sequence<Option<T>> source)
            => source.Traverse(Identity);

        public static Option<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Option<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public static Option<Sequence<TResult>> Traverse<T, TResult>(this Sequence<Option<T>> source, Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.PartialSelect(traverse).Apply(i))
                     .Traverse();
    }
}
