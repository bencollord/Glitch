
namespace Glitch.Functional.Experimental
{
    public interface IResult<TSuccess, TError>
    {
        bool IsOkay { get; }
        bool IsFail { get; }

        IResult<TResult, TError> And<TResult>(IResult<TResult, TError> other);
        IResult<TResult, TError> AndThen<TElement, TResult>(Func<TSuccess, IResult<TElement, TError>> bind, Func<TSuccess, TElement, TResult> project);
        IResult<TResult, TError> AndThen<TResult>(Func<TSuccess, IResult<TResult, TError>> bind);
        IResult<TResult, TError> Apply<TResult>(IResult<Func<TSuccess, TResult>, TError> function);
        IResult<TResult, TError> Cast<TResult>();
        IResult<TResult, TError> CastOr<TResult>(TError error);
        IResult<TResult, TError> CastOrElse<TResult>(Func<TSuccess, TError> error);
        IResult<TResult, TError> Choose<TResult>(Func<TSuccess, IResult<TResult, TError>> ifOkay, Func<TError, IResult<TResult, TError>> ifFail);
        IResult<TSuccess, TError> Do(Action<TSuccess> action);
        IResult<TSuccess, TError> Do(Func<TSuccess, Terminal> action);
        bool Equals(object? obj);
        bool Equals(IResult<TSuccess, TError>? other);
        IResult<TSuccess, TError> Filter(Func<TSuccess, bool> predicate);
        int GetHashCode();
        IResult<TSuccess, TError> Guard(bool condition, TError error);
        IResult<TSuccess, TError> Guard(bool condition, Func<TSuccess, TError> error);
        IResult<TSuccess, TError> Guard(Func<TSuccess, bool> predicate, TError error);
        IResult<TSuccess, TError> Guard(Func<TSuccess, bool> predicate, Func<TSuccess, TError> error);
        IResult<TSuccess, TError> GuardNot(bool condition, TError error);
        IResult<TSuccess, TError> GuardNot(bool condition, Func<TSuccess, TError> error);
        IResult<TSuccess, TError> GuardNot(Func<TSuccess, bool> predicate, TError error);
        IResult<TSuccess, TError> GuardNot(Func<TSuccess, bool> predicate, Func<TSuccess, TError> error);
        IResult<TSuccess, TError> IfFail(Action action);
        IResult<TSuccess, TError> IfFail(Action<TError> action);
        TSuccess IfFail(Func<TError, TSuccess> fallback);
        TSuccess IfFail(Func<TSuccess> fallback);
        TSuccess IfFail(TSuccess fallback);
        IResult<TSuccess, TError> IfOkay(Action<TSuccess> action);
        IResult<TSuccess, TError> IfOkay(Func<TSuccess, Terminal> action);
        bool IsFailAnd(Func<TError, bool> predicate);
        bool IsOkayAnd(Func<TSuccess, bool> predicate);
        IEnumerable<TSuccess> Iterate();
        IResult<TResult, TError> Map<TResult>(Func<TSuccess, TResult> map);
        IResult<TSuccess, TNewError> MapError<TNewError>(Func<TError, TNewError> map);
        IResult<TResult, TError> MapOr<TResult>(Func<TSuccess, TResult> map, TError ifFail);
        IResult<TResult, TNewError> MapOrElse<TResult, TNewError>(Func<TSuccess, TResult> map, Func<TError, TNewError> ifFail);
        TResult Match<TResult>(Func<TSuccess, TResult> ifOkay, Func<TError, TResult> ifFail);
        TResult Match<TResult>(Func<TSuccess, TResult> ifOkay, Func<TResult> ifFail);
        TResult Match<TResult>(Func<TSuccess, TResult> ifOkay, TResult ifFail);
        IResult<TSuccess, TError> Or(IResult<TSuccess, TError> other);
        IResult<TSuccess, TNewError> OrElse<TNewError>(Func<TError, IResult<TSuccess, TNewError>> other);
        IResult<Func<T2, TResult>, TError> PartialMap<T2, TResult>(Func<TSuccess, T2, TResult> map);
        bool TryUnwrap(out TSuccess result);
        bool TryUnwrapError(out TError result);
        TSuccess Unwrap();
        TError UnwrapError();
        TError UnwrapErrorOr(TError fallback);
        TError UnwrapErrorOrElse(Func<TError> fallback);
        TError UnwrapErrorOrElse(Func<TSuccess, TError> fallback);
        TSuccess UnwrapOr(TSuccess fallback);
        IResult<TResult, TError> Zip<TOther, TResult>(IResult<TOther, TError> other, Func<TSuccess, TOther, TResult> zipper);
        IResult<(TSuccess, TOther), TError> Zip<TOther>(IResult<TOther, TError> other);
    }

