using Glitch.Functional.Errors;

namespace Glitch.Functional;

public static partial class ResultExtensions
{
    extension<T>(Result<T> self)
    {
        // TODO Should these two coalesce?
        public static Result<T> operator |(Result<T> x, Okay<T> y) => x.Or(Result.Okay(y.Value));

        public static Result<T> operator |(Result<T> x, T y) => x.Or(Result.Okay(y));

        public static Result<T> operator |(Result<T> x, Error y) => x.Or(Result.Fail<T>(y));

        public static Result<T> operator |(Result<T> x, Fail<Error> y) => x.Or(Result.Fail<T>(y.Error));

        public static Result<T> operator &(Result<T> x, Error y) => Result.Fail<T>(y);

        public static Result<T> operator &(Result<T> x, Fail<Error> y) => Result.Fail<T>(y.Error);

    }

    extension<T, TResult>(Result<T> self)
    {
        public static Result<TResult> operator &(Result<T> x, TResult y) => x.And(Result.Okay(y));
        
        public static Result<TResult> operator &(Result<T> x, Okay<TResult> y) => x.And(Result.Okay(y.Value));

        public static Result<TResult> operator &(Result<T> x, Result<TResult> y) => x.And(y);
    }
}
