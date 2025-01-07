namespace Glitch
{
    public static class ObjectExtensions
    {
        public static T Convert<T>(this object obj) => (T)obj;

        public static TResult Convert<TSource, TResult>(this TSource obj, Func<TSource, TResult> converter)
            => converter(obj);
    }
}
