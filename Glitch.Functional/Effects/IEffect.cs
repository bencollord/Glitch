using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects
{
    [Monad]
    public interface IEffect<TInput, TOutput>
    {
        IEffect<TInput, TResult> Select<TResult>(Func<TOutput, TResult> map);
        IEffect<TInput, TOutput> SelectError(Func<Error, Error> map);
        IEffect<TInput, TResult> AndThen<TResult>(Func<TOutput, IEffect<TInput, TResult>> bind);
        virtual IEffect<TInput, TResult> AndThen<TElement, TResult>(Func<TOutput, IEffect<TInput, TElement>> bind, Func<TOutput, TElement, TResult> project)
            => AndThen(x => bind(x).Select(project.Curry(x)));

        IEffect<TInput, TResult> Match<TResult>(Func<TOutput, TResult> okay, Func<Error, TResult> error);

        IEffect<TInput, TOutput> Catch<TException>(Func<TException, TOutput> map) where TException : Exception;

        Expected<TOutput> Run(TInput input);

        virtual IEffect<TInput, TResult> Cast<TResult>() => Select(x => (TResult)(dynamic)x!);
        
        virtual IEffect<TInput, TOutput> SelectError<TError>(Func<TError, Error> map) 
            where TError : Error
            => SelectError(error => error is TError derived ? map(derived) : error);

        virtual IEffect<TInput, TOutput> Do(Action<TOutput> action)
            => Select(v =>
            {
                action(v);
                return v;
            });

        public static virtual IEffect<TInput, TOutput> operator >>(IEffect<TInput, TOutput> x, IEffect<TInput, TOutput> y) => x.AndThen(_ => y);
        public static virtual IEffect<TInput, TOutput> operator >>(IEffect<TInput, TOutput> x, IEffect<TInput, Unit> y) => x.AndThen(v => y.Select(_ => v));
    }
}