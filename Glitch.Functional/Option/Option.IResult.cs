namespace Glitch.Functional
{
    public readonly partial struct Option<T> : IResult<T, Nothing>
    {
        bool IResult<T, Nothing>.IsOkay => IsSome;

        bool IResult<T, Nothing>.IsError => IsNone;

        IResult<T, Nothing> IResult<T, Nothing>.Guard(Func<T, bool> predicate, Func<T, Nothing> error)
        {
            var filtered = Filter(predicate);

            // Ensure error runs in case it has side effects
            if (IsSome && filtered.IsNone)
            {
                error(value!);
            }

            return filtered;
        }

        IResult<TResult, Nothing> IResult<T, Nothing>.Map<TResult>(Func<T, TResult> map) => Map(map);

        IResult<T, TNewError> IResult<T, Nothing>.MapError<TNewError>(Func<Nothing, TNewError> map)
            => Match(some: Result.Okay<T, TNewError>,
                     none: _ => map(Nothing.Value));
    }
}