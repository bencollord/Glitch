namespace Glitch.Functional;

using static FN;

public partial struct Option<T>
{
    public Option<T> IfSome(Action<T> action) =>
        Match(action, Nop).Return(this);

    public Option<T> IfSome(Func<T, Unit> action) =>
        IfSome(action.ReturnVoid());

    public Option<T> IfNone(Action action) =>
        Match(Nop, action).Return(this);

    public Option<T> IfNone(Func<Unit> action) =>
        IfNone(action.ReturnVoid());

    public Option<T> IfNone(Action<Unit> action) =>
        Match(Nop, action).Return(this);

    // Alias for IfSome
    public Option<T> Do(Action<T> action) =>
        Match(action, Nop).Return(this);

    public Option<T> Do(Func<T, Unit> action) =>
        IfSome(action.ReturnVoid());

    public Unit Match(Action<T> okay, Action fail) =>
        Match(okay.Return(), fail.Return());

    public Unit Match(Action<T> okay, Action<Unit> fail) =>
        Match(okay.Return(), fail.Return());
}
