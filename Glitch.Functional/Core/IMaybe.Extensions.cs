using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional;

using static Glitch.Functional.Errors.Error;

public static partial class MaybeExtensions
{
    extension<T>(IMaybe<T> source)
    {
        // UNDONE Decide on naming convention. GetValue* or Unwrap*?

        public T Unwrap() => source.Match(
            Identity,
            () => BadUnwrap("Attempted to unwrap an element with no value").Throw<T>());

        public bool TryUnwrap([MaybeNullWhen(false)] out T result)
        {
            if (source.HasValue)
            {
                result = source.Unwrap()!;
                return true;
            }

            result = default;
            return false;
        }

        public T UnwrapOr(T fallback) => source.HasValue ? source.Unwrap() : fallback;

        public T UnwrapOrElse(Func<T> fallback) => source.Match(Identity, fallback);

        public T UnwrapOrElse(Func<Unit, T> fallback) => source.Match(Identity, () => fallback(Unit.Value));

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        // A potential null return is an understood part of the method's contract
        public T? UnwrapOrDefault() => source.UnwrapOr(default);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
    }
}