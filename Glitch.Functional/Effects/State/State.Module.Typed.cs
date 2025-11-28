namespace Glitch.Functional.Effects;

public static class State<S>
{
    public static State<S, T> Return<T>(T value) => State.Return<S, T>(value);

    public static State<S, S> Get() => State.Get<S>();

    public static State<S, T> Gets<T>(Func<S, T> function) => State.Gets(function);

    public static State<S, Unit> Put(S state) => State.Put(state);

    public static State<S, Unit> Modify(Func<S, S> modify) => State.Modify(modify);

    public static State<S, T> Lift<T>(Func<S, (S, T)> runner) => State.Lift(runner);

    public static State<S, T> Lift<T>(Func<S, StateResult<S, T>> runner) => State.Lift(runner);
}
