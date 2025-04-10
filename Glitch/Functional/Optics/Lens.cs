namespace Glitch.Functional.Optics
{
    public static class Lens
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

    public record Lens<TFocus, TValue>
    {
        private readonly Func<TFocus, TValue> getter;
        private readonly Func<TFocus, TValue, TFocus> setter;

        internal Lens(Func<TFocus, TValue> getter, Func<TFocus, TValue, TFocus> setter)
        {
            this.getter = getter;
            this.setter = setter;
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
        public static Lens<TFocus, TValue> New(Func<TFocus, TValue> get, Func<TFocus, TValue, TValue, TFocus> set)
            => new(get, (focus, newValue) => set(focus, get(focus), newValue));

        public TValue Get(TFocus focus) => getter(focus);

        public TFocus Set(TFocus focus, TValue value) => setter(focus, value);

        public Func<TFocus, TFocus> Set(TValue value) => focus => Set(focus, value);

        public Func<TValue, TFocus> Set(TFocus focus) => value => Set(focus, value);

        public Func<TFocus, TFocus> Update(Func<TValue, TValue> update) => focus => Set(focus, update(Get(focus)));

        public TFocus Update(TFocus focus, Func<TValue, TValue> update) => Set(focus, update(Get(focus)));

        public Lens<TFocus, TDeeper> Compose<TDeeper>(Lens<TValue, TDeeper> other)
            => new(f => other.Get(Get(f)),
                   (f, v) => Set(f, other.Set(Get(f), v)));
    }
}
