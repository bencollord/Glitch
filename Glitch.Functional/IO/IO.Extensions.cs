namespace Glitch.Functional
{
    public static partial class IOExtensions
    {
        public static IO<TResult> Apply<T, TResult>(this IO<Func<T, TResult>> map, IO<T> value) => value.Apply(map);
    }
}