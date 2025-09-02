namespace Glitch.Functional
{
    public record RWSInput<TEnv, S, W>(TEnv Env, S State, W Output)
        where W : IWritable<W>
    {
        public static implicit operator RWSInput<TEnv, S, W>((TEnv Env, S State, W Output) tuple) => new(tuple.Env, tuple.State, tuple.Output);
    }

    public record RWSResult<S, W, T>(S State, T Value, W Output)
        where W : IWritable<W>
    {
        public static implicit operator RWSResult<S, W, T>((S State, T Value, W Output) tuple) => new(tuple.State, tuple.Value, tuple.Output);
    }
}
