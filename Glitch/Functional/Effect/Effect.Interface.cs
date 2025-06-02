using System.Collections.Immutable;

namespace Glitch.Functional
{
    public partial class Effect<TInput, TOutput> : IEffect<TInput, TOutput>
    {
        IEffect<TInput, TResult> IEffect<TInput, TOutput>.And<TResult>(IEffect<TInput, TResult> other)
        {
            throw new NotImplementedException();
        }

        IEffect<TInput, TResult> IEffect<TInput, TOutput>.AndThen<TResult>(Func<TOutput, IEffect<TInput, TResult>> bind)
        {
            throw new NotImplementedException();
        }

        IEffect<TInput, TResult> IEffect<TInput, TOutput>.Cast<TResult>()
        {
            throw new NotImplementedException();
        }

        IEffect<TInput, TOutput> IEffect<TInput, TOutput>.Catch<TException>(Func<TException, Error> map)
        {
            throw new NotImplementedException();
        }

        IEffect<TInput, TOutput> IEffect<TInput, TOutput>.Catch<TException>(Func<TException, TOutput> map)
        {
            throw new NotImplementedException();
        }

        IEffect<TInput, TOutput> IEffect<TInput, TOutput>.Guard(Func<TOutput, bool> predicate, Func<TOutput, Error> error)
        {
            return Guard(predicate, error);
        }

        IEffect<TInput, TResult> IEffect<TInput, TOutput>.Map<TResult>(Func<TOutput, TResult> map)
        {
            throw new NotImplementedException();
        }

        IEffect<TInput, TOutput> IEffect<TInput, TOutput>.MapError(Func<Error, Error> map)
        {
            return MapError(map);
        }

        IEffect<TInput, TOutput> IEffect<TInput, TOutput>.MapError<TError>(Func<TError, Error> map)
        {
            throw new NotImplementedException();
        }

        IEffect<TInput, TOutput> IEffect<TInput, TOutput>.Or(IEffect<TInput, TOutput> other)
        {
            throw new NotImplementedException();
        }

        IEffect<TInput, TOutput> IEffect<TInput, TOutput>.OrElse(Func<Error, IEffect<TInput, TOutput>> other)
        {
            throw new NotImplementedException();
        }

        IResult<TOutput, Error> IEffect<TInput, TOutput>.Run(TInput input)
        {
            return Run(input);
        }
    }
}
