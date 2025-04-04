namespace Glitch.Functional.Optics
{
    public static class Lens
    {
        public static Lens<TFocus, TValue> New<TFocus, TValue>(Func<TFocus, TValue> get, Func<TFocus, TValue, TFocus> set) => new(get, set);
    }

    public static class Lens<TFocus>
    {
        public static Lens<TFocus, TValue> New<TValue>(Func<TFocus, TValue> get, Func<TFocus, TValue, TFocus> set) => new(get, set);
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

        public static Lens<TFocus, TValue> New(Func<TFocus, TValue> get, Func<TFocus, TValue, TFocus> set) => new(get, set);

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
