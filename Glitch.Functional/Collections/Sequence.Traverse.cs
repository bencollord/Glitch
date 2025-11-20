using Glitch.Functional.Core;
using Glitch.Functional.Effects;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Collections
{
    public partial class Sequence<T>
    {
        public Expected<Sequence<TResult>> Traverse<TResult>(Func<T, Expected<TResult>> traverse)
            => this.Aggregate(
                Expected.Okay(Sequence<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Append(i)));

        public Expected<Sequence<TResult>> Traverse<TResult>(Func<T, int, Expected<TResult>> traverse)
            => Index().Traverse(pair => traverse(pair.Item, pair.Index));

        public Effect<Sequence<TResult>> Traverse<TResult>(Func<T, Effect<TResult>> traverse)
            => items.Aggregate(
                Effect.Return(Sequence<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Append(i)));

        public Effect<Sequence<TResult>> Traverse<TResult>(Func<T, int, Effect<TResult>> traverse)
            => Index().Traverse(pair => traverse(pair.Item, pair.Index));

        public Effect<TInput, Sequence<TResult>> Traverse<TInput, TResult>(Func<T, Effect<TInput, TResult>> traverse)
            => items.Aggregate(
                Effect.Return(Sequence<TResult>.Empty).With<TInput>(),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Append(i)));

        public Effect<TInput, Sequence<TResult>> Traverse<TInput, TResult>(Func<T, int, Effect<TInput, TResult>> traverse)
            => Index().Traverse(pair => traverse(pair.Item, pair.Index));


        public Option<Sequence<TResult>> Traverse<TResult>(Func<T, Option<TResult>> traverse)
            => items.Aggregate(
                Option.Some(Sequence<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Append(i)));

        public Option<Sequence<TResult>> Traverse<TResult>(Func<T, int, Option<TResult>> traverse)
            => Index().Traverse(pair => traverse(pair.Item, pair.Index));
    }
}
