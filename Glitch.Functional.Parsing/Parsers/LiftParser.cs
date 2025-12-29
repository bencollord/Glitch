
namespace Glitch.Functional.Parsing;

internal class LiftParser<TToken, T> : IParser<TToken, T>
{
    private ParseRunner<TToken, T> runner;

    internal LiftParser(ParseRunner<TToken, T> runner)
    {
        this.runner = runner;
    }

    public IParseResult<TToken, T> Execute(ITokenSequence<TToken> input) => runner(input);
}