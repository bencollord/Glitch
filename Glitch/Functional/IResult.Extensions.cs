using Glitch.Functional;
using Glitch.Functional.Results;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    using static Errors;

    public static class ResultExtensions
    {
        public static IResult<T, E> AsResult<T, E>(this IResult<T, E> source) => source;

        public static bool IsOkayAnd<T, E>(this IResult<T, E> source, Func<T, bool> predicate) => source.Match(predicate, false);

        public static bool IsErrorOr<T, E>(this IResult<T, E> source, Func<T, bool> predicate) => source.Match(predicate, true);


        // Monadic combinators
        public static IResult<TResult, E> And<T, E, TResult>(this IResult<T, E> source, IResult<TResult, E> other) => source.IsOkay ? other : source.Cast<TResult>();
        
        public static IResult<TResult, E> AndThen<T, E, TResult>(this IResult<T, E> source, Func<T, IResult<TResult, E>> bind) 
            => source.Match(bind, _ => source.Cast<TResult>());

        public static IResult<TResult, E> AndThen<T, E, TElement, TResult>(this IResult<T, E> source, Func<T, IResult<TElement, E>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(x => bind(x).Select(y => project(x, y)));

        public static IResult<TResult, E> Apply<T, E, TResult>(this IResult<T, E> source, IResult<Func<T, TResult>, E> function)
            => source.AndThen(x => function.Select(fn => fn(x)));

        public static IResult<TResult, E> Apply<T, E, TResult>(this IResult<Func<T, TResult>, E> source, IResult<T, E> value)
            => source.AndThen(fn => value.Select(x => fn(x)));

        public static IResult<T, E> Where<T, E>(this IResult<T, E> source, Func<T, bool> predicate)
            where E : ICanBeEmpty<E>
            => source.Guard(predicate, E.Empty);

        public static IResult<T, Unit> Where<T>(this IResult<T, Unit> source, Func<T, bool> predicate)
            => source.Guard(predicate, Unit.Value);

        public static IResult<T, E> Guard<T, E>(this IResult<T, E> source, bool condition, E error)
            => condition ? source : new Fail<T, E>(error);

        public static IResult<T, E> Guard<T, E>(this IResult<T, E> source, bool condition, Func<T, E> error)
            => source.AndThen(x => condition ? source : new Fail<T, E>(error(x)));

        public static IResult<T, E> Guard<T, E>(this IResult<T, E> source, Func<T, bool> predicate, E error)
            => source.Guard(source.IsOkayAnd(predicate), error);

        public static IResult<T, E> Guard<T, E>(this IResult<T, E> source, Func<T, bool> predicate, Func<T, E> error)
            => source.Guard(source.IsOkayAnd(predicate), error);

        public static IResult<T, EResult> Or<T, E, EResult>(this IResult<T, E> source, IResult<T, EResult> other)
            => source.IsError ? other : source.CastError<EResult>();

        public static IResult<T, EResult> OrElse<T, E, EResult>(this IResult<T, E> source, Func<E, IResult<T, EResult>> other)
            => source.Match(_ => source.CastError<EResult>(), other);

        public static IResult<Func<T2, TResult>, E> PartialSelect<T, E, T2, TResult>(this IResult<T, E> source, Func<T, T2, TResult> map)
            => source.Select(map.Curry());

        public static IResult<TResult, E> Zip<T, E, TOther, TResult>(this IResult<T, E> source, IResult<TOther, E> other, Func<T, TOther, TResult> zipper)
            => source.AndThen(x => other.Select(y => zipper(x, y)));

        public static IResult<(T First, TOther Second), E> Zip<T, E, TOther>(this IResult<T, E> source, IResult<TOther, E> other)
            => source.Zip(other, (x, y) => (x, y));

        // Match overloads
        public static Unit Match<T, E>(this IResult<T, E> source, Action<T> okay, Action<E> error) => source.Match(okay.Return(Unit.Value), error.Return(Unit.Value));

        public static TResult Match<T, E, TResult>(this IResult<T, E> source, Func<T, TResult> okay, Func<TResult> error)
            => source.Match(okay, _ => error());

        public static TResult Match<T, E, TResult>(this IResult<T, E> source, Func<T, TResult> okay, TResult error)
            => source.IsOkay ? okay(source.Unwrap()) : error; // Avoid unnecessary delegate

        // Natural transformations
        public static Option<T> OkayOrNone<T, E>(this IResult<T, E> source)
            => source is Option<T> opt
             ? opt // Special case since Option<T> implements IResult<T, Unit>
             : source.Match(Option<T>.Some, _ => Option<T>.None);

        public static Option<E> ErrorOrNone<T, E>(this IResult<T, E> source) => source.Match(_ => Option<E>.None, Some);

        /// <summary>
        /// Converts the <paramref name="source">result</paramref> into an <see cref="Expected{T}"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="Expected{T}"/> is in essence just a result where the failure case is constrained
        /// to derive from the <see cref="Error"/> type. This method could also be called 'ToExpected',
        /// but is named this way for symmetry with the 'ExpectOr*' methods.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Expected<T> Expect<T>(this IResult<T, Error> source) => source.Match(Expected.Okay, Expected.Fail<T>);

        /// <summary>
        /// Converts the <paramref name="source">result</paramref> into an <see cref="Expected{T}"/>,
        /// converting the <see cref="Exception"/> into an <see cref="Error"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Expected<T> Expect<T>(this IResult<T, Exception> source) => source.Match(Expected.Okay, ex => Expected.Fail<T>(ex));

        /// <summary>
        /// Converts the <paramref name="source"/> into an <see cref="Expected{T}"/>
        /// instance. If the result is in an error state, the <typeparamref name="E">error</typeparamref>
        /// value is converted into an <see cref="Error"/> using the same conversion rules as
        /// <see cref="Error.From{E}(E)"/>.
        /// </summary>
        /// <remarks>
        /// This is an experimental method that may be removed.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Expected<T> Expect<T, E>(this IResult<T, E> source)
            => source.Match(
                okay: Expected.Okay,
                error: e => Expected.Fail<T>(Error.From(e)));

        /// <summary>
        /// Wraps the value in a <see cref="Expected{T}" /> if it exists,
        /// otherwise returns an errored <see cref="Expected{T}" /> containing 
        /// the provided error.
        /// </summary>
        /// <remarks>
        /// Convenience overload for <see cref="OkayOr{E}(E)"/> specifically for <see cref="Error"/> values.
        /// </remarks>
        /// <param name="error"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> ExpectOr<T, E>(this IResult<T, E> source, Error error) => source.IsOkay ? Expected.Okay(source.Unwrap()) : Expected.Fail(error);

        /// <summary>
        /// Wraps the value in a <see cref="Expected{T}" /> if it exists,
        /// otherwise returns an errored <see cref="Expected{T}" /> containing 
        /// the result of the provided error function.
        /// </summary>
        /// <remarks>
        /// Convenience overload for <see cref="OkayOrElse{E}(Func{E})"/> specifically for <see cref="Error"/> values.
        /// </remarks>
        /// <param name="error"></param>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> ExpectOrElse<T, E>(this IResult<T, E> source, Func<Error> function) => source.ExpectOrElse(_ => function());

        /// <summary>
        /// Wraps the value in a <see cref="Expected{T}" /> if it exists,
        /// otherwise returns an errored <see cref="Expected{T}" /> containing 
        /// the result of the provided error function.
        /// </summary>
        /// <remarks>
        /// Convenience overload for <see cref="OkayOrElse{E}(Func{Unit, E})"/> specifically for <see cref="Error"/> values.
        /// </remarks>
        /// <param name="error"></param>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> ExpectOrElse<T, E>(this IResult<T, E> source, Func<E, Error> function) => source.Match(Expected.Okay, err => Expected.Fail(function(err)));


        public static IEnumerable<T> Iterate<T, E>(this IResult<T, E> source) => source.Match(x => [x], _ => Enumerable.Empty<T>());

        // Unwrapping
        public static T Unwrap<T, E>(this IResult<T, E> source) => source.Match(Identity, err => BadUnwrap(err).Throw<T>());

        public static E UnwrapError<T, E>(this IResult<T, E> source) => source.Match(val => BadUnwrapError(val).Throw<E>(), Identity);

        public static bool TryUnwrap<T, E>(this IResult<T, E> source, [NotNullWhen(true)] out T? result)
        {
            if (source.IsOkay)
            {
                result = source.Unwrap()!;
                return true;
            }

            result = default!;
            return false;
        }

        public static bool TryUnwrapError<T, E>(this IResult<T, E> source, [NotNullWhen(true)] out E result)
        {
            if (source.IsError)
            {
                result = source.UnwrapError()!;
                return true;
            }

            result = default!;
            return false;
        }

        public static T UnwrapOr<T, E>(this IResult<T, E> source, T fallback) => source.Match(Identity, fallback);
        public static T UnwrapOrElse<T, E>(this IResult<T, E> source, Func<E, T> fallback) => source.Match(Identity, fallback);
        public static T UnwrapOrElse<T, E>(this IResult<T, E> source, Func<T> fallback) => source.Match(Identity, fallback);
        public static T? UnwrapOrDefault<T, E>(this IResult<T, E> source) => source.UnwrapOr(default);

        public static E UnwrapErrorOr<T, E>(this IResult<T, E> source, E fallback) => source.IsError ? source.UnwrapError() : fallback;
        public static E UnwrapErrorOrElse<T, E>(this IResult<T, E> source, Func<T, E> fallback) => source.Match(fallback, Identity);
        public static E UnwrapErrorOrElse<T, E>(this IResult<T, E> source, Func<E> fallback) => source.Match(_ => fallback(), Identity);
        public static E? UnwrapErrorOrDefault<T, E>(this IResult<T, E> source) => source.UnwrapErrorOr(default);

        // Typed specializations
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IResult<T, E> Flatten<T, E>(this IResult<IResult<T, E>, E> nested)
            => nested.AndThen(n => n);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IResult<T, E> Select<T, E>(this IResult<bool, E> result, Func<Unit, T> @true, Func<Unit, T> @false)
            => result.Select(flag => flag ? @true(default) : @false(default));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IResult<T, E> Select<T, E>(this IResult<bool, E> result, Func<T> @true, Func<T> @false)
            => result.Select(flag => flag ? @true() : @false());

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Match<T, E>(this IResult<bool, E> result, Func<Unit, T> @true, Func<Unit, T> @false, Func<E, T> error)
            => result.Match(flag => flag ? @true(default) : @false(default), error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Match<T, E>(this IResult<bool, E> result, Func<T> @true, Func<T> @false, Func<E, T> error)
            => result.Match(flag => flag ? @true() : @false(), error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Unit Match<E>(this IResult<bool, E> result, Action @true, Action @false, Action<E> error)
            => result.Match(flag => flag ? @true.Return()() : @false.Return()(), error.Return());

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Unit Match<E>(this IResult<bool, E> result, Action<Unit> @true, Action<Unit> @false, Action<E> error)
            => result.Match(flag => flag ? @true.Return()(default) : @false.Return()(default), error.Return());

        /// <summary>
        /// Returns a the unwrapped values of all the successful results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Successes<T, E>(this IEnumerable<IResult<T, E>> results)
            => results.Where(r => r.IsOkay).Select(r => r.Unwrap());

        /// <summary>
        /// Returns a the unwrapped errors of all the faulted results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<E> Errors<T, E>(this IEnumerable<IResult<T, E>> results)
            => results.Where(r => r.IsError).Select(r => r.UnwrapError());

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (IEnumerable<T> Successes, IEnumerable<E> Errors) Partition<T, E>(this IEnumerable<IResult<T, E>> results)
            => (results.Successes(), results.Errors());
    }
}