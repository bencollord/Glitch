namespace Glitch.Functional;

public readonly partial struct Option<T>
{
    public static Option<T> operator ^(Option<T> x, Option<T> y) => x.Xor(y);

    // Short-circuiting
    public static Option<T> operator |(Option<T> x, Option<T> y) => x.Or(y);

    public static Option<T> operator &(Option<T> x, Option<T> y) => x.And(y);

    public static bool operator true(Option<T> option) => option.IsSome;

    public static bool operator false(Option<T> option) => option.IsNone;
}

public static partial class OptionExtensions
{
    extension<T>(Option<T> _)
    {
        // Coalescing operators
        public static T operator |(Option<T> x, T y) => x.IfNone(y);

        public static T operator |(Option<T> x, Okay<T> y) => x.IfNone(y.Value);
    }

    extension<T, TResult>(Option<T> self)
    {
        // And
        public static Option<TResult> operator &(Option<T> x, Option<TResult> y) => x.And(y);
        
        public static Option<TResult> operator &(Option<T> x, Okay<TResult> y) => x.And(Option.Some(y.Value));

        public static Option<TResult> operator &(Option<T> x, TResult y) => x.And(Option.Some(y));
    }
}
