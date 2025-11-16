using Glitch.Functional.Core;

namespace Glitch.Functional
{
    public static partial class RWS
    {
        public static RWS<TEnv, S, W, T> Lift<TEnv, S, W, T>(Reader<TEnv, T> reader)
         where W : IWritable<W>
         => RWS<TEnv, S, W, T>.Lift(reader);

        public static RWS<TEnv, S, W, TEnv> Ask<TEnv, S, W>() where W : IWritable<W> => RWS<TEnv, S, W, TEnv>.Asks(Identity);
    }

    public partial class RWS<TEnv, S, W, T>
        where W : IWritable<W>
    {
        public static RWS<TEnv, S, W, T> Asks(Func<TEnv, T> runner) => new(input => (input.State, runner(input.Env), input.Output));

        public static RWS<TEnv, S, W, T> Lift(Reader<TEnv, T> reader) => new(input => (input.State, reader.Run(input.Env), input.Output));

        /// <inheritdoc cref="Reader{TEnv, T}.With{TNewEnv}(Func{TNewEnv, TEnv})"/>
        public RWS<TNewEnv, S, W, T> With<TNewEnv>(Func<TNewEnv, TEnv> map)
            => new(newEnv => Run(new(map(newEnv.Env), newEnv.State, newEnv.Output)));

        /// <inheritdoc cref="Reader{TEnv, T}.Local(Func{TEnv, TEnv})"/>
        public RWS<TEnv, S, W, T> Local(Func<TEnv, TEnv> map)
            => With(map);

        public RWS<TEnv, S, W, TOther> Then<TOther>(Reader<TEnv, TOther> other)
          => Then(RWS<TEnv, S, W, TOther>.Lift(other));

        public RWS<TEnv, S, W, TResult> Then<TOther, TResult>(Reader<TEnv, TOther> other, Func<T, TOther, TResult> project)
            => Then(RWS<TEnv, S, W, TOther>.Lift(other), project);

        public RWS<TEnv, S, W, T> Then(Reader<TEnv, Unit> other)
            => Then(RWS<TEnv, S, W, Unit>.Lift(other));

        public RWS<TEnv, S, W, TResult> AndThen<TResult>(Func<T, Reader<TEnv, TResult>> bind)
            => AndThen(x => RWS<TEnv, S, W, TResult>.Lift(bind(x)));

        public RWS<TEnv, S, W, TResult> AndThen<TNext, TResult>(Func<T, Reader<TEnv, TNext>> bind, Func<T, TNext, TResult> project)
            => AndThen(x => bind(x).Select(project.Curry(x)));
    }
}
