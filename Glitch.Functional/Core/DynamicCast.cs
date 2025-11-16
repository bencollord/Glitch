namespace Glitch.Functional.Core
{
    // TODO Remove duplication with Glitch
    public static class DynamicCast<T>
    {
        public static T From<TFrom>(TFrom obj)
            => obj switch
            {
                T upcast => upcast,
                _ => (T)(dynamic)obj!,
            };

        public static bool Try<TFrom>(TFrom obj, out T result)
        {
            try
            {
                result = From(obj);
                return true;
            }
            catch
            {
                result = default!;
                return false;
            }
        }
    }
}
