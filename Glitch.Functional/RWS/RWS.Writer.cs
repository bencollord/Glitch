using Glitch.Functional;

namespace Glitch.Functional;

public static partial class RWS
{
    public static RWS<TEnv, S, W, T> Lift<TEnv, S, W, T>(Writer<W, T> writer)
        where W : IWritable<W>
        => RWS<TEnv, S, W, T>.Lift(writer);

    /// <inheritdoc cref="Writer{W, T}.Write(T, W)"/>
    public static RWS<TEnv, S, W, T> Write<TEnv, S, W, T>(T value, W output)
        where W : IWritable<W>
        => new(input => (input.State, value, input.Output + output));

    /// <inheritdoc cref="Writer{W, T}.Write(ValueTuple{T, W})"/>
    public static RWS<TEnv, S, W, T> Write<TEnv, S, W, T>((T Value, W Output) tuple)
        where W : IWritable<W>
        => Write<TEnv, S, W, T>(tuple.Value, tuple.Output);

    /// <inheritdoc cref="Writer{W, T}.Write(WriteResult{W, T})"/>
    public static RWS<TEnv, S, W, T> Write<TEnv, S, W, T>(WriteResult<W, T> result)
        where W : IWritable<W>
        => Write<TEnv, S, W, T>(result.Value, result.Output);

    /// <inheritdoc cref="Writer{W}.Tell(W)"/>
    public static RWS<TEnv, S, W, Unit> Tell<TEnv, S, W>(W item)
        where W : IWritable<W>
        => new(input => (input.State, Unit.Value, input.Output + item));


    /// <inheritdoc cref="Writer{W}.Pass{T}(Writer{W, ValueTuple{T, Func{W, W}}})"/>
    public static RWS<TEnv, S, W, T> Pass<TEnv, S, W, T>(RWS<TEnv, S, W, (T Value, Func<W, W> Function)> writer)
        where W : IWritable<W>
        => new(input =>
        {
            var (state, (value, function), output) = writer.Run(input with { Output = W.Empty }); // Imitating Paul Louth here, but I'll revisit this after I've actually run this thing in LinqPad

            return (state, value, input.Output + function(output));
        });

    /// <inheritdoc cref="Writer{W, T}.Listen()"/>
    public static RWS<TEnv, S, W, (T Value, W Output)> Listen<TEnv, S, W, T>(RWS<TEnv, S, W, T> writer)
        where W : IWritable<W>
        => Listens(Identity, writer);

    /// <inheritdoc cref="Writer{W, T}.Listens{TListen}(Func{W, TListen})"/>
    public static RWS<TEnv, S, W, (T Value, TListen Output)> Listens<TEnv, S, W, T, TListen>(Func<W, TListen> listener, RWS<TEnv, S, W, T> writer)
        where W : IWritable<W>
        => writer.Listens(listener);

    /// <inheritdoc cref="Writer{W, T}.Censor(Func{W, W})"/>
    public static RWS<TEnv, S, W, T> Censor<TEnv, S, W, T>(Func<W, W> censor, RWS<TEnv, S, W, T> writer)
        where W : IWritable<W>
        => writer.Censor(censor);
}

public partial class RWS<TEnv, S, W, T>
    where W : IWritable<W>
{
    public static RWS<TEnv, S, W, T> Lift(Writer<W, T> writer) => new(input =>
    {
        var (value, output) = writer.Run(input.Output);

        return (input.State, value, output);
    });


    /// <inheritdoc cref="Writer{W, T}.Listen()"/>
    public RWS<TEnv, S, W, (T Value, W Output)> Listen()
        => Listens(Identity);

    /// <inheritdoc cref="Writer{W, T}.Listens{TListen}(Func{W, TListen})"/>
    public RWS<TEnv, S, W, (T Value, TListen Output)> Listens<TListen>(Func<W, TListen> listener)
        => new(input =>
        {
            var (state, value, output) = Run(input.Env, input.State);

            return (state, (value, listener(output)), input.Output + output);
        });

    /// <inheritdoc cref="Writer{W, T}.Censor(Func{W, W})"/>
    public RWS<TEnv, S, W, T> Censor(Func<W, W> censor)
        => new(input =>
        {
            var (state, value, output) = Run(input.Env, input.State);

            return (state, value, input.Output + censor(output));
        });

    public RWS<TEnv, S, W, TOther> Then<TOther>(Writer<W, TOther> other)
         => Then(RWS<TEnv, S, W, TOther>.Lift(other));

    public RWS<TEnv, S, W, TResult> Then<TOther, TResult>(Writer<W, TOther> other, Func<T, TOther, TResult> project)
        => Then(RWS<TEnv, S, W, TOther>.Lift(other), project);

    public RWS<TEnv, S, W, T> Then(Writer<W, Unit> other)
        => Then(RWS<TEnv, S, W, Unit>.Lift(other));

    public RWS<TEnv, S, W, TResult> AndThen<TResult>(Func<T, Writer<W, TResult>> bind)
        => AndThen(x => RWS<TEnv, S, W, TResult>.Lift(bind(x)));

    public RWS<TEnv, S, W, TResult> AndThen<TNext, TResult>(Func<T, Writer<W, TNext>> bind, Func<T, TNext, TResult> project)
        => AndThen(x => bind(x).Select(project.Curry(x)));
}
