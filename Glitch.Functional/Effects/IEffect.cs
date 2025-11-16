using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects
{
    public interface IEffect<TInput, TOutput>
    {
        TOutput Run(TInput input);
    }
}