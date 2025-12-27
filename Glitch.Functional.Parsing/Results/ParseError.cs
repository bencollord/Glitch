using Glitch.Functional;
using Glitch.Functional.Errors;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Glitch.Functional.Parsing.Results;

using static Option;

public record ParseError : Error
{
    public static readonly new ParseError Empty = new(ParseErrorKind.Empty);

    public static readonly ParseError Unknown = new(ParseErrorKind.Unknown);

    private Option<string> label;
    private ParseErrorKind kind;
    private ImmutableArray<string> expectations;

    private ParseError(ParseErrorKind kind, Option<string> label = default, ImmutableArray<string>? expectations = null)
    {
        Option<Exception> exception = kind switch
        {
            ParseErrorKind.Unexpected or ParseErrorKind.Message when label.IsNoneOr(string.IsNullOrEmpty) => 
                new ArgumentNullException(nameof(label), $"Label is required for {nameof(ParseErrorKind)}.{kind}"),
            
            ParseErrorKind.Expected when !expectations.HasValue || expectations.Value.Length == 0 => 
                new ArgumentException($"At least one expectation is required for {nameof(ParseErrorKind)}.{kind}", nameof(expectations)),
            
            _ => None
        };

        this.label = label;
        this.kind = exception.Match(none: kind, some: ex => throw ex);
        this.expectations = expectations ?? ImmutableArray<string>.Empty;
    }

    public ParseErrorKind Kind => kind;

    public string Label 
    { 
        get => label.IfNone(string.Empty); 
        init => label = Some(value).Except(string.IsNullOrEmpty);
    }

    public ImmutableArray<string> Expectations 
    { 
        get => expectations; 
        init => expectations = value; 
    }

    public override int Code => (int)kind;

    public override string Message => kind switch
    {
        ParseErrorKind.Message                                 => Label,
        ParseErrorKind.Unexpected when Expectations.Length > 0 => $"Unexpected {Label}. Expected: {FormatExpectations()}",
        ParseErrorKind.Expected   when label.IsSome            => $"Unexpected {Label}. Expected: {FormatExpectations()}",
        ParseErrorKind.Unexpected                              => $"Unexpected {Label}",
        ParseErrorKind.Expected                                => $"Expected: {FormatExpectations()}",
        ParseErrorKind.Empty                                   => kind.ToString(),
        ParseErrorKind.Unknown                                 => "An unknown error has occurred",

        _ => throw new UnreachableException($"A case for {nameof(ParseErrorKind)}.{kind} was unhanlded. Label: {label}, Expectations: {FormatExpectations()}")
    };

    public static new ParseError New(string message) => new(ParseErrorKind.Message, message);

    public static ParseError Unexpected<TToken>(TToken token) => Unexpected(token, ImmutableArray<string>.Empty);
    public static ParseError Unexpected<TToken>(TToken token, IEnumerable<TToken> expected) => Unexpected(token, expected.Select(e => $"'{e}'"));
    public static ParseError Unexpected<TToken>(TToken token, IEnumerable<string> expected) => Unexpected(token, [.. expected]);
    public static ParseError Unexpected<TToken>(TToken token, ImmutableArray<string> expected) => new(ParseErrorKind.Unexpected, $"'{token}'", expected);

    public static ParseError Expected(params IEnumerable<string> expected) => Expected([.. expected]);
    public static ParseError Expected(ImmutableArray<string> expected) => new(ParseErrorKind.Expected, None, expected);
    public static ParseError Expected(ImmutableArray<string> expected, string found) => new(ParseErrorKind.Expected, found, expected);

    public override Exception AsException() => new ParseException(this);

    private string FormatExpectations()
    {
        return Expectations switch
        {
            [var single]             => single,
            [var one, var two]       => $"{one} or {two}",
            [.. var items, var last] => $"{items.Join(", ")}, or {last}",
            []                       => string.Empty
        };
    }
}
