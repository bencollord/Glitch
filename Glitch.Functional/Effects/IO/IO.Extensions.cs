namespace Glitch.Functional.Effects;

public static partial class IOExtensions
{
    public static IO<TResult> Apply<T, TResult>(this IO<Func<T, TResult>> map, IO<T> value) => value.Apply(map);

    public static IO<T> Flatten<T>(this IO<IO<T>> source) => source.AndThen(Identity);
}