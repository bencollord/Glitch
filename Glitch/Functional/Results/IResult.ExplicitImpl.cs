
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Glitch.Functional.Results
{
    /**
     * Since <see cref="IResult{T, E}"/> is experimental and involves
     * some messiness with explicit interface implementations, I'm isolating
     * all of the implementation stuff that's in flux here so it's easily accessible.
     */
    public readonly partial struct Option<T>
        : IResult<T, Unit>,
          IResultFactory<T, Unit>
    {
        bool IResult.IsError => IsNone;
        bool IResult.IsOkay => IsSome;

        static IResult<T, Unit> IResultFactory<T, Unit>.Okay(T value) => Some(value);
        static IResult<T, Unit> IResultFactory<T, Unit>.Fail(Unit _) => None;

        IResult<TResult, Unit> IResult<T, Unit>.Select<TResult>(Func<T, TResult> map) => Select(map);
        IResult<T, EResult> IResult<T, Unit>.SelectError<EResult>(Func<Unit, EResult> map) => Match(Result.Okay<T, EResult>, unit => Result.Fail<T, EResult>(map(unit)));
    }

    public partial record Result<T, E>
        : IResult<T, E>,
          IResultFactory<T, E>
    {
        static IResult<T, E> IResultFactory<T, E>.Okay(T value) => Okay(value);
        static IResult<T, E> IResultFactory<T, E>.Fail(E error) => Fail(error);

        IResult<TResult, E> IResult<T, E>.Select<TResult>(Func<T, TResult> map) => Select(map);
        IResult<T, EResult> IResult<T, E>.SelectError<EResult>(Func<E, EResult> map) => SelectError(map);
    }

    public partial record Expected<T>
        : IResult<T, Error>,
          IResultFactory<T, Error>
    {
        static IResult<T, Error> IResultFactory<T, Error>.Okay(T value) => Okay(value);
        static IResult<T, Error> IResultFactory<T, Error>.Fail(Error error) => Fail(error);

        IResult<TResult, Error> IResult<T, Error>.Select<TResult>(Func<T, TResult> map) => Select(map);
        IResult<T, EResult> IResult<T, Error>.SelectError<EResult>(Func<Error, EResult> map) => Match(Result.Okay<T, EResult>, unit => Result.Fail<T, EResult>(map(unit)));
    }
}