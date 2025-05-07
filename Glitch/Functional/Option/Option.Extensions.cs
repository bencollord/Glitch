using System.Collections.Immutable;

namespace Glitch.Functional
{
    public static partial class Option
    {
        public static Option<IEnumerable<T>> Traverse<T>(this IEnumerable<Option<T>> source)
            => source.Traverse(Identity);

        public static Option<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Option<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Map(traverse));

        public static Option<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, Option<TResult>> traverse)
            => source.Aggregate(
                Some(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Map(l => l.AsEnumerable()));

        public static Option<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Option<T>> source, Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.PartialMap(traverse).Apply(i))
                     .Traverse();

        public static Option<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, int, Option<TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public static T? AsNullable<T>(this Option<T> option)
            where T : struct
            => option.Match(v => v, () => new T?());

        public static Option<TResult> Invoke<TResult>(this Option<Func<TResult>> function) => function.Map(fn => fn());
        public static Option<TResult> Invoke<T, TResult>(this Option<Func<T, TResult>> function, T value) => function.Map(fn => fn(value));
        public static Option<TResult> Invoke<T1, T2, TResult>(this Option<Func<T1, T2, TResult>> function, T1 arg1, T2 arg2) => function.Map(fn => fn(arg1, arg2));
        public static Option<TResult> Invoke<T1, T2, T3, TResult>(this Option<Func<T1, T2, T3, TResult>> function, T1 arg1, T2 arg2, T3 arg3) => function.Map(fn => fn(arg1, arg2, arg3));
        public static Option<TResult> Invoke<T1, T2, T3, T4, TResult>(this Option<Func<T1, T2, T3, T4, TResult>> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => function.Map(fn => fn(arg1, arg2, arg3, arg4));
        
        public static Option<Func<T2, TResult>> Invoke<T1, T2, TResult>(this Option<Func<T1, T2, TResult>> function, T1 arg) => function.Map(fn => fn.Curry()(arg));
        public static Option<Func<T2, Func<T3, TResult>>> Invoke<T1, T2, T3, TResult>(this Option<Func<T1, T2, T3, TResult>> function, T1 arg) => function.Map(fn => fn.Curry()(arg));
        public static Option<Func<T2, Func<T3, Func<T4, TResult>>>> Invoke<T1, T2, T3, T4, TResult>(this Option<Func<T1, T2, T3, T4, TResult>> function, T1 arg) => function.Map(fn => fn.Curry()(arg));

        public static Option<TResult> Apply<T, TResult>(this Option<Func<T, TResult>> function, Option<T> value)
            => value.Apply(function);

        /// <summary>
        /// Returns a the unwrapped values of all the non-empty options.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IEnumerable<T> Somes<T>(this IEnumerable<Option<T>> options)
            => options.Where(o => o.IsSome).Select(o => o.Unwrap());

        /// <summary>
        /// Allows three valued logic to be applied to an optional boolean.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="booleanOption"></param>
        /// <param name="ifTrue"></param>
        /// <param name="ifFalse"></param>
        /// <param name="ifNone"></param>
        /// <returns></returns>
        public static T Match<T>(this Option<bool> booleanOption, Func<T> ifTrue, Func<T> ifFalse, Func<T> ifNone)
            => booleanOption.Match(v => v ? ifTrue() : ifFalse(), ifNone);


        public static Terminal Match(this Option<bool> result, Action ifTrue, Action ifFalse, Action ifNone)
            => result.Match(flag => flag ? ifTrue.Return()() : ifFalse.Return()(), ifNone.Return());

        /// <summary>
        /// Wraps the error in a <see cref="Result{T}" /> if it exists,
        /// otherwise returns an okay <see cref="Result{T}" /> containing 
        /// the provided value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<TValue> FailOr<TError, TValue>(this Option<TError> opt, TValue value)
            where TError : Error
            => opt.Match(Fail<TValue>, _ => Okay(value));

        /// <summary>
        /// Wraps the error in a failed <see cref="Result{T}" /> if it exists,
        /// otherwise returns an okay <see cref="Result{T}" /> containing 
        /// the result of the provided function.
        /// </summary>
        /// <param name="function"></param>
        public static Result<TValue> FailOrElse<TError, TValue>(this Option<TError> opt, Func<TValue> function)
            where TError : Error
            => opt.Match(Fail<TValue>, function.Then(Okay));

        /// <summary>
        /// Wraps the error in a failed <see cref="Result{T}" /> if it exists,
        /// otherwise returns an okay <see cref="Result{T}" /> containing 
        /// the result of the provided function.
        /// </summary>
        /// <param name="function"></param>
        public static Result<TValue> FailOrElse<TError, TValue>(this Option<TError> opt, Func<Terminal, TValue> function)
            where TError : Error
            => opt.Match(Fail<TValue>, function.Then(Okay));

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
        public static (Option<T1>, Option<T2>) Unzip<T1, T2>(this Option<(T1, T2)> option)
            => (option.Map(o => o.Item1), option.Map(o => o.Item2));

        /// <summary>
        /// Flattens a nested option to a single level.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nested"></param>
        /// <returns></returns>
        public static Option<T> Flatten<T>(this Option<Option<T>> nested)
            => nested.AndThen(o => o);

        public static Result<Option<T>> Invert<T>(this Option<Result<T>> nested)
            => nested.Match(
                    res => res.Map(Some),
                    () => Okay(Option<T>.None)
                );
    }
}
