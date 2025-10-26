namespace Glitch.Functional
{
    public static partial class Lazy
    {
        public static Lazy<T> Return<T>(T value) => new(value);

        public static Lazy<T> New<T>(Func<T> function) => new(function);

        public static Lazy<T> New<T>(Func<Unit, T> function) => new(() => function(Unit.Value));
    }
}
