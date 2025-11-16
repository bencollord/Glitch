namespace Glitch.Functional.Optics
{
    public static partial class Lens
    {
        public static Builder<TFocus> For<TFocus>() => new();
        public static Builder<TFocus, TValue> For<TFocus, TValue>(Func<TFocus, TValue> get) => For<TFocus>().Get(get);

        public record Builder<TFocus>
        {
            public Builder<TFocus, TValue> Get<TValue>(Func<TFocus, TValue> get) => new(get);
        }

        public record Builder<TFocus, TValue>
        {
            private Func<TFocus, TValue> get;

            internal Builder(Func<TFocus, TValue> get)
            {
                this.get = get;
            }

            public Lens<TFocus, TValue> Set(Func<TFocus, TValue, TFocus> set) => New(get, set);
            public Lens<TFocus, TValue> Set(Func<TFocus, TValue, TValue, TFocus> set) => New(get, set);
        }
    }
}