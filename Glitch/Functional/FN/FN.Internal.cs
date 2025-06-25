namespace Glitch.Functional
{
    public static partial class FN
    {
        internal static TResult DynamicCast<TSource, TResult>(TSource value)
            => (TResult)(dynamic)value!;
    }
}
