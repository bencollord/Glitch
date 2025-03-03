namespace Glitch.Functional
{
    public static class ObjectExtensions
    {
        public static Unit Ignore<T>(this T _) => default;

        public static TResult PipeInto<T, TResult>(this T obj, Func<T, TResult> func) => func(obj);
    }
}
