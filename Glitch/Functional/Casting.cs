namespace Glitch.Functional
{
    public static class StaticCast<T>
    {
        public static T From<TDerived>(TDerived obj)
            where TDerived : T => obj;
    }

    public static class DynamicCast<T>
    {
        public static T From<TOther>(TOther obj)
            => (T)(dynamic)obj!;

        public static Fallible<T> TryFrom<TOther>(TOther obj)
            => Try(() => From(obj));

        public static Fallible<T> TryFrom<TOther>(TOther obj, Error ifFail)
            => TryFrom(obj).MapError(_ => ifFail);

        public static Fallible<T> TryFrom<TOther>(TOther obj, Func<Error> ifFail)
            => TryFrom(obj).MapError(_ => ifFail());

        public static Fallible<T> TryFrom<TOther>(TOther obj, Func<TOther, Error> ifFail)
            => TryFrom(obj).MapError(_ => ifFail(obj));
    }
}
