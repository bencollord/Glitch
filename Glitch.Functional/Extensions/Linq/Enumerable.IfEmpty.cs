namespace Glitch.Functional.Extensions;

using static Option;
using static Result;

public static partial class LinqExtensions
{
    extension<TSource>(IEnumerable<TSource> source)
    {
        public IEnumerable<Option<TSource>> NoneIfEmpty() => source.Select(Some).DefaultIfEmpty(None);
        public IEnumerable<Result<TSource, E>> FailIfEmpty<E>(E value) => source.FailIfEmpty(Fail(value));
        public IEnumerable<Result<TSource, E>> FailIfEmpty<E>(Fail<E> fail) => source.Select(Okay).FailIfEmpty(fail);
    }

    extension<TSource>(IEnumerable<Okay<TSource>> source)
    {
        public IEnumerable<Option<TSource>> NoneIfEmpty() => source.Select(ok => ok.OrNone()).DefaultIfEmpty(None);
        public IEnumerable<Result<TSource, E>> FailIfEmpty<E>(E value) => source.FailIfEmpty(Fail(value));
        public IEnumerable<Result<TSource, E>> FailIfEmpty<E>(Fail<E> fail) => source.Select(ok => ok.OrFail<E>()).FailIfEmpty(fail);
    }

    extension<TSource, E>(IEnumerable<Result<TSource, E>> source)
    {
        public IEnumerable<Result<TSource, E>> FailIfEmpty(E value) => source.FailIfEmpty(Fail(value));
        public IEnumerable<Result<TSource, E>> FailIfEmpty(Fail<E> fail) => source.DefaultIfEmpty(fail);
    }
}
