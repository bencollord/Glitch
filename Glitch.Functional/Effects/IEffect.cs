using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects
{
    public interface IEffect<TInput, T>
    {
        T Run(TInput input);

        // ====================================================================
        // The following methods -should- be extension methods to match the
        // the rest of the module, but the requirement to specify -every-
        // generic parameter if one needs to be specified makes it syntactically
        // unwieldy. Maybe I'll do all the methods like this later.
        // ====================================================================

        virtual IEffect<TNewInput, T> With<TNewInput>(Func<TNewInput, TInput> map)
            => new WithEffect<TNewInput, TInput, T>(this, map);

        virtual IEffect<TInput, TResult> Cast<TResult>() => this.Select(DynamicCast<TResult>.From);
    }
}