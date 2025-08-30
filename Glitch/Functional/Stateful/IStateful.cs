namespace Glitch.Functional
{
    public interface IStateful<S, T>
    {
        StateResult<S, T> Run(S state);

        static virtual IStateful<S, T> operator >>(IStateful<S, T> x, IStateful<S, T> y) => State.AndThen(x, _ => y);
        static virtual IStateful<S, T> operator >>(IStateful<S, T> x, IStateful<S, Unit> y) => State.AndThen(x, _ => y, (v, _) => v);
    }
}
