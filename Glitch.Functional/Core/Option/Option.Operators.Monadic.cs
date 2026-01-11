namespace Glitch.Functional;

public static partial class OptionExtensions
{
    // Bind
    // ========================================================================
    extension<T>(Option<T> self)
    {
        /// <summary>
        /// Monadic bind operator for unit returning functions.
        /// Returns left operand since right is uninteresting.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="bind"></param>
        /// <returns></returns>
        public static Option<T> operator >>>(Option<T> x, Func<T, Option<Unit>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<T, TResult>(Option<T> self)
    {
        /// <summary>
        /// Monadic bind operator
        /// </summary>
        /// <param name="x"></param>
        /// <param name="bind"></param>
        /// <returns></returns>
        public static Option<TResult> operator >>>(Option<T> x, Func<T, Option<TResult>> bind) => x.AndThen(bind);
    }

    // Map
    // ========================================================================
    extension<T, TResult>(Option<T> self)
    {
        /// <summary>
        /// Functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Option<TResult> operator *(Option<T> x, Func<T, TResult> map) => x.Select(map);

        /// <summary>
        /// Functor map operator.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Option<TResult> operator *(Func<T, TResult> map, Option<T> x) => x.Select(map);
    }

    extension<T1, T2, TResult>(Option<T1> self)
    {
        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Option<Func<T2, TResult>> operator *(Option<T1> x, Func<T1, T2, TResult> map) => x * map.Curry();

        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Option<Func<T2, TResult>> operator *(Func<T1, T2, TResult> map, Option<T1> x) => x * map.Curry();
    }

    extension<T1, T2, T3, TResult>(Option<T1> self)
    {
        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, TResult>>> operator *(Option<T1> x, Func<T1, T2, T3, TResult> map) => x * map.Curry();

        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, TResult>>> operator *(Func<T1, T2, T3, TResult> map, Option<T1> x) => x * map.Curry();
    }

    extension<T1, T2, T3, T4, TResult>(Option<T1> self)
    {
        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Option<T1> x, Func<T1, T2, T3, T4, TResult> map) => x * map.Curry();

        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Func<T1, T2, T3, T4, TResult> map, Option<T1> x) => x * map.Curry();
    }

    extension<T1, T2, T3, T4, T5, TResult>(Option<T1> self)
    {
        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Option<T1> x, Func<T1, T2, T3, T4, T5, TResult> map) => x * map.Curry();

        /// <summary>
        /// Curried functor map operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Func<T1, T2, T3, T4, T5, TResult> map, Option<T1> x) => x * map.Curry();
    }

    // Apply
    // ========================================================================
    extension<T, TResult>(Option<T> self)
    {
        /// <summary>
        /// Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Option<TResult> operator %(Option<T> x, Option<Func<T, TResult>> apply) => x.Apply(apply);

        /// <summary>
        /// Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Option<TResult> operator %(Option<Func<T, TResult>> apply, Option<T> x) => x.Apply(apply);
    }

    extension<T1, T2, TResult>(Option<T1> self)
    {
        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Option<Func<T2, TResult>> operator %(Option<T1> x, Option<Func<T1, T2, TResult>> apply) => x.Apply(apply * Curry);

        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Option<Func<T2, TResult>> operator %(Option<Func<T1, T2, TResult>> apply, Option<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, TResult>(Option<T1> self)
    {
        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, TResult>>> operator %(Option<T1> x, Option<Func<T1, T2, T3, TResult>> apply) => x.Apply(apply * Curry);

        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, TResult>>> operator %(Option<Func<T1, T2, T3, TResult>> apply, Option<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, TResult>(Option<T1> self)
    {
        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, Func<T4, TResult>>>> operator %(Option<T1> x, Option<Func<T1, T2, T3, T4, TResult>> apply) => x.Apply(apply * Curry);

        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, Func<T4, TResult>>>> operator %(Option<Func<T1, T2, T3, T4, TResult>> apply, Option<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, T5, TResult>(Option<T1> self)
    {
        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator %(Option<T1> x, Option<Func<T1, T2, T3, T4, T5, TResult>> apply) => x.Apply(apply * Curry);

        /// <summary>
        /// Curried Applicative apply operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        public static Option<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator %(Option<Func<T1, T2, T3, T4, T5, TResult>> apply, Option<T1> x) => x.Apply(apply * Curry);
    }
}