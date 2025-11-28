namespace Glitch.Functional.Effects;

public static partial class State
{
    public static State<S, T> Return<S, T>(T value) => Lift((S state) => (state, value));

    public static State<S, S> Get<S>() => Lift((S state) => (state, state));

    public static State<S, T> Gets<S, T>(Func<S, T> function) => Lift((S state) => (state, function(state)));

    public static State<S, Unit> Put<S>(S state) => Modify<S>(_ => state);

    public static State<S, Unit> Modify<S>(Func<S, S> modify) => Lift((S state) => (modify(state), Unit.Value));

    public static State<S, T> Lift<S, T>(Func<S, (S, T)> runner) => Lift<S, T>(s => runner(s));

    public static State<S, T> Lift<S, T>(Func<S, StateResult<S, T>> runner) => s => runner(s);
}
