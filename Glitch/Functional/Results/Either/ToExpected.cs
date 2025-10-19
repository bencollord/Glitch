using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Either
    {
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
        public static Expected<T> Expect<T>(this IEither<T, Error> source) => source.Match(Expected.Okay, Expected.Fail<T>);

        /// <summary>
        /// Converts the <paramref name="source">result</paramref> into an <see cref="Expected{T}"/>,
        /// converting the <see cref="Exception"/> into an <see cref="Error"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Expected<T> Expect<T>(this IEither<T, Exception> source) => source.Match(Expected.Okay, ex => Expected.Fail<T>(ex));

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
        public static Expected<T> Expect<T, E>(this IEither<T, E> source)
            => source.Match(
                okay: Expected.Okay,
                error: e => Expected.Fail<T>(Error.From(e)));
       
        public static Expected<T> ExpectOr<T, E>(this IEither<T, E> source, Error error) => source.IsOkay ? Expected.Okay(source.Unwrap()) : Expected.Fail(error);
        
        public static Expected<T> ExpectOrElse<T, E>(this IEither<T, E> source, Func<Error> function) => source.ExpectOrElse(_ => function());
        
        public static Expected<T> ExpectOrElse<T, E>(this IEither<T, E> source, Func<E, Error> function) => source.Match(Expected.Okay, err => Expected.Fail(function(err)));
    }
}