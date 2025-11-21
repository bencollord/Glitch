using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects;

[Monad]
public partial class Effect<T>
{
    private Effect<Unit, T> inner;

    internal Effect(Effect<Unit, T> inner)
    {
        this.inner = inner;
    }

    public static Effect<T> Return(T value) => new(Effect<Unit, T>.Return(value));

    public static Effect<T> Fail(Error error) => new(Effect<Unit, T>.Fail(error));

    public static Effect<T> Return(Result<T, Error> result) => new(Effect<Unit, T>.Return(result));

    public static Effect<T> Lift(Func<Expected<T>> function) => new(Effect<Unit, T>.Lift(_ => function()));

    public static Effect<T> Lift(Func<Result<T, Error>> function) => new(Effect<Unit, T>.Lift(_ => function()));

    public static Effect<T> Lift(Func<T> function) => new(Effect<Unit, T>.Lift(_ => function()));

    /// <summary>
    /// <inheritdoc cref="Effect{Unit, T}.Select{TResult}(Func{T, TResult})"/>.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="map"></param>
    /// <returns></returns>
    public Effect<TResult> Select<TResult>(Func<T, TResult> map)
        => new(inner.Select(map));

    public Effect<Func<T2, TResult>> PartialSelect<T2, TResult>(Func<T, T2, TResult> map)
        => new(inner.PartialSelect(map));

    /// <summary>
    /// <inheritdoc cref="Effect{Unit, T}.SelectError(Func{Error, Error})"/>.
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    public Effect<T> SelectError(Func<Error, Error> map)
        => new(inner.SelectError(map));

    public Effect<T> SelectError<TError>(Func<TError, Error> map)
        where TError : Error
        => new(inner.SelectError(map));

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
    /// <inheritdoc cref="Effect{Unit, T}.Then(Effect{Unit, Unit})"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public Effect<T> Then(Effect<Unit> other)
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
    /// <inheritdoc cref="Effect{Unit, T}.AndThen{TResult}(Func{T, Expected{TResult}})"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <returns></returns>
    public Effect<TResult> AndThen<TResult>(Func<T, Expected<TResult>> bind)
        => AndThen(e => Effect<TResult>.Return(bind(e)));

    /// <summary>
    /// <inheritdoc cref="Effect{Unit, T}.AndThen{TElement, TResult}(Func{T, Expected{TElement}}, Func{T, TElement, TResult})"/>
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <param name="project"></param>
    /// <returns></returns>
    public Effect<TResult> AndThen<TElement, TResult>(Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(x => bind(x).Select(y => project(x, y)));

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
        => new(inner.Where(predicate));

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

    public Effect<TInput, T> With<TInput>() => inner.With<TInput>(input => Unit.Value);

    /// <summary>
    /// <inheritdoc cref="Effect{Unit, T}.Run(Unit)"/>
    /// </summary>
    /// <returns></returns>
    public Expected<T> Run() => inner.Run(Unit.Value);
}
