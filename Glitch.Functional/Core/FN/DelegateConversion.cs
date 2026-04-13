using System.Diagnostics.CodeAnalysis;

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

    public static Func<string, Option<T>> AsOptional<T>(DotNetTryParse<T> tryParse) =>
        str => tryParse(str, out var val)
             ? Option.Some(val)
             : Option.None;

    public static Func<string, TArg, Option<T>> AsOptional<TArg, T>(DotNetTryParse<TArg, T> tryParse) =>
        (str, arg) => tryParse(str, arg, out var val)
             ? Option.Some(val)
             : Option.None;

    public static DotNetTryParse<T> AsTryParse<T>(Func<string, Option<T>> tryParse) =>
        new DotNetTryParse<T>((string? str, [NotNullWhen(true)] out T? res) =>
        {
            var opt = tryParse(str ?? string.Empty);

            res = opt.UnwrapOrDefault();
            return opt.IsSome;
        });

    public static DotNetTryParse<TArg, T> AsTryParse<TArg, T>(Func<string, TArg, Option<T>> tryParse) =>
        new DotNetTryParse<TArg, T>((string? str, TArg arg, [NotNullWhen(true)] out T? res) =>
        {
            var opt = tryParse(str ?? string.Empty, arg);

            res = opt.UnwrapOrDefault();
            return opt.IsSome;
        });

    public delegate bool DotNetTryParse<T>(string? input, [NotNullWhen(true)] out T? item);
    public delegate bool DotNetTryParse<in TArg, T>(string? input, TArg arg, [NotNullWhen(true)] out T? item);
}