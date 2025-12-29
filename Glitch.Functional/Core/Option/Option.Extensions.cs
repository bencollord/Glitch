using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional;

public static partial class OptionExtensions
{
    extension<T>(Option<T> source)
    {
        public bool IsSome([MaybeNullWhen(false)] out T value)
        {
            value = source.UnwrapOrDefault();
            return source.IsSome;
        }
    }

    extension<T>(Option<Option<T>> source)
    {
        public Option<T> Flatten() => source.AndThen(Identity);
    }
}