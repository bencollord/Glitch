namespace Glitch.Functional
{
    public static partial class FN
    {
        internal static TResult DynamicCast<TSource, TResult>(TSource value)
            => (TResult)(dynamic)value!;

        internal static TBase UpCast<TDerived, TBase>(TDerived value)
            where TDerived : TBase
            => (TBase)value!;
    }
}
