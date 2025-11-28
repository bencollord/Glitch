namespace Glitch.Functional.Effects;

public static class Writer
{
    /// <inheritdoc cref="Writer{W, T}.Return(T)"/>
    public static Writer<W, T> Return<W, T>(T value) where W : IWritable<W> => Writer<W, T>.Return(value);

    /// <inheritdoc cref="Writer{W, T}.Write(T, W)"/>
    public static Writer<W, T> Write<W, T>(T value, W output)
        where W : IWritable<W>
        => new(w => (value, w + output));

    /// <inheritdoc cref="Writer{W, T}.Write(ValueTuple{T, W})"/>
    public static Writer<W, T> Write<W, T>((T Value, W Output) tuple)
        where W : IWritable<W>
        => Write(tuple.Value, tuple.Output);

    /// <inheritdoc cref="Writer{W, T}.Write(WriteResult{W, T})"/>
    public static Writer<W, T> Write<W, T>(WriteResult<W, T> result)
        where W : IWritable<W>
        => Write(result.Value, result.Output);

    /// <inheritdoc cref="Writer{W}.Tell(W)"/>
    public static Writer<W, Unit> Tell<W>(W item)
        where W : IWritable<W>
        => new(w => (Unit.Value, w + item));


    /// <inheritdoc cref="Writer{W}.Pass{T}(Writer{W, ValueTuple{T, Func{W, W}}})"/>
    public static Writer<W, T> Pass<W, T>(Writer<W, (T Value, Func<W, W> Function)> writer)
        where W : IWritable<W>
        => new(w =>
        {
            var ((value, function), output) = writer.Run();

            return (value, w + function(output));
        });

    /// <inheritdoc cref="Writer{W, T}.Listen()"/>
    public static Writer<W, (T Value, W Output)> Listen<W, T>(Writer<W, T> writer)
        where W : IWritable<W>
        => Listens(Identity, writer);

    /// <inheritdoc cref="Writer{W, T}.Listens{TListen}(Func{W, TListen})"/>
    public static Writer<W, (T Value, TListen Output)> Listens<W, T, TListen>(Func<W, TListen> listener, Writer<W, T> writer)
        where W : IWritable<W>
        => writer.Listens(listener);

    /// <inheritdoc cref="Writer{W, T}.Censor(Func{W, W})"/>
    public static Writer<W, T> Censor<W, T>(Func<W, W> censor, Writer<W, T> writer)
        where W : IWritable<W>
        => writer.Censor(censor);
}

public static class Writer<W>
    where W : IWritable<W>
{
    /// <inheritdoc cref="Writer{W, T}.Return(T)"/>
    public static Writer<W, T> Return<T>(T value) => Writer<W, T>.Return(value);

    /// <inheritdoc cref="Writer{W, T}.Write(T, W)"/>
    public static Writer<W, T> Write<T>(T value, W output)
        => new(w => (value, w + output));

    /// <inheritdoc cref="Writer{W, T}.Write(ValueTuple{T, W})"/>
    public static Writer<W, T> Write<T>((T Value, W Output) tuple)
        => Write(tuple.Value, tuple.Output);

    /// <inheritdoc cref="Writer{W, T}.Write(WriteResult{W, T})"/>
    public static Writer<W, T> Write<T>(WriteResult<W, T> result)
        => Write(result.Value, result.Output);

    /// <summary>
    /// Returns a writer which appends the provided <paramref name="item"/>
    /// to its output stream.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static Writer<W, Unit> Tell(W item)
        => new(w => (Unit.Value, w + item));

    /// <summary>
    /// Takes a writer that returns a value and a function over the output
    /// and returns a new writer that applies the function to it's output
    /// before appending it to the stream.
    /// </summary>
    /// <remarks>
    /// This function is in Paul Louth's library and I'm not exactly sure what the use case is.
    /// It appears to basically run a writer that includes a censor delegate in its result
    /// and then combine it, but I'm not sure what would be creating such a writer like the
    /// one in the <paramref name="writer">parameters</paramref>. It also notably doesn't
    /// pass the current output stream into <paramref name="writer"/>'s Run() method.
    /// 
    /// This module is being written as a learning experience, so I'll put it in and see if I
    /// can discover what it's for.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="writer"></param>
    /// <returns></returns>
    public static Writer<W, T> Pass<T>(Writer<W, (T Value, Func<W, W> Function)> writer)
        => new(w =>
        {
            var ((value, function), output) = writer.Run();

            return (value, w + function(output));
        });

    /// <inheritdoc cref="Writer{W, T}.Listen()"/>
    public static Writer<W, (T Value, W Output)> Listen<T>(Writer<W, T> writer)
        => Listens(Identity, writer);

    /// <inheritdoc cref="Writer{W, T}.Listens{TListen}(Func{W, TListen})"/>
    public static Writer<W, (T Value, TListen Output)> Listens<T, TListen>(Func<W, TListen> listener, Writer<W, T> writer)
        => writer.Listens(listener);

    /// <inheritdoc cref="Writer{W, T}.Censor(System.Func{W, W})"/>
    public static Writer<W, T> Censor<T>(Func<W, W> censor, Writer<W, T> writer)
        => writer.Censor(censor);
}