    public record Res<T> : IResult<T, Error>
    {
        public bool IsOkay => throw new NotImplementedException();

        public bool IsFail => throw new NotImplementedException();

        public IResult<TResult, Error> And<TResult>(IResult<TResult, Error> other)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Error> AndThen<TElement, TResult>(Func<T, IResult<TElement, Error>> bind, Func<T, TElement, TResult> project)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Error> AndThen<TResult>(Func<T, IResult<TResult, Error>> bind)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Error> Apply<TResult>(IResult<Func<T, TResult>, Error> function)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Error> Cast<TResult>()
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Error> CastOr<TResult>(Error error)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Error> CastOrElse<TResult>(Func<T, Error> error)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Error> Choose<TResult>(Func<T, IResult<TResult, Error>> ifOkay, Func<Error, IResult<TResult, Error>> ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> Do(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> Do(Func<T, Terminal> action)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IResult<T, Error>? other)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> Filter(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> Guard(bool condition, Error error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> Guard(bool condition, Func<T, Error> error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> Guard(Func<T, bool> predicate, Error error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> Guard(Func<T, bool> predicate, Func<T, Error> error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> GuardNot(bool condition, Error error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> GuardNot(bool condition, Func<T, Error> error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> GuardNot(Func<T, bool> predicate, Error error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> GuardNot(Func<T, bool> predicate, Func<T, Error> error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> IfFail(Action action)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> IfFail(Action<Error> action)
        {
            throw new NotImplementedException();
        }

        public T IfFail(Func<Error, T> fallback)
        {
            throw new NotImplementedException();
        }

        public T IfFail(Func<T> fallback)
        {
            throw new NotImplementedException();
        }

        public T IfFail(T fallback)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> IfOkay(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> IfOkay(Func<T, Terminal> action)
        {
            throw new NotImplementedException();
        }

        public bool IsFailAnd(Func<Error, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public bool IsOkayAnd(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Iterate()
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Error> Map<TResult>(Func<T, TResult> map)
        {
            throw new NotImplementedException();
        }

        public IResult<T, TNewError> MapError<TNewError>(Func<Error, TNewError> map)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Error> MapOr<TResult>(Func<T, TResult> map, Error ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TNewError> MapOrElse<TResult, TNewError>(Func<T, TResult> map, Func<Error, TNewError> ifFail)
        {
            throw new NotImplementedException();
        }

        public TResult Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> ifFail)
        {
            throw new NotImplementedException();
        }

        public TResult Match<TResult>(Func<T, TResult> ifOkay, Func<TResult> ifFail)
        {
            throw new NotImplementedException();
        }

        public TResult Match<TResult>(Func<T, TResult> ifOkay, TResult ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Error> Or(IResult<T, Error> other)
        {
            throw new NotImplementedException();
        }

        public IResult<T, TNewError> OrElse<TNewError>(Func<Error, IResult<T, TNewError>> other)
        {
            throw new NotImplementedException();
        }

        public IResult<Func<T2, TResult>, Error> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
        {
            throw new NotImplementedException();
        }

        public bool TryUnwrap(out T result)
        {
            throw new NotImplementedException();
        }

        public bool TryUnwrapError(out Error result)
        {
            throw new NotImplementedException();
        }

        public T Unwrap()
        {
            throw new NotImplementedException();
        }

        public Error UnwrapError()
        {
            throw new NotImplementedException();
        }

        public Error UnwrapErrorOr(Error fallback)
        {
            throw new NotImplementedException();
        }

        public Error UnwrapErrorOrElse(Func<Error> fallback)
        {
            throw new NotImplementedException();
        }

        public Error UnwrapErrorOrElse(Func<T, Error> fallback)
        {
            throw new NotImplementedException();
        }

        public T UnwrapOr(T fallback)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Error> Zip<TOther, TResult>(IResult<TOther, Error> other, Func<T, TOther, TResult> zipper)
        {
            throw new NotImplementedException();
        }

        public IResult<(T, TOther), Error> Zip<TOther>(IResult<TOther, Error> other)
        {
            throw new NotImplementedException();
        }
    }

    public struct Opt<T> : IResult<T, Terminal>
    {
        public bool IsOkay => throw new NotImplementedException();

        public bool IsFail => throw new NotImplementedException();

        public IResult<TResult, Terminal> And<TResult>(IResult<TResult, Terminal> other)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Terminal> AndThen<TElement, TResult>(Func<T, IResult<TElement, Terminal>> bind, Func<T, TElement, TResult> project)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Terminal> AndThen<TResult>(Func<T, IResult<TResult, Terminal>> bind)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Terminal> Apply<TResult>(IResult<Func<T, TResult>, Terminal> function)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Terminal> Cast<TResult>()
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Terminal> CastOr<TResult>(Terminal error)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Terminal> CastOrElse<TResult>(Func<T, Terminal> error)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Terminal> Choose<TResult>(Func<T, IResult<TResult, Terminal>> ifOkay, Func<Terminal, IResult<TResult, Terminal>> ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> Do(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> Do(Func<T, Terminal> action)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IResult<T, Terminal>? other)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> Filter(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> Guard(bool condition, Terminal error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> Guard(bool condition, Func<T, Terminal> error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> Guard(Func<T, bool> predicate, Terminal error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> Guard(Func<T, bool> predicate, Func<T, Terminal> error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> GuardNot(bool condition, Terminal error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> GuardNot(bool condition, Func<T, Terminal> error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> GuardNot(Func<T, bool> predicate, Terminal error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> GuardNot(Func<T, bool> predicate, Func<T, Terminal> error)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> IfFail(Action action)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> IfFail(Action<Terminal> action)
        {
            throw new NotImplementedException();
        }

        public T IfFail(Func<Terminal, T> fallback)
        {
            throw new NotImplementedException();
        }

        public T IfFail(Func<T> fallback)
        {
            throw new NotImplementedException();
        }

        public T IfFail(T fallback)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> IfOkay(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> IfOkay(Func<T, Terminal> action)
        {
            throw new NotImplementedException();
        }

        public bool IsFailAnd(Func<Terminal, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public bool IsOkayAnd(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Iterate()
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Terminal> Map<TResult>(Func<T, TResult> map)
        {
            throw new NotImplementedException();
        }

        public IResult<T, TNewError> MapError<TNewError>(Func<Terminal, TNewError> map)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Terminal> MapOr<TResult>(Func<T, TResult> map, Terminal ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TNewError> MapOrElse<TResult, TNewError>(Func<T, TResult> map, Func<Terminal, TNewError> ifFail)
        {
            throw new NotImplementedException();
        }

        public TResult Match<TResult>(Func<T, TResult> ifOkay, Func<Terminal, TResult> ifFail)
        {
            throw new NotImplementedException();
        }

        public TResult Match<TResult>(Func<T, TResult> ifOkay, Func<TResult> ifFail)
        {
            throw new NotImplementedException();
        }

        public TResult Match<TResult>(Func<T, TResult> ifOkay, TResult ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<T, Terminal> Or(IResult<T, Terminal> other)
        {
            throw new NotImplementedException();
        }

        public IResult<T, TNewError> OrElse<TNewError>(Func<Terminal, IResult<T, TNewError>> other)
        {
            throw new NotImplementedException();
        }

        public IResult<Func<T2, TResult>, Terminal> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
        {
            throw new NotImplementedException();
        }

        public bool TryUnwrap(out T result)
        {
            throw new NotImplementedException();
        }

        public bool TryUnwrapError(out Terminal result)
        {
            throw new NotImplementedException();
        }

        public T Unwrap()
        {
            throw new NotImplementedException();
        }

        public Terminal UnwrapError()
        {
            throw new NotImplementedException();
        }

        public Terminal UnwrapErrorOr(Terminal fallback)
        {
            throw new NotImplementedException();
        }

        public Terminal UnwrapErrorOrElse(Func<Terminal> fallback)
        {
            throw new NotImplementedException();
        }

        public Terminal UnwrapErrorOrElse(Func<T, Terminal> fallback)
        {
            throw new NotImplementedException();
        }

        public T UnwrapOr(T fallback)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, Terminal> Zip<TOther, TResult>(IResult<TOther, Terminal> other, Func<T, TOther, TResult> zipper)
        {
            throw new NotImplementedException();
        }

        public IResult<(T, TOther), Terminal> Zip<TOther>(IResult<TOther, Terminal> other)
        {
            throw new NotImplementedException();
        }
    }

    public record Res<TSucc, TErr> : IResult<TSucc, TErr>
    {
        public bool IsOkay => throw new NotImplementedException();

        public bool IsFail => throw new NotImplementedException();

        public IResult<TResult, TErr> And<TResult>(IResult<TResult, TErr> other)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TErr> AndThen<TElement, TResult>(Func<TSucc, IResult<TElement, TErr>> bind, Func<TSucc, TElement, TResult> project)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TErr> AndThen<TResult>(Func<TSucc, IResult<TResult, TErr>> bind)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TErr> Apply<TResult>(IResult<Func<TSucc, TResult>, TErr> function)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TErr> Cast<TResult>()
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TErr> CastOr<TResult>(TErr error)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TErr> CastOrElse<TResult>(Func<TSucc, TErr> error)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TErr> Choose<TResult>(Func<TSucc, IResult<TResult, TErr>> ifOkay, Func<TErr, IResult<TResult, TErr>> ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> Do(Action<TSucc> action)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> Do(Func<TSucc, Terminal> action)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IResult<TSucc, TErr>? other)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> Filter(Func<TSucc, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> Guard(bool condition, TErr error)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> Guard(bool condition, Func<TSucc, TErr> error)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> Guard(Func<TSucc, bool> predicate, TErr error)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> Guard(Func<TSucc, bool> predicate, Func<TSucc, TErr> error)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> GuardNot(bool condition, TErr error)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> GuardNot(bool condition, Func<TSucc, TErr> error)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> GuardNot(Func<TSucc, bool> predicate, TErr error)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> GuardNot(Func<TSucc, bool> predicate, Func<TSucc, TErr> error)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> IfFail(Action action)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> IfFail(Action<TErr> action)
        {
            throw new NotImplementedException();
        }

        public TSucc IfFail(Func<TErr, TSucc> fallback)
        {
            throw new NotImplementedException();
        }

        public TSucc IfFail(Func<TSucc> fallback)
        {
            throw new NotImplementedException();
        }

        public TSucc IfFail(TSucc fallback)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> IfOkay(Action<TSucc> action)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> IfOkay(Func<TSucc, Terminal> action)
        {
            throw new NotImplementedException();
        }

        public bool IsFailAnd(Func<TErr, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public bool IsOkayAnd(Func<TSucc, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TSucc> Iterate()
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TErr> Map<TResult>(Func<TSucc, TResult> map)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TNewError> MapError<TNewError>(Func<TErr, TNewError> map)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TErr> MapOr<TResult>(Func<TSucc, TResult> map, TErr ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TNewError> MapOrElse<TResult, TNewError>(Func<TSucc, TResult> map, Func<TErr, TNewError> ifFail)
        {
            throw new NotImplementedException();
        }

        public TResult Match<TResult>(Func<TSucc, TResult> ifOkay, Func<TErr, TResult> ifFail)
        {
            throw new NotImplementedException();
        }

        public TResult Match<TResult>(Func<TSucc, TResult> ifOkay, Func<TResult> ifFail)
        {
            throw new NotImplementedException();
        }

        public TResult Match<TResult>(Func<TSucc, TResult> ifOkay, TResult ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TErr> Or(IResult<TSucc, TErr> other)
        {
            throw new NotImplementedException();
        }

        public IResult<TSucc, TNewError> OrElse<TNewError>(Func<TErr, IResult<TSucc, TNewError>> other)
        {
            throw new NotImplementedException();
        }

        public IResult<Func<T2, TResult>, TErr> PartialMap<T2, TResult>(Func<TSucc, T2, TResult> map)
        {
            throw new NotImplementedException();
        }

        public bool TryUnwrap(out TSucc result)
        {
            throw new NotImplementedException();
        }

        public bool TryUnwrapError(out TErr result)
        {
            throw new NotImplementedException();
        }

        public TSucc Unwrap()
        {
            throw new NotImplementedException();
        }

        public TErr UnwrapError()
        {
            throw new NotImplementedException();
        }

        public TErr UnwrapErrorOr(TErr fallback)
        {
            throw new NotImplementedException();
        }

        public TErr UnwrapErrorOrElse(Func<TErr> fallback)
        {
            throw new NotImplementedException();
        }

        public TErr UnwrapErrorOrElse(Func<TSucc, TErr> fallback)
        {
            throw new NotImplementedException();
        }

        public TSucc UnwrapOr(TSucc fallback)
        {
            throw new NotImplementedException();
        }

        public IResult<TResult, TErr> Zip<TOther, TResult>(IResult<TOther, TErr> other, Func<TSucc, TOther, TResult> zipper)
        {
            throw new NotImplementedException();
        }

        public IResult<(TSucc, TOther), TErr> Zip<TOther>(IResult<TOther, TErr> other)
        {
            throw new NotImplementedException();
        }
    }
}