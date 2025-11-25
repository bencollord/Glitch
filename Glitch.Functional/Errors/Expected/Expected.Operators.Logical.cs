namespace Glitch.Functional.Errors;

// Instance
public partial record Expected<T>
{
    public static bool operator true(Expected<T> result) => result.IsOkay;

    public static bool operator false(Expected<T> result) => result.IsFail;

    // Short circuiting
    public static Expected<T> operator &(Expected<T> x, Expected<T> y) => x.And(y);

    public static Expected<T> operator |(Expected<T> x, Expected<T> y) => x.Or(y);
}

// Extensions
public static partial class ExpectedExtensions
{
    extension<T>(Expected<T> self)
    {
        // TODO Should these two coalesce?
        public static Expected<T> operator |(Expected<T> x, Okay<T> y) => x.Or(Expected.Okay(y.Value));

        public static Expected<T> operator |(Expected<T> x, T y) => x.Or(Expected.Okay(y));

        public static Expected<T> operator |(Expected<T> x, Error y) => x.Or(Expected.Fail<T>(y));

        public static Expected<T> operator |(Expected<T> x, Fail<Error> y) => x.Or(Expected.Fail<T>(y.Error));

        public static Expected<T> operator &(Expected<T> x, Error y) => Expected.Fail<T>(y);

        public static Expected<T> operator &(Expected<T> x, Fail<Error> y) => Expected.Fail<T>(y.Error);

    }

    extension<T, TResult>(Expected<T> self)
    {
        public static Expected<TResult> operator &(Expected<T> x, TResult y) => x.And(Expected.Okay(y));
        
        public static Expected<TResult> operator &(Expected<T> x, Okay<TResult> y) => x.And(Expected.Okay(y.Value));

        public static Expected<TResult> operator &(Expected<T> x, Expected<TResult> y) => x.And(y);
    }
}
