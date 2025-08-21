namespace Glitch.Functional
{
    public static class IO
    {
        public static IO<TEnv, TEnv> Ask<TEnv>() => Lift<TEnv, TEnv>(Identity);
        public static IO<TEnv, T> Return<TEnv, T>(T value) => IO<TEnv, T>.Return(value);
        public static IO<TEnv, T> Fail<TEnv, T>(Error error) => IO<TEnv, T>.Fail(error);
        public static IO<TEnv, T> Lift<TEnv, T>(Func<TEnv, T> func) => IO<TEnv, T>.Lift(func);

        public static IO<Nothing, T> Return<T>(T value) => IO<Nothing, T>.Return(value);
        public static IO<Nothing, T> Fail<T>(Error error) => IO<Nothing, T>.Fail(error);
        public static IO<Nothing, T> Lift<T>(Func<T> func) => IO<Nothing, T>.Lift(_ => func());

        public static IO<Nothing, Nothing> Return() => Return(Nothing.Value);
        public static IO<Nothing, Nothing> Fail(Error error) => Fail<Nothing>(error);
        public static IO<Nothing, Nothing> Lift(Action action) => Lift(() => { action(); return Nothing.Value; });
    }

    public static class IO<TEnv>
    {
        public static IO<TEnv, TEnv> Ask() => Lift(Identity);
        public static IO<TEnv, T> Return<T>(T value) => IO<TEnv, T>.Return(value);
        public static IO<TEnv, T> Fail<T>(Error error) => IO<TEnv, T>.Fail(error);
        public static IO<TEnv, T> Lift<T>(Func<TEnv, T> func) => IO<TEnv, T>.Lift(func);

        public static IO<TEnv, Nothing> Return() => Return(Nothing.Value);
        public static IO<TEnv, Nothing> Fail(Error error) => Fail<Nothing>(error);
        public static IO<TEnv, Nothing> Lift(Action<TEnv> action) => Lift(x => { action(x); return Nothing.Value; });
    }

    public abstract partial class IO<TEnv, T>
    {
        public static IO<TEnv, T> Return(T value) => new ReturnIO<TEnv, T>(value);
        public static IO<TEnv, T> Fail(Error error) => new FailIO<TEnv, T>(error);
        public static IO<TEnv, T> Lift(Func<TEnv, T> func) => new LiftIO<TEnv, T>(func);
    }
}