namespace Glitch.Functional.Optics;

public static partial class Lens
{
    /// <summary>
    /// <inheritdoc cref="Lens{TFocus, TValue}.New(System.Func{TFocus, TValue}, System.Func{TFocus, TValue, TFocus})"/>
    /// </summary>
    /// <typeparam name="TFocus"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static Lens<TFocus, TValue> New<TFocus, TValue>(Func<TFocus, TValue> get, Func<TFocus, TValue, TFocus> set) => new(get, set);

    /// <summary>
    /// <inheritdoc cref="Lens{TFocus, TValue}.New(System.Func{TFocus, TValue}, System.Func{TFocus, TValue, TValue, TFocus})"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static Lens<TFocus, TValue> New<TFocus, TValue>(Func<TFocus, TValue> get, Func<TFocus, TValue, TValue, TFocus> set)
        => new(get, (focus, newValue) => set(focus, get(focus), newValue));
}

public static class Lens<TFocus>
{
    /// <summary>
    /// <inheritdoc cref="Lens{TFocus, TValue}.New(System.Func{TFocus, TValue}, System.Func{TFocus, TValue, TFocus})"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static Lens<TFocus, TValue> New<TValue>(Func<TFocus, TValue> get, Func<TFocus, TValue, TFocus> set) => new(get, set);

    /// <summary>
    /// <inheritdoc cref="Lens{TFocus, TValue}.New(System.Func{TFocus, TValue}, System.Func{TFocus, TValue, TValue, TFocus})"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static Lens<TFocus, TValue> New<TValue>(Func<TFocus, TValue> get, Func<TFocus, TValue, TValue, TFocus> set)
        => new(get, (focus, newValue) => set(focus, get(focus), newValue));
}