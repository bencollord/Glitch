namespace Glitch.Functional;

public static partial class FN
{
    public static void Nop<T>(T _) { /* Do nothing */ }

    public static Func<T> Func<T>(Func<T> func) => func;

    public static Func<T, TResult> Func<T, TResult>(Func<T, TResult> func) => func;

    public static Func<T1, T2, TResult> Func<T1, T2, TResult>(Func<T1, T2, TResult> func) => func;

    public static Func<T1, T2, T3, TResult> Func<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func) => func;

    public static Func<T1, T2, T3, T4, TResult> Func<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func) => func;

    public static Action Action(Action action) => action;

    public static Action<T> Action<T>(Action<T> action) => action;

    public static Action<T1, T2> Action<T1, T2>(Action<T1, T2> action) => action;

    public static Action<T1, T2, T3> Action<T1, T2, T3>(Action<T1, T2, T3> action) => action;

    public static Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) => action;

    public static Action Action(Func<Unit> action) => action.ReturnVoid();

    public static Action<T> Action<T>(Func<T, Unit> action) => action.ReturnVoid();

    public static Action<T1, T2> Action<T1, T2>(Func<T1, T2, Unit> action) => action.ReturnVoid();

    public static Action<T1, T2, T3> Action<T1, T2, T3>(Func<T1, T2, T3, Unit> action) => action.ReturnVoid();

    public static Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>(Func<T1, T2, T3, T4, Unit> action) => action.ReturnVoid();
}

public static partial class FN<T>
{
    public static void Nop(T _) { /* Do nothing */ }

    public static Func<T> Func(Func<T> func) => func;

    public static Func<T, TResult> Func<TResult>(Func<T, TResult> func) => func;

    public static Func<T, T2, TResult> Func<T2, TResult>(Func<T, T2, TResult> func) => func;

    public static Func<T, T2, T3, TResult> Func<T2, T3, TResult>(Func<T, T2, T3, TResult> func) => func;

    public static Func<T, T2, T3, T4, TResult> Func<T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> func) => func;

    public static Action<T> Action(Action<T> action) => action;

    public static Action<T, T2> Action<T2>(Action<T, T2> action) => action;

    public static Action<T, T2, T3> Action<T2, T3>(Action<T, T2, T3> action) => action;

    public static Action<T, T2, T3, T4> Action<T2, T3, T4>(Action<T, T2, T3, T4> action) => action;
}
