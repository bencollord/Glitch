
namespace Glitch.Functional.Effects;

public static partial class LazyExtensions
{
    extension<T>(Lazy<T> source)
    {
        public Lazy<TResult> Select<TResult>(Func<T, TResult> map) =>
            new Lazy<TResult>(() => map(source.Value));

        public Lazy<TResult> Apply<TResult>(Lazy<Func<T, TResult>> function)
            => source.AndThen(v => function.Select(fn => fn(v)));

        public Lazy<TResult> AndThen<TResult>(Func<T, Lazy<TResult>> bind) =>
            new Lazy<TResult>(() => bind(source.Value).Value);


        public Lazy<TResult> AndThen<TElement, TResult>(Func<T, Lazy<TElement>> bind, Func<T, TElement, TResult> project) =>
            new Lazy<TResult>(() => project.Apply(source.Value) << bind(source.Value).Value);

        public Lazy<Option<T>> Where(Func<T, bool> predicate) =>
            new Lazy<Option<T>>(() => predicate(source.Value) ? Option.Some(source.Value) : Option.None);

        public Lazy<TResult> SelectMany<TResult>(Func<T, Lazy<TResult>> bind)
         => source.AndThen(bind);

        public Lazy<TResult> SelectMany<TElement, TResult>(Func<T, Lazy<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));
    }

    extension<T>(Lazy<Option<T>> source)
    {
        // Prevent multiple calls to Where from nesting Option<Option<Option<Option<Option<Option<TLikeThisForever>>>>>>
        public Lazy<Option<T>> Where(Func<T, bool> predicate) =>
            new Lazy<Option<T>>(() => source.Value.Where(predicate));
    }
}
