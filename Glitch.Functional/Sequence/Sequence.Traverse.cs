namespace Glitch.Functional
{
    public partial class Sequence<T>
    {
        public Result<Sequence<TResult>> Traverse<TResult>(Func<T, Result<TResult>> traverse)
            => this.Aggregate(
                Okay(Sequence<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Append(i)));

        public Result<Sequence<TResult>> Traverse<TResult>(Func<T, int, Result<TResult>> traverse)
            => Index().Traverse(pair => traverse(pair.Item, pair.Index));

        public Fallible<Sequence<TResult>> Traverse<TResult>(Func<T, Fallible<TResult>> traverse)
            => items.Aggregate(
                Fallible.Okay(Sequence<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Append(i)));

        public Fallible<Sequence<TResult>> Traverse<TResult>(Func<T, int, Fallible<TResult>> traverse)
            => Index().Traverse(pair => traverse(pair.Item, pair.Index));

        public Effect<TInput, Sequence<TResult>> Traverse<TInput, TResult>(Func<T, Effect<TInput, TResult>> traverse)
            => items.Aggregate(
                Effect<TInput>.Okay(Sequence<TResult>.Empty),
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
