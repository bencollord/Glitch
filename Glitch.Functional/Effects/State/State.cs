namespace Glitch.Functional.Effects;

public delegate StateResult<S, T> State<S, T>(S state);