using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static class ObjectExtensions
    {
        public static TResult PipeInto<T, TResult>(this T obj, Func<T, TResult> func) => func(obj);

        public static Option<T> AsOption<T>(this T? obj) => Option.Maybe(obj);
        public static Option<T> AsOption<T>(this T? obj) where T : struct => Option.Maybe(obj);
    }
}
