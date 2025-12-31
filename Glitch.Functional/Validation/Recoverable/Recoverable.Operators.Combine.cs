using Glitch.Functional.Validation.Recoverable;

namespace Glitch.Functional;

public static partial class RecoverableExtensions
{
    extension<T, E>(Recoverable<T, E> self)
    {
        public static Recoverable<T, E> operator |(Recoverable<T, E> lhs, Recoverable<T, E> rhs) =>
            lhs.Or(rhs);

        public static Recoverable<T, E> operator |(Recoverable<T, E> lhs, Okay<T> rhs) =>
            lhs.Or(Recoverable.Okay<T, E>(rhs.Value));

        public static Recoverable<T, E> operator |(Recoverable<T, E> lhs, Fail<E> rhs) =>
            lhs.Or(Recoverable.Fatal<T, E>(rhs.Error));

        public static Recoverable<T, E> operator |(Recoverable<T, E> lhs, E rhs) =>
            lhs.Or(Recoverable.Fatal<T, E>(rhs));

        public static Recoverable<T, E> operator &(Recoverable<T, E> x, Fail<E> y) => x.And(Recoverable.Fatal<T, E>(y.Error));
    }

    extension<T, E, TResult>(Recoverable<T, E> self)
    {
        public static Recoverable<TResult, E> operator &(Recoverable<T, E> x, Recoverable<TResult, E> y) => x.And(y);

        public static Recoverable<TResult, E> operator &(Recoverable<T, E> x, Okay<TResult> y) => x.And(Recoverable.Okay<TResult, E>(y.Value));   
    }
}