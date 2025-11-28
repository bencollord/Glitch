namespace Glitch.Functional;

public static partial class ResultExtensions
{
    extension<T, E>(Result<T, E> self)
    {
        public static Result<T, E> operator |(Result<T, E> x, Okay<T> y) => x.Or(Result.Okay<T, E>(y.Value));

        public static Result<T, E> operator &(Result<T, E> x, Fail<E> y) => x.And(Result.Fail<T, E>(y.Error));
    }

    extension<T, E, TResult>(Result<T, E> self)
    {
        public static Result<TResult, E> operator &(Result<T, E> x, Result<TResult, E> y) => x.And(y);

        public static Result<TResult, E> operator &(Result<T, E> x, Okay<TResult> y) => x.And(Result.Okay<TResult, E>(y.Value));
        
        public static Result<TResult, E> operator &(Result<T, E> x, TResult y) => x.And(Result.Okay<TResult, E>(y));
    }

    extension<T, E, EResult>(Result<T, E> self)
    {
        public static Result<T, EResult> operator |(Result<T, E> x, Result<T, EResult> y) => x.Or(y);

        public static Result<T, EResult> operator |(Result<T, E> x, Fail<EResult> y) => x.Or(Result.Fail<T, EResult>(y.Error));
        
        public static Result<T, EResult> operator |(Result<T, E> x, EResult y) => x.Or(Result.Fail<T, EResult>(y));
    }
}