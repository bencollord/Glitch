namespace Glitch.Functional.Effects;

public interface IStateful<S, T>
{
    StateResult<S, T> Run(S state);
}
