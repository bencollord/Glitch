using Glitch.Functional.Parsing.Results;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional.Parsing;

using static Parse;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, T> Guard(Func<T, bool> predicate) => source.Guard(predicate, ParseError.Empty);

        public IParser<TToken, T> Guard(Func<T, bool> predicate, ParseError error) => source.Guard(predicate, _ => error);

        public IParser<TToken, T> Guard(Func<T, bool> predicate, Func<T, ParseError> error) => new GuardParser<TToken, T>(source, predicate, error);
    }
}