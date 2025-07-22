
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public interface IEffect<TInput, TOutput>
    {
        IEffect<TInput, TResult> Map<TResult>(Func<TOutput, TResult> map);
        IEffect<TInput, TOutput> MapError(Func<Error, Error> map);

        IEffect<TInput, TOutput> Guard(Func<TOutput, bool> predicate, Func<TOutput, Error> error);

        IEffect<TInput, TResult> Match<TResult>(Func<TOutput, TResult> ifOkay, Func<Error, TResult> ifFail);

        IResult<TOutput, Error> Run(TInput input);

        virtual IEffect<TInput, TResult> AndThen<TResult>(Func<TOutput, IEffect<TInput, TResult>> bind)
            => new EffectContinuation<TInput, TOutput, TResult>(this, bind);
        virtual IEffect<TInput, TOutput> OrElse(Func<Error, IEffect<TInput, TOutput>> bind)
            => new EffectRecovery<TInput, TOutput>(this, bind);

        virtual IEffect<TInput, TOutput> MapError<TError>(Func<TError, Error> map) where TError : Error
            => MapError(error => error is TError derived ? map(derived) : error);

        virtual IEffect<TInput, TResult> Cast<TResult>() => Map(v => (TResult)(dynamic)v!);

        virtual IEffect<TInput, TOutput> Do(Action<TOutput> action)
            => Map(v =>
            {
                action(v);
                return v;
            });
        virtual IEffect<TInput, TResult> And<TResult>(IEffect<TInput, TResult> other)
            => AndThen(_ => other);

        virtual IEffect<TInput, TOutput> Or(IEffect<TInput, TOutput> other)
            => OrElse(_ => other);

        public static virtual IEffect<TInput, TOutput> operator &(IEffect<TInput, TOutput> x, IEffect<TInput, TOutput> y) => x.And(y);
        public static virtual IEffect<TInput, TOutput> operator |(IEffect<TInput, TOutput> x, IEffect<TInput, TOutput> y) => x.Or(y);
        public static virtual IEffect<TInput, TOutput> operator >>(IEffect<TInput, TOutput> x, IEffect<TInput, TOutput> y) => x.AndThen(_ => y);
        public static virtual IEffect<TInput, TOutput> operator >>(IEffect<TInput, TOutput> x, IEffect<TInput, Unit> y) => x.AndThen(v => y.Map(_ => v));
    }

    public static class IEffectExtensions
    {
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> Select<TInput, TOutput, TResult>(this IEffect<TInput, TOutput> result, Func<TOutput, TResult> map)
            => result.Map(map);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> SelectMany<TInput, TOutput, TResult>(this IEffect<TInput, TOutput> result, Func<TOutput, IEffect<TInput, TResult>> bind)
            => result.AndThen(bind);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> SelectMany<TInput, TOutput, TElement, TResult>(this IEffect<TInput, TOutput> result, Func<TOutput, IEffect<TInput, TElement>> bind, Func<TOutput, TElement, TResult> projection)
            => result.AndThen(v => bind(v).Map(x => projection(v, x)));

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TOutput> Where<TInput, TOutput>(this IEffect<TInput, TOutput> result, Func<TOutput, bool> predicate)
        {
            return result.Guard(predicate, _ => Error.Empty);
        }

    }
}