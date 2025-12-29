namespace Glitch.Functional.Parsing;

public delegate IParseResult<TToken, T> ParseRunner<TToken, out T>(ITokenSequence<TToken> tokens);