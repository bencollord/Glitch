namespace Glitch.Functional;

public static partial class ResultExtensions
{
    extension<T, E>(IResult<T, E> source)
    {
        public Unit Match(Action<T> okay, Action<E> error) => source.Match(okay.Return(Unit.Value), error.Return(Unit.Value));

        public TResult Match<TResult>(Func<T, TResult> okay, Func<TResult> error)
            => source.Match(okay, _ => error());

        public TResult Match<TResult>(Func<T, TResult> okay, TResult error)
            => source.IsOkay ? okay(source.Unwrap()) : error; // Avoid unnecessary delegate
    }

    extension<E>(IResult<bool, E> self)
    {
        public Unit Match(Action @true, Action @false, Action<E> error)
            => self.Match(flag => flag ? @true.Return()() : @false.Return()(), error.Return());

        public Unit Match(Action<Unit> @true, Action<Unit> @false, Action<E> error)
            => self.Match(flag => flag ? @true.Return()(default) : @false.Return()(default), error.Return());

        public T Match<T>(Func<Unit, T> @true, Func<Unit, T> @false, Func<E, T> error)
            => self.Match(flag => flag ? @true(default) : @false(default), error);

        public T Match<T>(Func<T> @true, Func<T> @false, Func<E, T> error)
            => self.Match(flag => flag ? @true() : @false(), error);
    }
}