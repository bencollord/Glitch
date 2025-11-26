using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Validation;

public static partial class ValidatedExtensions
{
    extension<T, E>(Validated<T, E> self)
    {
        public static Validated<T, E> operator |(Validated<T, E> lhs, Validated<T, E> rhs) =>
            lhs.Or(rhs);

        public static Validated<T, E> operator |(Validated<T, E> lhs, Okay<T> rhs) =>
            lhs.Or(Validated.Okay<T, E>(rhs.Value));

        public static Validated<T, E> operator |(Validated<T, E> lhs, Fail<E> rhs) =>
            lhs.Or(Validated.Fail<T, E>(rhs.Error));

        public static Validated<T, E> operator |(Validated<T, E> lhs, E rhs) =>
            lhs.Or(Validated.Fail<T, E>(rhs));

        public static Validated<T, E> operator &(Validated<T, E> x, Fail<E> y) => x.And(Validated.Fail<T, E>(y.Error));
    }

    extension<T, E, TResult>(Validated<T, E> self)
    {
        public static Validated<TResult, E> operator &(Validated<T, E> x, Validated<TResult, E> y) => x.And(y);

        public static Validated<TResult, E> operator &(Validated<T, E> x, Okay<TResult> y) => x.And(Validated.Okay<TResult, E>(y.Value));   
    }
}