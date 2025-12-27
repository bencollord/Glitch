namespace Glitch.Functional;

public static partial class FN
{
    public static Func<T, bool> AsFunc<T>(Predicate<T> predicate) => x => predicate(x);
    public static Func<T, TResult> AsFunc<T, TResult>(Converter<T, TResult> converter) => x => converter(x);

    public static Predicate<T> AsPredicate<T>(Func<T, bool> predicate) => x => predicate(x);
    public static Converter<T, TResult> AsConverter<T, TResult>(Func<T, TResult> converter) => x => converter(x);

    public static Func<Unit> AsUnitFunc(Action action) => action.Return(Unit.Value);
    public static Func<T, Unit> AsUnitFunc<T>(Action<T> action) => action.Return(Unit.Value);
    public static Func<T1, T2, Unit> AsUnitFunc<T1, T2>(Action<T1, T2> action) => action.Return(Unit.Value);
    public static Func<T1, T2, T3, Unit> AsUnitFunc<T1, T2, T3>(Action<T1, T2, T3> action) => action.Return(Unit.Value);
    public static Func<T1, T2, T3, T4, Unit> AsUnitFunc<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) => action.Return(Unit.Value);
    public static Func<T1, T2, T3, T4, T5, Unit> AsUnitFunc<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action) => action.Return(Unit.Value);

    public static Action AsAction<T>(Func<T> action) => action.ReturnVoid();
    public static Action<T> AsAction<T, TResult>(Func<T, TResult> action) => action.ReturnVoid();
    public static Action<T1, T2> AsAction<T1, T2, TResult>(Func<T1, T2, TResult> action) => action.ReturnVoid();
    public static Action<T1, T2, T3> AsAction<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> action) => action.ReturnVoid();
    public static Action<T1, T2, T3, T4> AsAction<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> action) => action.ReturnVoid();
    public static Action<T1, T2, T3, T4, T5> AsAction<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> action) => action.ReturnVoid();
}