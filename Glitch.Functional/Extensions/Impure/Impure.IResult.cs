namespace Glitch.Functional.Extensions.Impure;

using static FN;

public static partial class ImpureExtensions
{
    extension<T, E>(IResult<T, E> source)
    {
        public Unit Match(Action<T> okay, Action<E> error) => source.Match(okay.Return(Unit.Value), error.Return(Unit.Value));
    }

    extension<E>(IResult<bool, E> self)
    {
        public Unit Match(Action @true, Action @false, Action<E> error)
            => self.Match(flag => flag ? @true.Return()() : @false.Return()(), error.Return());

        public Unit Match(Action<Unit> @true, Action<Unit> @false, Action<E> error)
            => self.Match(flag => flag ? @true.Return()(default) : @false.Return()(default), error.Return());
    }
}
