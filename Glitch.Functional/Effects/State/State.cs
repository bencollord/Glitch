namespace Glitch.Functional.Effects;

[Monad]
public delegate StateResult<S, T> State<S, T>(S state);