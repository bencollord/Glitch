using Glitch.Functional.Core;

namespace Glitch.Functional.Optics
{
    public static partial class Prism
    {
        /// <summary>
        /// <inheritdoc cref="Prism{TFocus, TValue}.New(Func{TFocus, Option{TValue}}, Func{TFocus, TValue, TFocus})"/>
        /// </summary>
        /// <typeparam name="TFocus"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="get"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static Prism<TFocus, TValue> New<TFocus, TValue>(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TFocus> set) 
            => Prism<TFocus, TValue>.New(get, set);

        /// <summary>
        /// <inheritdoc cref="Prism{TFocus, TValue}.New(Func{TFocus, Option{TValue}}, Func{TFocus, TValue, TValue, TFocus})"/>
        /// </summary>
        /// <typeparam name="TFocus"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="get"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static Prism<TFocus, TValue> New<TFocus, TValue>(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TValue, TFocus> set)
            => Prism<TFocus, TValue>.New(get, set);
    }

    public static class Prism<TFocus>
    {
        /// <summary>
        /// <inheritdoc cref="Prism{TFocus, TValue}.New(Func{TFocus, Option{TValue}}, Func{TFocus, TValue, TFocus})"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="get"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static Prism<TFocus, TValue> New<TValue>(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TFocus> set) 
            => Prism<TFocus, TValue>.New(get, set);

        /// <summary>
        /// <inheritdoc cref="Prism{TFocus, TValue}.New(Func{TFocus, Option{TValue}}, Func{TFocus, TValue, TValue, TFocus})"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="get"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static Prism<TFocus, TValue> New<TValue>(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TValue, TFocus> set)
            => Prism<TFocus, TValue>.New(get, set);
    }

    public record Prism<TFocus, TValue>
    {
        private readonly Func<TFocus, Option<TValue>> getter;
        private readonly Func<TFocus, TValue, TFocus> setter;

        public Prism(Func<TFocus, Option<TValue>> getter, Func<TFocus, TValue, TFocus> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public Prism(Func<TFocus, Option<TValue>> getter, Func<TFocus, TValue, TValue, TFocus> setter)
        {
            this.getter = getter;
            this.setter = (focus, value) => getter(focus)
                .Select(old => setter(focus, old, value))
                .IfNone(focus);
        }

        /// <summary>
        /// Creates a new prism from the provided get and set functions.
        /// </summary>
        /// <param name="get"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static Prism<TFocus, TValue> New(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TFocus> set) => new(get, set);

        /// <summary>
        /// Creates a new prism from the provided get and set functions.
        /// The set function takes both the old value and the new value as arguments,
        /// thus allowing things such as replacing an item in an immutable collection.
        /// </summary>
        /// <param name="get"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static Prism<TFocus, TValue> New(Func<TFocus, Option<TValue>> get, Func<TFocus, TValue, TValue, TFocus> set)
            => new(get, set);

        public Option<TValue> Get(TFocus focus) => getter(focus);

        public TFocus Set(TFocus focus, TValue value) => setter(focus, value);

        public Func<TValue, TFocus> Set(TFocus focus) => v => Set(focus, v);

        public Func<TFocus, TFocus> Set(TValue value) => f => Set(f, value);

        public Func<TFocus, TFocus> Update(Func<TValue, TValue> update)
            => focus => Update(focus, update);

        public TFocus Update(TFocus focus, Func<TValue, TValue> update)
            => Get(focus).Select(old => Set(focus, update(old))).IfNone(focus);

        public Prism<TFocus, TDeeper> Compose<TDeeper>(Func<TValue, TDeeper> get, Func<TValue, TDeeper, TValue> set)
            => Compose(get.Then(Option.Some), set);

        public Prism<TFocus, TDeeper> Compose<TDeeper>(Func<TValue, Option<TDeeper>> get, Func<TValue, TDeeper, TValue> set)
            => Compose(new Prism<TValue, TDeeper>(get, set));

        public Prism<TFocus, TDeeper> Compose<TDeeper>(Prism<TValue, TDeeper> next)
            => new(focus => Get(focus).AndThen(next.Get),
                  (focus, deeper) => Update(focus, next.Set(deeper)));

        public static implicit operator Prism<TFocus, TValue>(Lens<TFocus, TValue> lens)
            => new(f => Option.Some(lens.Get(f)), lens.Set);

        public static implicit operator Prism<TFocus, TValue>(Lens<TFocus, Option<TValue>> lens)
            => new(lens.Get, (f, v) => lens.Set(f, v));
    }
}
