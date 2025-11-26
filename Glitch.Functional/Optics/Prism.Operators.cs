namespace Glitch.Functional.Optics;

public static class PrismExtensions
{
    extension<TFocus, TValue, TDeeper>(Prism<TFocus, TValue> _)
    {
        public static Prism<TFocus, TDeeper> operator *(Prism<TFocus, TValue> lhs, Prism<TValue, TDeeper> rhs) =>
            lhs.Compose(rhs);

        public static Prism<TFocus, TDeeper> operator *(Prism<TFocus, TValue> lhs, Lens<TValue, TDeeper> rhs) =>
            lhs.Compose(rhs);
    }
}
