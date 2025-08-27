using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static class IO
    {
        public static IO<T> Return<T>(T value) => IO<T>.Return(value);
        public static IO<T> Fail<T>(Error error) => IO<T>.Fail(error);
        public static IO<T> Lift<T>(Func<T> func) => IO<T>.Lift(func);
        public static IO<T> Lift<T>(Func<IOEnv, T> func) => IO<T>.Lift(func);

        public static IO<Unit> Return() => Return(Unit.Value);
        public static IO<Unit> Fail(Error error) => Fail<Unit>(error);
        public static IO<Unit> Lift(Action action) => Lift(() => { action(); return Unit.Value; });
        public static IO<Unit> Lift(Action<IOEnv> action) => Lift(env => { action(env); return Unit.Value; });
    }

    public abstract partial class IO<T>
    {
        public static IO<T> Return(T value) => new ReturnIO<T>(value);
        public static IO<T> Fail(Error error) => new FailIO<T>(error);
        public static IO<T> Lift(Func<T> func) => new LiftIO<T>(_ => func());
        public static IO<T> Lift(Func<IOEnv, T> func) => new LiftIO<T>(func);
    }
}