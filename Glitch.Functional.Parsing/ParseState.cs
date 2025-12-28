using Glitch.Functional.Parsing.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glitch.Functional.Parsing;

/// <summary>
/// Represents the current parse state of a given parser.
/// </summary>
/// <remarks>
/// UNDONE Currently only holds the remaining input tokens,
/// but needs to be extended to include, errors, labels, expectations,
/// and possibly values.
/// </remarks>
/// <typeparam name="TToken"></typeparam>
public record ParseState<TToken>(ITokenSequence<TToken> Remaining)
{
    /// <summary>
    /// Returns true if the current state is EOF.
    /// </summary>
    /// <remarks>
    /// Currently just passes through to <see cref="Remaining"/>,
    /// but I put this property here to avoid Law Of Demeter violations
    /// that may become problematic when I get around to adding more features
    /// to this class.
    /// </remarks>
    public bool IsEnd => Remaining.IsEnd;

    // Module
    // ============================================================================================
    public static IParser<TToken, ParseState<TToken>> Get => new StateParser<TToken>();

    public static IParser<TToken, Unit> Put(ParseState<TToken> state) => new ReturnParser<TToken, Unit>(ParseResult.Okay(Unit.Value, state.Remaining));

    public static IParser<TToken, Unit> Modify(Func<ParseState<TToken>, ParseState<TToken>> update) =>
        from s in Get
        from _ in Put(update(s))
        select Unit.Value;
}
