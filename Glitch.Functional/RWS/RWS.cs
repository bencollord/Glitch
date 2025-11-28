namespace Glitch.Functional;

[Monad]
public partial class RWS<TEnv, S, W, T>
    where W : IWritable<W>
{
    private readonly Func<RWSInput<TEnv, S, W>, RWSResult<S, W, T>> runner;

    internal RWS(Func<RWSInput<TEnv, S, W>, RWSResult<S, W, T>> runner)
    {
        this.runner = runner;
    }

    public static RWS<TEnv, S, W, T> Return(T value) => new(input => (input.State, value, input.Output));

    public static RWS<TEnv, S, W, T> Lift(Func<T> runner) => new(input => (input.State, runner(), input.Output));

    public RWS<TEnv, S, W, TResult> Select<TResult>(Func<T, TResult> map)
        => new(input =>
        {
            var (s, v, w) = Run(input);
            return (s, map(v), input.Output + w);
        });

    public RWS<TEnv, S, W, TOther> Then<TOther>(RWS<TEnv, S, W, TOther> other)
         => Then(other, (_, y) => y);

    public RWS<TEnv, S, W, TResult> Then<TOther, TResult>(RWS<TEnv, S, W, TOther> other, Func<T, TOther, TResult> project)
        => new(input =>
        {
            var (s, v, w) = Run(input);
            var (s2, v2, w2) = other.Run(input.Env, s, w);

            return (s2, project(v, v2), w2);
        });

    public RWS<TEnv, S, W, T> Then(RWS<TEnv, S, W, Unit> other)
         => Then(other, (x, _) => x);

    public RWS<TEnv, S, W, TResult> AndThen<TResult>(Func<T, RWS<TEnv, S, W, TResult>> bind)
         => new(input =>
         {
             var (s, v, w) = Run(input);
             return bind(v).Run((input.Env, s, w));
         });

    public RWS<TEnv, S, W, TResult> Apply<TResult>(RWS<TEnv, S, W, Func<T, TResult>> apply)
        => apply.AndThen(fn => Select(fn));

    public RWS<TEnv, S, W, TResult> AndThen<TElement, TResult>(Func<T, RWS<TEnv, S, W, TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(x => bind(x).Select(project.Curry(x)));

    public RWSResult<S, W, T> Run(TEnv env, S state) => Run(env, state, W.Empty);

    public RWSResult<S, W, T> Run(RWSInput<TEnv, S, W> input) => runner(input);

    public RWSResult<S, W, T> Run(TEnv env, S state, W output) => Run((env, state, output));

    public static implicit operator RWS<TEnv, S, W, T>(T value) => Return(value);
}
