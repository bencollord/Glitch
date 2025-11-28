namespace Glitch.Functional;

public interface IStateful<S, T>
{
    StateResult<S, T> Run(S state);
}
