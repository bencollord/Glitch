namespace Glitch.Functional;

public static partial class RecoverableExtensions
{
    public static Recoverable<T, E> Flatten<T, E>(this Recoverable<Recoverable<T, E>, E> source) => source.AndThen(Identity);
}