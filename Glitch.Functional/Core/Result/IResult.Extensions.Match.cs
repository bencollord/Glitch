namespace Glitch.Functional;

public static partial class ResultExtensions
{
    extension<T, E>(IResult<T, E> source)
    {
        public TResult Match<TResult>(Func<T, TResult> okay, Func<TResult> error)
            => source.Match(okay, _ => error());

        public TResult Match<TResult>(Func<T, TResult> okay, TResult error)
            => source.IsOkay ? okay(source.Unwrap()) : error; // Avoid unnecessary delegate
    }

    extension<E>(IResult<bool, E> self)
    {
        public T Match<T>(Func<Unit, T> @true, Func<Unit, T> @false, Func<E, T> error)
            => self.Match(flag => flag ? @true(default) : @false(default), error);

        public T Match<T>(Func<T> @true, Func<T> @false, Func<E, T> error)
            => self.Match(flag => flag ? @true() : @false(), error);
    }
}