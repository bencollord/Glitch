namespace Glitch.Functional;

public static partial class OptionExtensions
{
    extension<T, TResult>(Option<T> self)
    {
        // And
        public static Option<TResult> operator &(Option<T> x, Option<TResult> y) => x.And(y);
        
        public static Option<TResult> operator &(Option<T> x, Okay<TResult> y) => x.And(Option.Some(y.Value));

        public static Option<TResult> operator &(Option<T> x, TResult y) => x.And(Option.Some(y));
    }

    extension<T, E>(Option<T> self)
    {
        // Natural transformation
        public static Result<T, E> operator |(Option<T> x, Fail<E> y) => x.OkayOr(y.Error);
    }
}
