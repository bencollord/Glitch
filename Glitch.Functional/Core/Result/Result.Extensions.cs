namespace Glitch.Functional;

public static partial class ResultExtensions
{
    extension<T, E>(Result<Result<T, E>, E> source)
    {
        public Result<T, E> Flatten() => source.AndThen(Identity);
    }

    extension<T, E>(Result<Option<T>, E> nested)
    {
        public Option<Result<T, E>> Invert() =>
            nested.Match(
                okay: opt => opt * Result.Okay<T, E>,
                fail: err => Option.Some(err) * Result.Fail<T, E>);
    }
}