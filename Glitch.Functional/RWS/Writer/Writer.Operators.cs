namespace Glitch.Functional;

public static partial class WriterExtensions
{
    extension<W, T>(Writer<W, T> self)
        where W : IWritable<W>
    {
        public static Writer<W, T> operator >>>(Writer<W, T> x, Func<T, Writer<W, Unit>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<W, T, TResult>(Writer<W, T> self)
        where W : IWritable<W>
    {
        // Map
        public static Writer<W, TResult> operator *(Writer<W, T> x, Func<T, TResult> map) => x.Select(map);
        public static Writer<W, TResult> operator *(Func<T, TResult> map, Writer<W, T> x) => x.Select(map);

        // Apply
        public static Writer<W, TResult> operator *(Writer<W, T> x, Writer<W, Func<T, TResult>> apply) => x.Apply(apply);
        public static Writer<W, TResult> operator *(Writer<W, Func<T, TResult>> apply, Writer<W, T> x) => x.Apply(apply);

        // Bind
        public static Writer<W, TResult> operator >>>(Writer<W, T> x, Func<T, Writer<W, TResult>> bind) => x.AndThen(bind);

        public static Writer<W, TResult> operator >>>(Writer<W, T> x, Writer<W, TResult> y) => x.Then(y);
    }
}
