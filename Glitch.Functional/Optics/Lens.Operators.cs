namespace Glitch.Functional.Optics;

public static class LensExtensions
{
    extension<TFocus, TValue, TDeeper>(Lens<TFocus, TValue> _)
    {
        public static Lens<TFocus, TDeeper> operator *(Lens<TFocus, TValue> lhs, Lens<TValue, TDeeper> rhs) =>
            lhs.Compose(rhs);

        public static Prism<TFocus, TDeeper> operator *(Lens<TFocus, TValue> lhs, Prism<TValue, TDeeper> rhs) =>
            lhs.Compose(rhs);
    }
}
