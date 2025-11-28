namespace Glitch.Functional.Effects;

public static partial class RWS
{
    public static RWS<TEnv, S, W, T> Lift<TEnv, S, W, T>(State<S, T> stateful)
        where W : IWritable<W>
        => RWS<TEnv, S, W, T>.Lift(stateful);

    public static RWS<TEnv, S, W, S> Get<TEnv, S, W>() where W : IWritable<W> => RWS<TEnv, S, W, S>.Gets(Identity);

    public static RWS<TEnv, S, W, Unit> Put<TEnv, S, W>(S state) where W : IWritable<W> => Modify<TEnv, S, W>(_ => state);

    public static RWS<TEnv, S, W, Unit> Modify<TEnv, S, W>(Func<S, S> modify) where W : IWritable<W> => new(input => (modify(input.State), Unit.Value, input.Output));
}

public partial class RWS<TEnv, S, W, T>
{
    public static RWS<TEnv, S, W, T> Gets(Func<S, T> function) => new(input => (input.State, function(input.State), input.Output));

    public static RWS<TEnv, S, W, T> Lift(State<S, T> stateful) => new(input =>
    {
        var (state, value) = stateful(input.State);

        return (state, value, input.Output);
    });

    public RWS<TEnv, S, W, TOther> Then<TOther>(State<S, TOther> other)
      => Then(RWS<TEnv, S, W, TOther>.Lift(other));

    public RWS<TEnv, S, W, TResult> Then<TOther, TResult>(State<S, TOther> other, Func<T, TOther, TResult> project)
        => Then(RWS<TEnv, S, W, TOther>.Lift(other), project);

    public RWS<TEnv, S, W, T> Then(State<S, Unit> other)
        => Then(RWS<TEnv, S, W, Unit>.Lift(other));

    public RWS<TEnv, S, W, TResult> AndThen<TResult>(Func<T, State<S, TResult>> bind)
         => new(input =>
         {
             var (s, v, w) = Run(input);
             var (s2, v2) = bind(v)(s);

             return (s2, v2, w);
         });

    public RWS<TEnv, S, W, TResult> AndThen<TElement, TResult>(Func<T, State<S, TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(x => bind(x).Select(project.Curry(x)));
}
