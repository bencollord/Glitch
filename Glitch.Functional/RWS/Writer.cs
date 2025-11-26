namespace Glitch.Functional;

[Monad]
public class Writer<W, T>
    where W : IWritable<W>
{
    private Func<W, WriteResult<W, T>> runner;

    internal Writer(Func<W, WriteResult<W, T>> runner)
    {
        this.runner = runner;
    }

    /// <summary>
    /// Returns a writer that simply returns the <paramref name="value"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Writer<W, T> Return(T value) => new(w => (value, w));

    /// <summary>
    /// Returns a new writer from a value and output pair.
    /// </summary>
    /// <remarks>
    /// This was taken from Paul Louth's LanguageExt library, which is
    /// the inspiration for a lot of these functional types. I'm not
    /// sure why he didn't make this method static, considering it discards
    /// the computation held by this instance without running it. I'm writing
    /// this library mostly as a learning experience, so I'll update these docs
    /// whenever I figure out why.
    /// </remarks>
    /// <param name="tuple"></param>
    /// <returns></returns>
    public Writer<W, T> Write(T value, W output)
        => new(w => (value, w + output));

    /// <summary>
    /// Returns a new writer from a value and output tuple.
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    public Writer<W, T> Write((T Value, W Output) tuple)
        => Write(tuple.Value, tuple.Output);

    /// <summary>
    /// Returns a new writer from a <see cref="WriteResult{W, T}"/>.
    /// Essentially an inverse operation to "Run."
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    public Writer<W, T> Write(WriteResult<W, T> result)
        => Write(result.Value, result.Output);

    /// <summary>
    /// Returns a writer that runs the current writer and returns
    /// a snapshot of its output along with its result value in a tuple.
    /// </summary>
    /// <typeparam name="TListen"></typeparam>
    /// <param name="listener"></param>
    /// <returns></returns>
    public Writer<W, (T Value, W Output)> Listen()
        => Listens(Identity);

    /// <summary>
    /// Returns a writer that runs the current writer and returns
    /// a snapshot of its output, transformed via the <paramref name="listener"/>
    /// function, along with its result value in a tuple.
    /// </summary>
    /// <typeparam name="TListen"></typeparam>
    /// <param name="listener"></param>
    /// <returns></returns>
    public Writer<W, (T Value, TListen Output)> Listens<TListen>(Func<W, TListen> listener)
        => new(w =>
        {
            var (value, output) = Run(w);

            return ((value, listener(output)), w + output);
        });

    /// <summary>
    /// Returns a writer that runs its output through the
    /// <paramref name="censor"/> function before appending
    /// it to the total.
    /// </summary>
    /// <param name="censor"></param>
    /// <returns></returns>
    public Writer<W, T> Censor(Func<W, W> censor)
        => new(w =>
        {
            var (value, output) = Run(w);

            return (value, w + censor(output));
        });

    public Writer<W, TResult> Select<TResult>(Func<T, TResult> map)
        => new(w =>
        {
            var (value, output) = Run(w);
            return (map(value), w + output);
        });

    public Writer<W, TOther> Then<TOther>(Writer<W, TOther> other)
         => new(w =>
         {
             var (_, output) = Run(w);
             return other.Run(w + output);
         });

    public Writer<W, TResult> Then<TOther, TResult>(Writer<W, TOther> other, Func<T, TOther, TResult> project)
        => new(w =>
        {
            var (value, output) = Run(w);
            var (nextValue, nextOutput) = other.Run(w + output);

            return (project(value, nextValue), nextOutput);
        });

    public Writer<W, T> Then(Writer<W, Unit> other)
         => new(w =>
         {
             var (value, output) = Run(w);
             var (_, nextOutput) = other.Run(w + output);
             return (value, nextOutput);
         });

    public Writer<W, TResult> AndThen<TResult>(Func<T, Writer<W, TResult>> bind)
         => new(w =>
         {
             var (value, output) = Run(w);
             return bind(value).Run(w + output);
         });

    public Writer<W, TResult> AndThen<TElement, TResult>(Func<T, Writer<W, TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(x => bind(x).Select(project.Curry(x)));

    public Writer<W, TResult> SelectMany<TElement, TResult>(Func<T, Writer<W, TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(bind, project);

    public WriteResult<W, T> Run() => Run(W.Empty);

    public WriteResult<W, T> Run(W runningTotal) => runner(runningTotal);

    public static implicit operator Writer<W, T>(T value) => Return(value);

    public static Writer<W, T> operator >>(Writer<W, T> x, Writer<W, T> y)
        => x.Then(y);

    public static Writer<W, T> operator >>(Writer<W, T> x, Writer<W, Unit> y)
        => x.Then(y);
}
