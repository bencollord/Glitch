namespace Glitch.Functional;

public static partial class ResultExtensions
{
    // Bind
    // ========================================================================
    extension<T, E>(Result<T, E> self)
    {
        /// <summary>
        /// Monadic bind operator for unit returning functions.
        /// Returns left operand since right is uninteresting.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="bind"></param>
        /// <returns></returns>
        public static Result<T, E> operator >>>(Result<T, E> x, Func<T, Result<Unit, E>> bind) => x.AndThen(bind, (x, _) => x);

        /// <inheritdoc cref="operator }}}(Result{T, E}, System.Func{T, Result{Unit, E}})"/>
        public static Result<T, E> operator >>>(Result<T, E> x, Func<T, Okay<Unit>> bind) => x.AndThen<Unit, T>(y => bind(y), (x, _) => x);

        /// <inheritdoc cref="operator }}}(Result{T, E}, System.Func{T, Result{T, E}})"/>
        public static Result<T, E> operator >>>(Result<T, E> x, Func<T, Fail<E>> bind) => x.AndThen<T>(y => bind(y));
    }

    extension<T, E, TResult>(Result<T, E> self)
    {
        /// <summary>
        /// Monadic bind operator
        /// </summary>
        /// <param name="x"></param>
        /// <param name="bind"></param>
        /// <returns></returns>
        public static Result<TResult, E> operator >>>(Result<T, E> x, Func<T, Result<TResult, E>> bind) => x.AndThen(bind);

        /// <inheritdoc cref="operator }}}(Result{T, E}, System.Func{T, Result{TResult, E}})"/>
        public static Result<TResult, E> operator >>>(Result<T, E> x, Func<T, Okay<TResult>> bind) => x.AndThen<TResult>(y => bind(y));
    }

    extension<T, E>(Okay<T>)
    {
        /// <inheritdoc cref="operator }}}(Result{T, E}, System.Func{T, Result{Unit, E}})"/>
        public static Result<T, E> operator >>>(Okay<T> x, Func<T, Result<Unit, E>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<T, E, TResult>(Okay<T>)
    {
        /// <inheritdoc cref="operator }}}(Result{T, E}, System.Func{T, Result{TResult, E}})"/>
        public static Result<TResult, E> operator >>>(Okay<T> x, Func<T, Result<TResult, E>> bind) => x.AndThen(bind);
    }

    // Map
    // ========================================================================
    extension<T, E, TResult>(Result<T, E> self)
    {
        /// <summary>
        /// Functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Result<TResult, E> operator *(Result<T, E> x, Func<T, TResult> map) => x.Select(map);

        /// <summary>
        /// Functor map operator.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Result<TResult, E> operator *(Func<T, TResult> map, Result<T, E> x) => x.Select(map);
    }

    extension<T1, T2, E, TResult>(Result<T1, E> self)
    {
        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Result<Func<T2, TResult>, E> operator *(Result<T1, E> x, Func<T1, T2, TResult> map) => x * map.Curry();

        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Result<Func<T2, TResult>, E> operator *(Func<T1, T2, TResult> map, Result<T1, E> x) => x * map.Curry();
    }

    extension<T1, T2, T3, E, TResult>(Result<T1, E> self)
    {
        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, TResult>>, E> operator *(Result<T1, E> x, Func<T1, T2, T3, TResult> map) => x * map.Curry();

        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, TResult>>, E> operator *(Func<T1, T2, T3, TResult> map, Result<T1, E> x) => x * map.Curry();
    }

    extension<T1, T2, T3, T4, E, TResult>(Result<T1, E> self)
    {
        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Result<T1, E> x, Func<T1, T2, T3, T4, TResult> map) => x * map.Curry();

        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Func<T1, T2, T3, T4, TResult> map, Result<T1, E> x) => x * map.Curry();
    }

    extension<T1, T2, T3, T4, T5, E, TResult>(Result<T1, E> self)
    {
        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Result<T1, E> x, Func<T1, T2, T3, T4, T5, TResult> map) => x * map.Curry();

        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Func<T1, T2, T3, T4, T5, TResult> map, Result<T1, E> x) => x * map.Curry();
    }

    // Apply
    // ========================================================================
    extension<T, E, TResult>(Result<T, E> self)
    {
        /// <summary>
        /// Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Result<TResult, E> operator %(Result<T, E> x, Result<Func<T, TResult>, E> apply) => x.Apply(apply);

        /// <summary>
        /// Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Result<TResult, E> operator %(Result<Func<T, TResult>, E> apply, Result<T, E> x) => x.Apply(apply);
    }

    extension<T1, T2, E, TResult>(Result<T1, E> self)
    {
        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Result<Func<T2, TResult>, E> operator %(Result<T1, E> x, Result<Func<T1, T2, TResult>, E> apply) => x.Apply(apply * Curry);

        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Result<Func<T2, TResult>, E> operator %(Result<Func<T1, T2, TResult>, E> apply, Result<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, E, TResult>(Result<T1, E> self)
    {
        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, TResult>>, E> operator %(Result<T1, E> x, Result<Func<T1, T2, T3, TResult>, E> apply) => x.Apply(apply * Curry);

        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, TResult>>, E> operator %(Result<Func<T1, T2, T3, TResult>, E> apply, Result<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, E, TResult>(Result<T1, E> self)
    {
        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator %(Result<T1, E> x, Result<Func<T1, T2, T3, T4, TResult>, E> apply) => x.Apply(apply * Curry);

        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator %(Result<Func<T1, T2, T3, T4, TResult>, E> apply, Result<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, T5, E, TResult>(Result<T1, E> self)
    {
        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator %(Result<T1, E> x, Result<Func<T1, T2, T3, T4, T5, TResult>, E> apply) => x.Apply(apply * Curry);

        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator %(Result<Func<T1, T2, T3, T4, T5, TResult>, E> apply, Result<T1, E> x) => x.Apply(apply * Curry);
    }
}