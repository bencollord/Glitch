using Glitch.Functional.Errors;
using Glitch.Functional.Parsing.Legacy.Input;
using Glitch.Functional.Parsing.Legacy.Results;

namespace Glitch.Functional.Parsing.Legacy;

public abstract partial class Parser<TToken, T>
{
    public abstract ParseResult<TToken, T> Execute(TokenSequence<TToken> input);

    public virtual Expected<T> TryParse(TokenSequence<TToken> input)
        => Execute(input).Match(ok => Expected.Okay(ok.Value),
                                err => Expected.Fail<T>(err.Message));

    public virtual T Parse(TokenSequence<TToken> input)
        => Execute(input).Match(ok => ok.Value,
                                err => throw ParseException.FromError(err));
}
