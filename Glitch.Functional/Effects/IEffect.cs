using Glitch.Functional.Core;

namespace Glitch.Functional.Effects
{
    /// <summary>
    /// Represents an effect that doesn't take any input.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEffect<T> : IEffect<Unit, T>
    {
    }

    /// <summary>
    /// Represents an effectful computation that functions can be composed over.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="T"></typeparam>
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