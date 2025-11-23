using Glitch.Functional;

namespace Glitch.Functional
{
    public static class State<S>
    {
        public static IStateful<S, T> Return<T>(T value) => State.Return<S, T>(value);

        public static IStateful<S, S> Get() => State.Get<S>();

        public static IStateful<S, T> Gets<T>(Func<S, T> function) => State.Gets(function);

        public static IStateful<S, Unit> Put(S state) => State.Put(state);

        public static IStateful<S, Unit> Modify(Func<S, S> modify) => State.Modify(modify);

        public static IStateful<S, T> Lift<T>(Func<S, (S, T)> runner) => State.Lift(runner);

        public static IStateful<S, T> Lift<T>(Func<S, StateResult<S, T>> runner) => State.Lift(runner);
    }
}
