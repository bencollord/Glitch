namespace Glitch.Functional.Inference;

public static class ReturnType<T>
{
    public static Func<T> Infer(Func<T> func) => func;
    public static Func<T1, T> Infer<T1>(Func<T1, T> func) => func;
    public static Func<T1, T2, T> Infer<T1, T2>(Func<T1, T2, T> func) => func;
    public static Func<T1, T2, T3, T> Infer<T1, T2, T3>(Func<T1, T2, T3, T> func) => func;
    public static Func<T1, T2, T3, T4, T> Infer<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T> func) => func;
    public static Func<T1, T2, T3, T4, T5, T> Infer<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T> func) => func;
    public static Func<T1, T2, T3, T4, T5, T6, T> Infer<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T> func) => func;
    public static Func<T1, T2, T3, T4, T5, T6, T7, T> Infer<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T> func) => func;
}
