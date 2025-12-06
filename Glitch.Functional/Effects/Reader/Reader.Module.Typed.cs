namespace Glitch.Functional.Effects;

public static class Reader<TEnv>
{
    public static Reader<TEnv, TEnv> Ask() => Asks(Identity);

    public static Reader<TEnv, T> Return<T>(T value) => Reader<TEnv, T>.Return(value);

    public static Reader<TEnv, T> Lift<T>(Func<T> runner) => Reader<TEnv, T>.Lift(runner);

    public static Reader<TEnv, T> Asks<T>(Func<TEnv, T> runner) => Reader<TEnv, T>.Asks(runner);
}
