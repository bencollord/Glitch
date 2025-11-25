using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Validation;

public static partial class ValidatedExtensions
{
    extension<T, E>(Validated<T, E> self)
    {
        public static Validated<T, E> operator +(Validated<T, E> lhs, Validated<T, E> rhs) =>
            lhs.Coalesce(rhs);

        public static Validated<T, E> operator +(Validated<T, E> lhs, Okay<T> rhs) =>
            lhs.Coalesce(Validated.Okay<T, E>(rhs.Value));

        public static Validated<T, E> operator +(Validated<T, E> lhs, Fail<E> rhs) =>
            lhs.Coalesce(Validated.Fail<T, E>(rhs.Error));

        public static Validated<T, E> operator +(Validated<T, E> lhs, E rhs) =>
            lhs.Coalesce(Validated.Fail<T, E>(rhs));

        public static Validated<T, E> operator |(Validated<T, E> x, Okay<T> y) => x.Or(Validated.Okay<T, E>(y.Value));

        public static Validated<T, E> operator &(Validated<T, E> x, Fail<E> y) => x.And(Validated.Fail<T, E>(y.Error));
    }

    extension<T, E, TResult>(Validated<T, E> self)
    {
        public static Validated<TResult, E> operator &(Validated<T, E> x, Validated<TResult, E> y) => x.And(y);

        public static Validated<TResult, E> operator &(Validated<T, E> x, Okay<TResult> y) => x.And(Validated.Okay<TResult, E>(y.Value));   
    }

    extension<T, E, EResult>(Validated<T, E> self)
    {
        public static Validated<T, EResult> operator |(Validated<T, E> x, Validated<T, EResult> y) => x.Or(y);

        public static Validated<T, EResult> operator |(Validated<T, E> x, Fail<EResult> y) => x.Or(Validated.Fail<T, EResult>(y.Error));
    }
}