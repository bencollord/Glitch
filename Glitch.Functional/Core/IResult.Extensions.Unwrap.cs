using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    using static Glitch.Functional.Errors.Error;

    public static partial class ResultExtensions
    {
        extension<T, E>(IResult<T, E> source)
        {
            public T Unwrap() => source.Match(Identity, err => BadUnwrap(err).Throw<T>());

            public E UnwrapError() => source.Match(val => BadUnwrapError(val).Throw<E>(), Identity);

            public bool TryUnwrap([NotNullWhen(true)] out T? result)
            {
                if (source.IsOkay)
                {
                    result = source.Unwrap()!;
                    return true;
                }

                result = default!;
                return false;
            }

            public bool TryUnwrapError([NotNullWhen(true)] out E result)
            {
                if (source.IsFail)
                {
                    result = source.UnwrapError()!;
                    return true;
                }

                result = default!;
                return false;
            }

            public T UnwrapOr(T fallback) => source.Match(Identity, fallback);
            public T UnwrapOrElse(Func<E, T> fallback) => source.Match(Identity, fallback);
            public T UnwrapOrElse(Func<T> fallback) => source.Match(Identity, fallback);
            public E UnwrapErrorOr(E fallback) => source.IsFail ? source.UnwrapError() : fallback;
            public E UnwrapErrorOrElse(Func<T, E> fallback) => source.Match(fallback, Identity);
            public E UnwrapErrorOrElse(Func<E> fallback) => source.Match(_ => fallback(), Identity);

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            // A potential null return is an understood part of the method's contract
            public T? UnwrapOrDefault() => source.UnwrapOr(default);
            public E? UnwrapErrorOrDefault() => source.UnwrapErrorOr(default);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
}