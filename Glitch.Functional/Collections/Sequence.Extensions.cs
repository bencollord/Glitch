namespace Glitch.Functional.Collections;

public static partial class SequenceExtensions
{
    public static Sequence<T> Flatten<T>(this Sequence<Sequence<T>> source) => source.AndThen(Identity);
}
