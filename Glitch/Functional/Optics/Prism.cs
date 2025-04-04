using Glitch.Functional.Optics;

namespace Glitch.Functional
{
    public static class Prism
    {
        public static Prism<TFocus, TValue> New<TFocus, TValue>(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TFocus> set) => new(get, set);
    }

    public static class Prism<TFocus>
    {
        public static Prism<TFocus, TValue> New<TValue>(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TFocus> set) => new(get, set);
    }

    public record Prism<TFocus, TValue>
    {
        private readonly Func<TFocus, Option<TValue>> getter;
        private readonly Func<TFocus, TValue, TFocus> setter;

        internal Prism(Func<TFocus, Option<TValue>> getter, Func<TFocus, TValue, TFocus> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public static Prism<TFocus, TValue> New(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TFocus> set) => new(get, set);

        public Option<TValue> Get(TFocus focus) => getter(focus);

        public TFocus Set(TFocus focus, TValue value) => setter(focus, value);

        public Func<TValue, TFocus> Set(TFocus focus) => v => Set(focus, v);

        public Func<TFocus, TFocus> Set(TValue value) => f => Set(f, value);

        public Func<TFocus, TFocus> Update(Func<TValue, TValue> update)
            => focus => Update(focus, update);

        public TFocus Update(TFocus focus, Func<TValue, TValue> update)
            => Get(focus).Map(old => Set(focus, update(old))).IfNone(focus);

        public Prism<TFocus, TDeeper> Compose<TDeeper>(Prism<TValue, TDeeper> next)
            => new(focus => Get(focus).AndThen(next.Get),
                  (focus, deeper) => Update(focus, next.Set(deeper)));

        public static implicit operator Prism<TFocus, TValue>(Lens<TFocus, TValue> lens)
            => new(f => Some(lens.Get(f)), lens.Set);

        public static implicit operator Prism<TFocus, TValue>(Lens<TFocus, Option<TValue>> lens)
            => new(lens.Get, (f, v) => lens.Set(f, v));
    }
}
