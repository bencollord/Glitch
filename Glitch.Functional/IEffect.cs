
namespace Glitch.Functional
{
    public interface IEffect<TInput, TOutput>
    {
        IEffect<TInput, TResult> Map<TResult>(Func<TOutput, TResult> map);
        IEffect<TInput, TOutput> MapError(Func<Error, Error> map);
        IEffect<TInput, TOutput> MapError<TError>(Func<TError, Error> map) where TError : Error;
        IEffect<TInput, TResult> Cast<TResult>();

        IEffect<TInput, TOutput> Guard(Func<TOutput, bool> predicate, Func<TOutput, Error> error);

        IEffect<TInput, TResult> And<TResult>(IEffect<TInput, TResult> other);
        IEffect<TInput, TResult> AndThen<TResult>(Func<TOutput, IEffect<TInput, TResult>> bind);
        IEffect<TInput, TOutput> Or(IEffect<TInput, TOutput> other);
        IEffect<TInput, TOutput> OrElse(Func<Error, IEffect<TInput, TOutput>> other);

        IEffect<TInput, TOutput> Catch<TException>(Func<TException, Error> map) where TException : Exception;
        IEffect<TInput, TOutput> Catch<TException>(Func<TException, TOutput> map) where TException : Exception;

        IResult<TOutput, Error> Run(TInput input);

        public static virtual IEffect<TInput, TOutput> operator &(IEffect<TInput, TOutput> x, IEffect<TInput, TOutput> y) => x.And(y);
        public static IEffect<TInput, TOutput> operator |(IEffect<TInput, TOutput> x, IEffect<TInput, TOutput> y) => x.Or(y);
        public static IEffect<TInput, TOutput> operator >>(IEffect<TInput, TOutput> x, IEffect<TInput, TOutput> y) => x.AndThen(_ => y);
        public static IEffect<TInput, TOutput> operator >>(IEffect<TInput, TOutput> x, IEffect<TInput, Unit> y) => x.AndThen(v => y.Map(_ => v));
    }
}