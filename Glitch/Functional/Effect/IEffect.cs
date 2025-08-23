using Glitch.Functional.Attributes;

namespace Glitch.Functional
{
    [Monad]
    public interface IEffect<TInput, TOutput> : IGuardable<IEffect<TInput, TOutput>, TOutput, Error>
    {
        IEffect<TInput, TResult> Map<TResult>(Func<TOutput, TResult> map);
        IEffect<TInput, TOutput> MapError(Func<Error, Error> map);
        IEffect<TInput, TResult> AndThen<TResult>(Func<TOutput, IEffect<TInput, TResult>> bind);

        IEffect<TInput, TResult> Match<TResult>(Func<TOutput, TResult> ifOkay, Func<Error, TResult> ifFail);

        IEffect<TInput, TOutput> Catch<TException>(Func<TException, TOutput> map) where TException : Exception;

        Result<TOutput, Error> Run(TInput input);

        virtual IEffect<TInput, TResult> Cast<TResult>() => Map(x => (TResult)(dynamic)x!);
        
        virtual IEffect<TInput, TOutput> MapError<TError>(Func<TError, Error> map) 
            where TError : Error
            => MapError(error => error is TError derived ? map(derived) : error);

        virtual IEffect<TInput, TOutput> Do(Action<TOutput> action)
            => Map(v =>
            {
                action(v);
                return v;
            });

        public static virtual IEffect<TInput, TOutput> operator >>(IEffect<TInput, TOutput> x, IEffect<TInput, TOutput> y) => x.AndThen(_ => y);
        public static virtual IEffect<TInput, TOutput> operator >>(IEffect<TInput, TOutput> x, IEffect<TInput, Unit> y) => x.AndThen(v => y.Map(_ => v));
    }
}