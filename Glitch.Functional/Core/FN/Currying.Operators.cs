namespace Glitch.Functional;

/// <summary>
/// Operators for currying and uncurrying functions.
/// </summary>
/// <remarks>
/// The convention I'm using is that unary plus curries functions
/// and unary minus uncurries them. I've also approprated the bitwise
/// compliment operator to flip the arguments of a function.
/// 
/// Needless to say, this is some wild abuse of operator overloading, even for me.
/// As such, everything in this file is SUPER experimental and may or may not be
/// around for long. I'm going to use it in my scripts for a month or two and see
/// if I find the conventions intuitive or just confusing.
/// </remarks>
public static partial class FuncExtensions
{
    // HACK Putting this in show it shows up in my task list to make a decision on this file.
    extension<T1, T2, TResult>(Func<T1, T2, TResult> func)
    {
        // Inversion operator
        // HACK This idea is SUPER experimental. I'm going to use this in my scripts for a
        // month and see whether I find it intuitive or confusing.
        public static Func<T2, T1, TResult> operator ~(Func<T1, T2, TResult> fn) => (a2, a1) => fn(a1, a2);
    }

    extension<T1, T2, R>(Func<T1, T2, R> _)
    {
        public static Func<T1, Func<T2, R>> operator +(Func<T1, T2, R> func) => FN.Curry(func);
    }

    extension<T1, T2, T3, R>(Func<T1, T2, T3, R> _)
    {
        public static Func<T1, Func<T2, Func<T3, R>>> operator +(Func<T1, T2, T3, R> func) => FN.Curry(func);
    }

    extension<T1, T2, T3, T4, R>(Func<T1, T2, T3, T4, R> _)
    {
        public static Func<T1, Func<T2, Func<T3, Func<T4, R>>>> operator +(Func<T1, T2, T3, T4, R> func) => FN.Curry(func);
    }

    extension<T1, T2, T3, T4, T5, R>(Func<T1, T2, T3, T4, T5, R> _)
    {
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, R>>>>> operator +(Func<T1, T2, T3, T4, T5, R> func) => FN.Curry(func);
    }

    extension<T1, T2, T3, T4, T5, T6, R>(Func<T1, T2, T3, T4, T5, T6, R> _)
    {
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, R>>>>>> operator +(Func<T1, T2, T3, T4, T5, T6, R> func) => FN.Curry(func);
    }

    extension<T1, T2, T3, T4, T5, T6, T7, R>(Func<T1, T2, T3, T4, T5, T6, T7, R> _)
    {
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, R>>>>>>> operator +(Func<T1, T2, T3, T4, T5, T6, T7, R> func) => FN.Curry(func);
    }

    extension<T1, T2, T3, T4, T5, T6, T7, T8, R>(Func<T1, T2, T3, T4, T5, T6, T7, T8, R> _)
    {
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, R>>>>>>>> operator +(Func<T1, T2, T3, T4, T5, T6, T7, T8, R> func) => FN.Curry(func);
    }

    extension<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> _)
    {
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, R>>>>>>>>> operator +(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> func) => FN.Curry(func);
    }

    extension<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> _)
    {
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, R>>>>>>>>>> operator +(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> func) => FN.Curry(func);
    }

    extension<T1, T2, R>(Func<T1, Func<T2, R>> _)
    {
        public static Func<T1, T2, R> operator -(Func<T1, Func<T2, R>> func) => FN.Uncurry(func);
    }

    extension<T1, T2, T3, R>(Func<T1, Func<T2, Func<T3, R>>> _)
    {
        public static Func<T1, T2, T3, R> operator -(Func<T1, Func<T2, Func<T3, R>>> func) => FN.Uncurry(func);
    }

    extension<T1, T2, T3, T4, R>(Func<T1, Func<T2, Func<T3, Func<T4, R>>>> _)
    {
        public static Func<T1, T2, T3, T4, R> operator -(Func<T1, Func<T2, Func<T3, Func<T4, R>>>> func) => FN.Uncurry(func);
    }

    extension<T1, T2, T3, T4, T5, R>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, R>>>>> _)
    {
        public static Func<T1, T2, T3, T4, T5, R> operator -(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, R>>>>> func) => FN.Uncurry(func);
    }

    extension<T1, T2, T3, T4, T5, T6, R>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, R>>>>>> _)
    {
        public static Func<T1, T2, T3, T4, T5, T6, R> operator -(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, R>>>>>> func) => FN.Uncurry(func);
    }

    extension<T1, T2, T3, T4, T5, T6, T7, R>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, R>>>>>>> _)
    {
        public static Func<T1, T2, T3, T4, T5, T6, T7, R> operator -(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, R>>>>>>> func) => FN.Uncurry(func);
    }

    extension<T1, T2, T3, T4, T5, T6, T7, T8, R>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, R>>>>>>>> _)
    {
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, R> operator -(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, R>>>>>>>> func) => FN.Uncurry(func);
    }

    extension<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, R>>>>>>>>> _)
    {
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> operator -(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, R>>>>>>>>> func) => FN.Uncurry(func);
    }

    extension<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, R>>>>>>>>>> _)
    {
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> operator -(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, R>>>>>>>>>> func) => FN.Uncurry(func);
    }
}
