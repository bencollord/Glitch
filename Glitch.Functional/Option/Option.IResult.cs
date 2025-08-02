namespace Glitch.Functional
{
    public readonly partial struct Option<T> : IResult<T, Unit>
    {
        bool IResult<T, Unit>.IsOkay => IsSome;

        bool IResult<T, Unit>.IsError => IsNone;

        IResult<T, Unit> IResult<T, Unit>.Guard(Func<T, bool> predicate, Func<T, Unit> error)
        {
            var filtered = Filter(predicate);

            // Ensure error runs in case it has side effects
            if (IsSome && filtered.IsNone)
            {
                error(value!);
            }

            return filtered;
        }

        IResult<TResult, Unit> IResult<T, Unit>.Map<TResult>(Func<T, TResult> map) => Map(map);

        IResult<T, TNewError> IResult<T, Unit>.MapError<TNewError>(Func<Unit, TNewError> map)
            => Match(some: Result.Okay<T, TNewError>,
                     none: _ => map(Unit.Value));
    }
}