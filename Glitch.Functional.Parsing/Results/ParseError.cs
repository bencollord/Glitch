using Glitch.Functional;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Glitch.Functional.Parsing.Results;

using static Option;

public record ParseError
{
    public static readonly ParseError Empty = new(ParseErrorKind.Empty);

    public static readonly ParseError Unknown = new(ParseErrorKind.Unknown);

    private Option<string> label;
    private ParseErrorKind kind;
    private Expectation expectations;

    private ParseError(ParseErrorKind kind, Option<string> label = default, Option<Expectation> expectations = default)
    {
        Option<Exception> exception = kind switch
        {
            ParseErrorKind.Unexpected or ParseErrorKind.Message when label.IsNoneOr(string.IsNullOrEmpty) =>
                new ArgumentNullException(nameof(label), $"Label is required for {nameof(ParseErrorKind)}.{kind}"),

            ParseErrorKind.Expected when expectations.IsNoneOr(ex => ex.Count == 0) =>
                new ArgumentException($"At least one expectation is required for {nameof(ParseErrorKind)}.{kind}", nameof(expectations)),

            _ => None
        };

        this.label = label;
        this.kind = exception.Match(none: kind, some: ex => throw ex);
        this.expectations = expectations.IfNone(Expectation.None);
    }

    public ParseErrorKind Kind => kind;

    public string Label 
    { 
        get => label.IfNone(string.Empty); 
        init => label = Some(value).Except(string.IsNullOrEmpty);
    }

    public Expectation Expectations 
    { 
        get => expectations; 
        init => expectations = value; 
    }

    public string Message => kind switch
        {
            ParseErrorKind.Message                                => Label,
            ParseErrorKind.Unexpected when Expectations.Count > 0 => $"Unexpected {Label}. Expected: {Expectations}",
            ParseErrorKind.Expected   when label.IsSome           => $"Unexpected {Label}. Expected: {Expectations}",
            ParseErrorKind.Unexpected                             => $"Unexpected {Label}",
            ParseErrorKind.Expected                               => $"Expected: {Expectations}",
            ParseErrorKind.Empty                                  => kind.ToString(),
            ParseErrorKind.Unknown                                => "An unknown error has occurred",

            _ => throw new UnreachableException($"A case for {nameof(ParseErrorKind)}.{kind} was unhanlded. Label: {label}, {Expectations}")
        };

    public static ParseError FromMessage(string message) => new(ParseErrorKind.Message, message);

    public static ParseError Unexpected<TToken>(TToken token) => Unexpected(token, ImmutableArray<string>.Empty);
    public static ParseError Unexpected<TToken>(TToken token, IEnumerable<TToken> expected) => Unexpected(token, expected.Select(FormatToken));
    public static ParseError Unexpected<TToken>(TToken token, IEnumerable<string> expected) => Unexpected(token, new Expectation([.. expected]));
    public static ParseError Unexpected<TToken>(TToken token, Expectation expected) => new(ParseErrorKind.Unexpected, FormatToken(token), expected);

    public static ParseError Expected(params IEnumerable<string> expected) => Expected(new Expectation([.. expected]));
    public static ParseError Expected(IEnumerable<string> expected, string found) => Expected(new Expectation(expected.ToImmutableArray()), found);
    public static ParseError Expected(Expectation expected) => new(ParseErrorKind.Expected, None, expected);
    public static ParseError Expected(Expectation expected, string found) => new(ParseErrorKind.Expected, found, expected);


    public static implicit operator ParseError(string message) => FromMessage(message);

    public static implicit operator ParseException(ParseError error) => new(error);

    private static string FormatToken<TToken>(TToken token)
    {
        string? formatted = token?.ToString();

        Debug.Assert(formatted != null, "Token should never be null.");

        return $"'{formatted}'";
    }
}
