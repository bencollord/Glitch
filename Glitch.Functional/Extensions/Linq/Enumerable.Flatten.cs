using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Extensions;

public static partial class LinqExtensions
{
    extension<TSource>(IEnumerable<IEnumerable<TSource>> source)
    {
        public IEnumerable<TSource> Flatten() => source.SelectMany(Identity);
    }
}
