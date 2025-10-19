using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Either
    {
        public static IEither<TResult, E> And<T, E, TResult>(this IEither<T, E> source, IEither<TResult, E> other) => source.IsOkay ? other : source.Cast<TResult>();
       
        public static IEither<TResult, E> AndThen<T, E, TResult>(this IEither<T, E> source, Func<T, IEither<TResult, E>> bind) 
            => source.Match(bind, _ => source.Cast<TResult>());
        
        public static IEither<TResult, E> AndThen<T, E, TElement, TResult>(this IEither<T, E> source, Func<T, IEither<TElement, E>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(x => bind(x).Select(y => project(x, y)));

        public static IEither<T, EResult> Or<T, E, EResult>(this IEither<T, E> source, IEither<T, EResult> other)
            => source.IsError ? other : source.CastError<EResult>();
       
        public static IEither<T, EResult> OrElse<T, E, EResult>(this IEither<T, E> source, Func<E, IEither<T, EResult>> other)
            => source.Match(_ => source.CastError<EResult>(), other);
        
        public static IEither<T, EResult> OrElse<T, E, EElement, EResult>(this IEither<T, E> source, Func<E, IEither<T, EElement>> other, Func<E, EElement, EResult> project)
            => source.OrElse(e => other(e).SelectError(x => project(e, x)));
    }
}