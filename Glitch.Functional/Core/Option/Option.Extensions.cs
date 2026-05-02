using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional;

public static partial class OptionExtensions
{
    extension<T>(Option<T> source)
    {
        public bool IsSome([MaybeNullWhen(false)] out T value)
        {
            value = source.UnwrapOrDefault();
            return source.IsSome;
        }
    }

    extension<T>(Option<T> source) where T : struct
    {
        public T? AsNullable() => source.Match(v => v, () => new T?());
    }

    extension<T, TResult>(Option<Func<T, TResult>> function)
    {
        public Option<TResult> Apply(Option<T> value) => value.Apply(function);
    }

    extension<T>(IEnumerable<Option<T>> source)
    {
        /// <summary>
        /// Returns a the unwrapped values of all the non-empty options.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public IEnumerable<T> Somes() => source.Where(o => o.IsSome).Select(o => o.Unwrap());
    }

    extension(Option<bool> source)
    {
        public Option<T> Select<T>(Func<Unit, T> ifTrue, Func<Unit, T> ifFalse)
            => source.Select(flag => flag ? ifTrue(default) : ifFalse(default));

        public Unit Match(Action ifTrue, Action ifFalse, Action ifNone)
            => source.Match(flag => flag ? ifTrue.Return()() : ifFalse.Return()(), ifNone.Return());

        /// <summary>
        /// Allows three valued logic to be applied to an optional boolean.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="booleanOption"></param>
        /// <param name="ifTrue"></param>
        /// <param name="ifFalse"></param>
        /// <param name="ifNone"></param>
        /// <returns></returns>
        public T Match<T>(Func<T> ifTrue, Func<T> ifFalse, Func<T> ifNone)
            => source.Match(v => v ? ifTrue() : ifFalse(), ifNone);
    }

    extension<E>(Option<E> opt)
    {
        /// <summary>
        /// Wraps the error in a <see cref="Expected{T}" /> if it exists,
        /// otherwise returns an okay <see cref="Expected{T}" /> containing 
        /// the provided value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Result<T, E> FailOr<T>(T value)
            => opt.Match(Result.Fail<T, E>, () => Result.Okay<T, E>(value));

        /// <summary>
        /// Wraps the error in a failed <see cref="Expected{T}" /> if it exists,
        /// otherwise returns an okay <see cref="Expected{T}" /> containing 
        /// the result of the provided function.
        /// </summary>
        /// <param name="function"></param>
        public Result<T, E> FailOrElse<T>(Func<Unit, T> function)
            => opt.Match(Result.Fail<T, E>, function.Then(Result.Okay<T, E>));
    }

    extension<T1, T2>(Option<(T1, T2)> option)
    {
        /// <summary>
        /// Unzips an option of a tuple into a tuple of two options.
        /// </summary>
        /// <remarks>
        /// This method is intended to be used inline with tuple deconstruction.
        /// </remarks>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public (Option<T1>, Option<T2>) Unzip()
            => (option.Select(o => o.Item1), option.Select(o => o.Item2));
    }

    extension<T>(Option<Option<T>> nested)
    {
        /// <summary>
        /// Flattens a nested option to a single level.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nested"></param>
        /// <returns></returns>
        public Option<T> Flatten() => nested.AndThen(o => o);
    }

    extension<T, E>(Option<Result<T, E>> nested)
    {
        public Result<Option<T>, E> Invert() =>
            nested.Match(
                some: res => res * Option.Some,
                none: Result.Okay(Option<T>.None));
    }
}