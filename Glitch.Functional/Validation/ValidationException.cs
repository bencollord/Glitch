using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.Serialization;
using System.Text;

namespace Glitch.Functional.Validation;

public class ValidationException : Exception
{
    public ValidationException() : this([])
    {
    }

    public ValidationException(string? message) : this(message, [])
    {
    }

    public ValidationException(string? message, Exception? innerException) : this(message, [], innerException)
    {
    }

    public ValidationException(IEnumerable<string> issues) : this("Validation failed", issues)
    {
    }

    public ValidationException(string? message, IEnumerable<string> issues) : this(message, issues, null)
    {
    }

    public ValidationException(string? message, IEnumerable<string> issues, Exception? innerException) : base(message, innerException)
    {
        Issues = issues.ToImmutableList();
    }

    public IImmutableList<string> Issues { get; }

    public override string Message => string.Join(Environment.NewLine, [base.Message, ..Issues]);
}
