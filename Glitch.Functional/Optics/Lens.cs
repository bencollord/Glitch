namespace Glitch.Functional.Optics;

public record Lens<TFocus, TValue>
{
    private readonly Func<TFocus, TValue> getter;
    private readonly Func<TFocus, TValue, TFocus> setter;

    public Lens(Func<TFocus, TValue> getter, Func<TFocus, TValue, TFocus> setter)
    {
        this.getter = getter;
        this.setter = setter;
    }

    public Lens(Func<TFocus, TValue> getter, Func<TFocus, TValue, TValue, TFocus> setter)
        : this(getter, (focus, newValue) => setter(focus, getter(focus), newValue))
    {
    }

    /// <summary>
    /// Creates a new <see cref="Lens{TFocus, TValue}"/> from the provided get and set functions.
    /// </summary>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static Lens<TFocus, TValue> New(Func<TFocus, TValue> get, Func<TFocus, TValue, TFocus> set) => new(get, set);

    /// <summary>
    /// Creates a new <see cref="Lens{TFocus, TValue}"/> from the provided get and set functions.
    /// The set function takes both the old value and the new value as arguments,
    /// thus allowing things such as replacing an item in an immutable collection.
    /// </summary>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public static Lens<TFocus, TValue> New(Func<TFocus, TValue> get, Func<TFocus, TValue, TValue, TFocus> set) => new(get, set);

    public TValue Get(TFocus focus) => getter(focus);

    public TFocus Set(TFocus focus, TValue value) => setter(focus, value);

    public Func<TFocus, TFocus> Set(TValue value) => focus => Set(focus, value);

    public Func<TValue, TFocus> Set(TFocus focus) => value => Set(focus, value);

    public Func<TFocus, TFocus> Update(Func<TValue, TValue> update) => focus => Set(focus, update(Get(focus)));

    public TFocus Update(TFocus focus, Func<TValue, TValue> update) => Set(focus, update(Get(focus)));

    public Lens<TFocus, TDeeper> Compose<TDeeper>(Func<TValue, TDeeper> get, Func<TValue, TDeeper, TValue> set)
        => Compose(new Lens<TValue, TDeeper>(get, set));

    public Lens<TFocus, TDeeper> Compose<TDeeper>(Lens<TValue, TDeeper> other)
        => new(f => other.Get(Get(f)),
               (f, v) => Set(f, other.Set(Get(f), v)));

    public Prism<TFocus, TDeeper> Compose<TDeeper>(Prism<TValue, TDeeper> prism)
        => Prism.New<TFocus, TValue>(f => Option.Some(Get(f)), Set).Compose(prism);
}
