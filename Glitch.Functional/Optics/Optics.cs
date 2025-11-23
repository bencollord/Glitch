using Glitch.Functional.Core;

namespace Glitch.Functional.Optics;

// UNDONE
public class Optics<TFocus, TValue>
{
    public static PrismBuilder PrismFor(Func<TFocus, Option<TValue>> getter)
        => new(getter);

    public class PrismBuilder
    {
        private Func<TFocus, Option<TValue>> getter;

        internal PrismBuilder(Func<TFocus, Option<TValue>> getter)
        {
            this.getter = getter;
        }
    }
}
