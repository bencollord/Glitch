using Glitch.Functional;

namespace Glitch.Functional.Optics;

public static partial class Prism
{
    public static Builder<TFocus> For<TFocus>() => new();
    public static Builder<TFocus, TValue> For<TFocus, TValue>(Func<TFocus, TValue> get) => For<TFocus>().Get(get);
    public static Builder<TFocus, TValue> For<TFocus, TValue>(Func<TFocus, Option<TValue>> get) => For<TFocus>().Get(get);

    public record Builder<TFocus>
    {
        public Builder<TFocus, TValue> Get<TValue>(Func<TFocus, TValue> get) => new(x => Option.Some(get(x)));
        public Builder<TFocus, TValue> Get<TValue>(Func<TFocus, Option<TValue>> get) => new(get);
    }

    public record Builder<TFocus, TValue>
    {
        private Func<TFocus, Option<TValue>> get;

        internal Builder(Func<TFocus, Option<TValue>> get)
        {
            this.get = get;
        }

        public Prism<TFocus, TValue> Set(Func<TFocus, TValue, TFocus> set) => New(get, set);
        public Prism<TFocus, TValue> Set(Func<TFocus, TValue, TValue, TFocus> set) => New(get, set);
    }
}