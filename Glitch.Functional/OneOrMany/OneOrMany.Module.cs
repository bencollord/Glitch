namespace Glitch.Functional
{
    public static partial class OneOrMany
    {
        public static OneOrMany<T> One<T>(T value) => OneOrMany<T>.One(value);

        public static OneOrMany<T> Many<T>(IEnumerable<T> items) => OneOrMany<T>.Many(items);

        public static OneOrMany<T> Of<T>(params T[] items) => OneOrMany<T>.Of(items);
    }
}