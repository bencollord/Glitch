using System.Collections.Immutable;

namespace Glitch.Functional
{
    public interface IResult<TSuccess, TError>
    {
        bool IsOkay { get; }
        bool IsFail { get; }

        IResult<TResult, TError> Map<TResult>(Func<TSuccess, TResult> map);
        IResult<TSuccess, TNewError> MapError<TNewError>(Func<TError, TNewError> map);
        IResult<TResult, TError> Cast<TResult>();
        IResult<TSuccess, TError> Do(Action<TSuccess> action);
        IResult<TSuccess, TError> Guard(bool condition, Func<TSuccess, TError> error);
        
        IResult<TResult, TError> And<TResult>(IResult<TResult, TError> other);
        IResult<TResult, TError> AndThen<TResult>(Func<TSuccess, IResult<TResult, TError>> bind);

        IResult<TSuccess, TError> Or(IResult<TSuccess, TError> other);
        IResult<TSuccess, TNewError> OrElse<TNewError>(Func<TError, IResult<TSuccess, TNewError>> other);

        TResult Match<TResult>(Func<TSuccess, TResult> ifOkay, Func<TError, TResult> ifFail);
    }

    public static class IResultExtensions
    {
        public static FluentContext<TSource, TError, TResult> IfOkay<TSource, TError, TResult>(this IResult<TSource, TError> result, Func<TSource, TResult> ifOkay)
            => new(result, ifOkay);

        public static FluentActionContext<TSource, TError> IfOkay<TSource, TError>(this IResult<TSource, TError> result, Action<TSource> ifOkay)
            => new(result, ifOkay);

        public static FluentActionContext<TSource, TError> IfOkay<TSource, TError>(this IResult<TSource, TError> result, Func<TSource, Unit> ifOkay)
            => new(result, ifOkay);
    }

    public class FluentContext<TSource, TError, TResult>
    {
        private readonly IResult<TSource, TError> result;
        private readonly Func<TSource, TResult> ifOkay;

        internal FluentContext(IResult<TSource, TError> result, Func<TSource, TResult> ifOkay)
        {
            this.result = result;
            this.ifOkay = ifOkay;
        }

        public TResult Otherwise(TResult fallback) => Otherwise(_ => fallback);

        public TResult Otherwise(Func<TResult> fallback) => Otherwise(_ => fallback());

        public TResult Otherwise(Func<TError, TResult> fallback) => result.Match(ifOkay, fallback);

        public TResult? OtherwiseDefault() => default;

        public TResult OtherwiseThrow(Exception exception) => Otherwise(_ => throw exception);

        public TResult OtherwiseThrow() => OtherwiseThrow(e => new InvalidOperationException($"Attempted to unwrap a faulted result. Error: {e}"));

        public TResult OtherwiseThrow(Func<TError, Exception> exceptionFactory) => Otherwise(e => throw exceptionFactory(e));
    }

    public class FluentActionContext<TSource, TError>
    {
        private readonly IResult<TSource, TError> result;
        private readonly Action<TSource> ifOkay;
        private readonly ImmutableDictionary<Type, Action<TError>> errorHandlers;

        internal FluentActionContext(IResult<TSource, TError> result, Func<TSource, Unit> ifOkay)
            : this(result, new Action<TSource>(t => ifOkay(t))) { }

        internal FluentActionContext(IResult<TSource, TError> result, Action<TSource> ifOkay)
            : this(result, ifOkay, ImmutableDictionary<Type, Action<TError>>.Empty)
        {
        }

        private FluentActionContext(IResult<TSource, TError> result, Action<TSource> ifOkay, ImmutableDictionary<Type, Action<TError>> errorHandlers)
        {
            this.result = result;
            this.ifOkay = ifOkay;
            this.errorHandlers = errorHandlers;
        }

        public FluentActionContext<TSource, TError> Then(Action<TSource> action) => new(result, ifOkay + action);

        public FluentActionContext<TSource, TError> Then(Func<TSource, Unit> action) => Then(new Action<TSource>(t => action(t)));

        public FluentActionContext<TSource, TError> Catch<TDerivedError>(Action<TDerivedError> ifError)
                where TDerivedError : TError
                => new(result, ifOkay, errorHandlers.Add(typeof(TError), err => ifError((TDerivedError)err!)));

        public Unit OtherwiseDoNothing() => Otherwise(_ => { /* Nop */ });

        public Unit Otherwise(Action<TError> ifFail)
        {
            return result.Match(ifOkay.Return(), ifFail.Return());
        }

        public Unit OtherwiseThrow(Exception exception) => Otherwise(_ => throw exception);

        public Unit OtherwiseThrow() => OtherwiseThrow(e => new InvalidOperationException($"Attempted to unwrap a faulted result. Error: {e}"));

        public Unit OtherwiseThrow(Func<TError, Exception> exceptionFactory) => Otherwise(e => throw exceptionFactory(e));

        public IResult<TSource, TError> OtherwiseContinue() => result;
    }

    // Experimental
    public class FluentActionContext<TResultType, TSource, TError>
        where TResultType : IResult<TSource, TError>
    {
        private readonly TResultType result;
        private readonly Action<TSource> ifOkay;
        private readonly ImmutableDictionary<Type, Action<TError>> errorHandlers;

        internal FluentActionContext(TResultType result, Func<TSource, Unit> ifOkay)
            : this(result, new Action<TSource>(t => ifOkay(t))) { }

        internal FluentActionContext(TResultType result, Action<TSource> ifOkay)
            : this(result, ifOkay, ImmutableDictionary<Type, Action<TError>>.Empty)
        {
        }

        private FluentActionContext(TResultType result, Action<TSource> ifOkay, ImmutableDictionary<Type, Action<TError>> errorHandlers)
        {
            this.result = result;
            this.ifOkay = ifOkay;
            this.errorHandlers = errorHandlers;
        }

        public FluentActionContext<TResultType, TSource, TError> Then(Action<TSource> action) => new(result, ifOkay + action);

        public FluentActionContext<TResultType, TSource, TError> Then(Func<TSource, Unit> action) => Then(new Action<TSource>(t => action(t)));

        public FluentActionContext<TResultType, TSource, TError> Catch<TDerivedError>(Action<TDerivedError> ifError)
                where TDerivedError : TError
                => new(result, ifOkay, errorHandlers.Add(typeof(TError), err => ifError((TDerivedError)err!)));

        public Unit OtherwiseDoNothing() => Otherwise(_ => { /* Nop */ });

        public Unit Otherwise(Action<TError> ifFail)
        {
            return result.Match(ifOkay.Return(), ifFail.Return());
        }

        public Unit OtherwiseThrow(Exception exception) => Otherwise(_ => throw exception);

        public Unit OtherwiseThrow() => OtherwiseThrow(e => new InvalidOperationException($"Attempted to unwrap a faulted result. Error: {e}"));

        public Unit OtherwiseThrow(Func<TError, Exception> exceptionFactory) => Otherwise(e => throw exceptionFactory(e));

        public TResultType OtherwiseContinue() => result;
    }
}