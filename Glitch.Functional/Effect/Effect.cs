
namespace Glitch.Functional
{
    public partial class Effect<T>
    {
        private Effect<Nothing, T> inner;

        internal Effect(Effect<Nothing, T> inner)
        {
            this.inner = inner;
        }

        public static Effect<T> Okay(T value) => new(Effect<Nothing, T>.Okay(value));

        public static Effect<T> Fail(Error error) => new(Effect<Nothing, T>.Fail(error));

        public static Effect<T> FromResult(IResult<T, Error> result) => new(Effect<Nothing, T>.FromResult(result));

        public static Effect<T> Lift(Func<IResult<T, Error>> function) => new(Effect<Nothing, T>.Lift(_ => function()));

        public static Effect<T> Lift(Func<T> function) => new(Effect<Nothing, T>.Lift(_ => function()));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Map{TResult}(Func{T, TResult})"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Effect<TResult> Map<TResult>(Func<T, TResult> map)
            => new(inner.Map(map));

        public Effect<Func<T2, TResult>> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
            => new(inner.PartialMap(map));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.MapError(Func{Error, Error})"/>.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public Effect<T> MapError(Func<Error, Error> map)
            => new(inner.MapError(map));

        public Effect<T> MapError<TError>(Func<TError, Error> map)
            where TError : Error
            => new(inner.MapError(map));

        public Effect<T> Catch<TException>(Func<TException, T> map)
            where TException : Exception
            => new(inner.Catch(map));

        public Effect<T> Catch<TException>(Func<TException, Error> map)
            where TException : Exception
            => new(inner.Catch(map));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Apply{TResult}(Effect{Unit, Func{T, TResult}})"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public Effect<TResult> Apply<TResult>(Effect<Func<T, TResult>> function)
            => new(inner.Apply(function.inner));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Then{TResult}(Effect{Unit, TResult})"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<TResult> Then<TResult>(Effect<TResult> other)
            => new(inner.Then(other.inner));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Then{TOther, TResult}(Effect{Unit, TOther}, Func{T, TOther, TResult})"/>
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Effect<TResult> Then<TOther, TResult>(Effect<TOther> other, Func<T, TOther, TResult> project)
            => new(inner.Then(other.inner, project));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.AndThen{TResult}(Func{T, Effect{Unit, TResult}})"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Effect<TResult> AndThen<TResult>(Func<T, Effect<TResult>> bind)
            => new(inner.AndThen(v => bind(v).inner));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.AndThen{TElement, TResult}(Func{T, Effect{TElement}}, Func{T, TElement, TResult})"/>
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Effect<TResult> AndThen<TElement, TResult>(Func<T, Effect<TElement>> bind, Func<T, TElement, TResult> project)
            => new(inner.AndThen(v => bind(v).inner, project));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.AndThen{TResult}(Func{T, Result{TResult}})"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Effect<TResult> AndThen<TResult>(Func<T, Result<TResult>> bind)
            => AndThen(e => Effect<TResult>.FromResult(bind(e)));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.AndThen{TElement, TResult}(Func{T, Result{TElement}}, Func{T, TElement, TResult})"/>
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Effect<TResult> AndThen<TElement, TResult>(Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public Effect<TResult> Choose<TResult>(Func<T, Effect<TResult>> okay, Func<Error, Effect<TResult>> error)
            => new(inner.Choose(okay: v => okay(v).inner, error: e => error(e).inner));

        public Effect<T> Guard(Func<T, bool> predicate, Error error)
            => new(inner.Guard(predicate, error));

        public Effect<T> Guard(Func<T, bool> predicate, Func<T, Error> error)
            => new(inner.Guard(predicate, error));

        public Effect<T> Guard(bool condition, Error error)
            => new(inner.Guard(condition, error));

        public Effect<T> Guard(bool condition, Func<T, Error> error)
            => new(inner.Guard(condition, error));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Or(Effect{Unit, T})"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<T> Or(Effect<T> other)
            => new(inner.Or(other.inner));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.OrElse(Func{Error, Effect{Unit, T}})"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<T> OrElse(Func<Error, Effect<T>> other)
            => new(inner.OrElse(err => other(err).inner));

        public Effect<T> Filter(Func<T, bool> predicate)
            => new(inner.Filter(predicate));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Do(Action{T})"/>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<T> Do(Action<T> action) => new(inner.Do(action));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.IfOkay(Action{T})"/>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<T> IfOkay(Action<T> action) => new(inner.IfOkay(action));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.IfFail(Action)"/>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<T> IfFail(Action action) => new(inner.IfFail(action));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.IfFail(Action{Error})"/>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<T> IfFail(Action<Error> action) => new(inner.IfFail(action));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Match{TResult}(Func{T, TResult}, TResult)"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Effect<TResult> Match<TResult>(Func<T, TResult> okay, TResult error)
            => new(inner.Match(okay, error));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Match{TResult}(Func{T, TResult}, Func{TResult})"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Effect<TResult> Match<TResult>(Func<T, TResult> okay, Func<TResult> error)
            => new(inner.Match(okay, error));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Match{TResult}(Func{T, TResult}, Func{Error, TResult})"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<T, TResult> okay, Func<Error, TResult> error)
            => Run().Match(okay, error);

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Cast{TResult}"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Effect<TResult> Cast<TResult>() => new(inner.Cast<TResult>());

        public Effect<TResult> CastOr<TResult>(Error error) => new(inner.CastOr<TResult>(error));

        public Effect<TResult> CastOrElse<TResult>(Func<T, Error> error) => new(inner.CastOrElse<TResult>(error));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Zip{TOther}(Effect{Unit, TOther})"/>
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<(T, TOther)> Zip<TOther>(Effect<TOther> other)
            => new(inner.Zip(other.inner));

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Zip{TOther, TResult}(Effect{Unit, TOther}, Func{T, TOther, TResult})"/>
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <param name="zipper"></param>
        /// <returns></returns>
        public Effect<TResult> Zip<TOther, TResult>(Effect<TOther> other, Func<T, TOther, TResult> zipper)
            => new(inner.Zip(other.inner, zipper));

        public Effect<TInput, T> WithInput<TInput>() => inner.With<TInput>(input => Nothing.Value);

        /// <summary>
        /// <inheritdoc cref="Effect{Unit, T}.Run(Unit)"/>
        /// </summary>
        /// <returns></returns>
        public Result<T> Run() => inner.Run(Nothing.Value);

        public static implicit operator Effect<T>(Result<T> result) => FromResult(result);

        public static implicit operator Effect<T>(T value) => Okay(value);

        public static implicit operator Effect<T>(Error error) => Fail(error);

        public static implicit operator Effect<T>(Effect<Nothing, T> effect) => new(effect);

        public static Effect<T> operator |(Effect<T> x, Effect<T> y) => x.Or(y);

        public static Effect<T> operator |(Effect<T> x, Result<T> y) => x.Or(y);

        public static Effect<T> operator |(Effect<T> x, Error y) => x.Or(y);

        public static Effect<T> operator >>(Effect<T> x, Effect<T> y) => x.Then(y);

        public static Effect<T> operator >>(Effect<T> x, Effect<Nothing> y) => x.Then(y, (v, _) => v);

        public static Effect<T> operator >>(Effect<T> x, Func<Result<T>> y) => x.AndThen(_ => y());

        public static Effect<T> operator >>(Effect<T> x, Func<T> y) => x.Map(_ => y());
    }
}
