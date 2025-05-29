namespace Glitch.Functional
{
    public static class ObjectExtensions
    {
        public static Terminal Ignore<T>(this T _) => default;

        public static TResult PipeInto<T, TResult>(this T obj, Func<T, TResult> func) => func(obj);

        public static Option<T> AsOption<T>(this T? obj) => Maybe(obj);

        public static Option<T> CastOrNone<T>(this object? obj) 
            where T : class 
            => Maybe(obj as T);
    }
}
