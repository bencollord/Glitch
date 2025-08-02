namespace Glitch.Functional
{
    public partial class Sequence<T>
    {
        public Result<Sequence<TResult>> Traverse<TResult>(Func<T, Result<TResult>> traverse)
            => this.Aggregate(
                Result.Okay(Sequence<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Append(i)));

        public Result<Sequence<TResult>> Traverse<TResult>(Func<T, int, Result<TResult>> traverse)
            => Index().Traverse(pair => traverse(pair.Item, pair.Index));

        public Effect<Sequence<TResult>> Traverse<TResult>(Func<T, Effect<TResult>> traverse)
            => items.Aggregate(
                Effect.Okay(Sequence<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Append(i)));

        public Effect<Sequence<TResult>> Traverse<TResult>(Func<T, int, Effect<TResult>> traverse)
            => Index().Traverse(pair => traverse(pair.Item, pair.Index));

        public Effect<TInput, Sequence<TResult>> Traverse<TInput, TResult>(Func<T, Effect<TInput, TResult>> traverse)
            => items.Aggregate(
                Effect.Okay(Sequence<TResult>.Empty).WithInput<TInput>(),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Append(i)));

        public Effect<TInput, Sequence<TResult>> Traverse<TInput, TResult>(Func<T, int, Effect<TInput, TResult>> traverse)
            => Index().Traverse(pair => traverse(pair.Item, pair.Index));


        public Option<Sequence<TResult>> Traverse<TResult>(Func<T, Option<TResult>> traverse)
            => items.Aggregate(
                Some(Sequence<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Append(i)));

        public Option<Sequence<TResult>> Traverse<TResult>(Func<T, int, Option<TResult>> traverse)
            => Index().Traverse(pair => traverse(pair.Item, pair.Index));
    }
}
