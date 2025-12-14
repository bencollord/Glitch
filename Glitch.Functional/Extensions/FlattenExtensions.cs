using Glitch.Functional;
using Glitch.Functional.Collections;
using Glitch.Functional.Errors;
using Glitch.Functional.Parsing.Results;
using System.Diagnostics;

namespace Glitch.Functional;

[DebuggerStepThrough]
public static class FlattenExtensions
{


    public static Option<T> Flatten<T>(this Option<Option<T>> source) => source.AndThen(Identity);

    public static Expected<T> Flatten<T>(this Expected<Expected<T>> source) => source.AndThen(Identity);

}
