
namespace Glitch.Functional.Results
{
    public static class ResultExtensions
    {
        public static IResult<TResult, E> And<T, E, TResult>(this IResult<T, E> source, IResult<TResult, E> other) => source.IsOkay ? other : source.Cast<TResult>();
        
        public static IResult<TResult, E> AndThen<T, E, TResult>(this IResult<T, E> source, Func<T, IResult<TResult, E>> bind) 
            => source.Match(bind, _ => source.Cast<TResult>());

        public static IResult<TResult, E> AndThen<T, E, TElement, TResult>(this IResult<T, E> source, Func<T, IResult<TElement, E>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(x => bind(x).Select(y => project(x, y)));

        public static IResult<TResult, E> Apply<T, E, TResult>(this IResult<T, E> source, IResult<Func<T, TResult>, E> function)
            => source.AndThen(x => function.Select(fn => fn(x)));

        public static IResult<TResult, E> Apply<T, E, TResult>(this IResult<Func<T, TResult>, E> source, IResult<T, E> value)
            => source.AndThen(fn => value.Select(x => fn(x)));

        public static IResult<TResult, EResult> Choose<T, E, TResult, EResult>(this IResult<T, E> source, Func<T, IResult<TResult, EResult>> okay, Func<E, IResult<TResult, EResult>> error)
            => source.Match(okay, error);

        public static IResult<T, E> Do<T, E>(this IResult<T, E> source, Action<T> action)
            => source.Do(action.Return(Unit.Value));

        public static IResult<T, E> Do<T, E>(this IResult<T, E> source, Func<T, Unit> action)
            => source.Select(x => action(x).Return(x));

        public static Option<E> ErrorOrNone<T, E>(this IResult<T, E> source) => source.Match(_ => Option<E>.None, Some);

        // UNDONE
        //public static IResult<T, E> Guard(this IResult<T, E> source, bool condition, E error);
        //public static IResult<T, E> Guard(this IResult<T, E> source, bool condition, Func<T, E> error);
        //public static IResult<T, E> Guard(this IResult<T, E> source, Func<T, bool> predicate, E error);
        //public static IResult<T, E> Guard(this IResult<T, E> source, Func<T, bool> predicate, Func<T, E> error);

        public static IResult<T, E> IfFail<T, E>(this IResult<T, E> source, Action action) => source.IfFail(_ => action());

        public static IResult<T, E> IfFail<T, E>(this IResult<T, E> source, Action<E> action)
            => source.IfFail(action.Return(Unit.Value));

        public static IResult<T, E> IfFail<T, E>(this IResult<T, E> source, Func<Unit> action) => source.IfFail(_ => action());

        public static IResult<T, E> IfFail<T, E>(this IResult<T, E> source, Func<E, Unit> action)
            => source.SelectError(e => action(e).Return(e));

        public static T IfFail<T, E>(this IResult<T, E> source, Func<E, T> fallback)
            => source.Match(Identity, fallback);

        public static T IfFail<T, E>(this IResult<T, E> source, Func<T> fallback) => source.IfFail(_ => fallback());
        public static T IfFail<T, E>(this IResult<T, E> source, T fallback) => source.IfFail(_ => fallback);

        public static bool IsErrorOr<T, E>(this IResult<T, E> source, Func<T, bool> predicate) => source.Match(predicate, true);

        public static bool IsOkayAnd<T, E>(this IResult<T, E> source, Func<T, bool> predicate) => source.Match(predicate, false);
        public static IEnumerable<T> Iterate<T, E>(this IResult<T, E> source) => source.Match(x => [x], _ => Enumerable.Empty<T>());

        public static Unit Match<T, E>(this IResult<T, E> source, Action<T> okay, Action<E> error) => source.Match(okay.Return(Unit.Value), error.Return(Unit.Value));

        public static TResult Match<T, E, TResult>(this IResult<T, E> source, Func<T, TResult> okay, Func<E, TResult> error)
        {
            if (source.IsOkay)
            {
                return okay(source.Unwrap());
            }

            if (source.IsError)
            {
                return error(source.UnwrapError());
            }

            throw new ArgumentException("Result was invalid. IsOkay and IsError both returned false.");
        }

        public static TResult Match<T, E, TResult>(this IResult<T, E> source, Func<T, TResult> okay, Func<TResult> error)
            => source.Match(okay, _ => error());

        public static TResult Match<T, E, TResult>(this IResult<T, E> source, Func<T, TResult> okay, TResult error)
            => source.Match(okay, _ => error);

        // TODO Move near ErrorOrNone
        public static Option<T> OkayOrNone<T, E>(this IResult<T, E> source) => source.Match(Option<T>.Some, _ => Option<T>.None);
        public static IResult<T, EResult> Or<T, E, EResult>(this IResult<T, E> source, IResult<T, EResult> other)
            => source.IsError ? other : source.CastError<EResult>();

        public static IResult<T, EResult> OrElse<T, E, EResult>(this IResult<T, E> source, Func<E, IResult<T, EResult>> other)
            => source.Match(_ => source.CastError<EResult>(), other);

        public static IResult<Func<T2, TResult>, E> PartialSelect<T, E, T2, TResult>(this IResult<T, E> source, Func<T, T2, TResult> map)
            => source.Select(map.Curry());

        public static bool TryUnwrap<T, E>(this IResult<T, E> source, out T result)
        {
            if (source.IsOkay)
            {
                result = source.Unwrap();
                return true;
            }

            result = default!;
            return false;
        }

        public static bool TryUnwrapError<T, E>(this IResult<T, E> source, out E result)
        {
            if (source.IsError)
            {
                result = source.UnwrapError();
                return true;
            }

            result = default!;
            return false;
        }

        // Aliases for IfFail
        public static T UnwrapOr<T, E>(this IResult<T, E> source, T fallback) => source.IfFail(fallback);
        public static T UnwrapOrElse<T, E>(this IResult<T, E> source, Func<T> fallback) => source.IfFail(fallback);
        public static T UnwrapOrElse<T, E>(this IResult<T, E> source, Func<E, T> fallback) => source.IfFail(fallback);
        
        public static E UnwrapErrorOr<T, E>(this IResult<T, E> source, E fallback) => source.TryUnwrapError(out E error) ? error : fallback;
        public static E UnwrapErrorOrElse<T, E>(this IResult<T, E> source, Func<E> fallback) => source.UnwrapErrorOrElse(_ => fallback());
        public static E UnwrapErrorOrElse<T, E>(this IResult<T, E> source, Func<T, E> fallback) => source.Match(fallback, Identity);

        public static IResult<TResult, E> Zip<T, E, TOther, TResult>(this IResult<T, E> source, IResult<TOther, E> other, Func<T, TOther, TResult> zipper)
            => source.AndThen(x => other.Select(y => zipper(x, y)));

        public static IResult<(T First, TOther Second), E> Zip<T, E, TOther>(this IResult<T, E> source, IResult<TOther, E> other)
            => source.Zip(other, (x, y) => (x, y));
    }
}