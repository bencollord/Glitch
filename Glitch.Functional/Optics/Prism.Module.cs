using Glitch.Functional;

namespace Glitch.Functional.Optics;

public static partial class Prism
{
    /// <summary>
    /// <inheritdoc cref="Prism{TFocus, TValue}.New(System.Func{TFocus, Option{TValue}}, System.Func{TFocus, TValue, TFocus})"/>
    /// </summary>
    /// <typeparam name="TFocus"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static Prism<TFocus, TValue> New<TFocus, TValue>(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TFocus> set) 
        => Prism<TFocus, TValue>.New(get, set);

    /// <summary>
    /// <inheritdoc cref="Prism{TFocus, TValue}.New(System.Func{TFocus, Option{TValue}}, System.Func{TFocus, TValue, TValue, TFocus})"/>
    /// </summary>
    /// <typeparam name="TFocus"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static Prism<TFocus, TValue> New<TFocus, TValue>(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TValue, TFocus> set)
        => Prism<TFocus, TValue>.New(get, set);
}

public static class Prism<TFocus>
{
    /// <summary>
    /// <inheritdoc cref="Prism{TFocus, TValue}.New(System.Func{TFocus, Option{TValue}}, System.Func{TFocus, TValue, TFocus})"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static Prism<TFocus, TValue> New<TValue>(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TFocus> set) 
        => Prism<TFocus, TValue>.New(get, set);

    /// <summary>
    /// <inheritdoc cref="Prism{TFocus, TValue}.New(System.Func{TFocus, Option{TValue}}, System.Func{TFocus, TValue, TValue, TFocus})"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static Prism<TFocus, TValue> New<TValue>(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TValue, TFocus> set)
        => Prism<TFocus, TValue>.New(get, set);
}