namespace Glitch.Functional
{
    public static partial class FN
    {
        public static T Identity<T>(T x) => x;

        public static Func<T, TResult> Constant<T, TResult>(TResult x) => _ => x;
    }

    public static partial class FN<T>
    {
        public static T Identity(T x) => x;

        public static Func<T, TResult> Constant<TResult>(TResult value) => _ => value;
    }
}
