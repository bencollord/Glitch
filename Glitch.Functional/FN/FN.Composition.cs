namespace Glitch.Functional
{
    public static partial class FN
    {
        public static Func<T, bool> True<T>() => _ => true;

        public static Func<T, bool> False<T>() => _ => false;

        public static Func<T, bool> And<T>(this Func<T, bool> self, Func<T, bool> other) => t => self(t) && other(t);
        
        public static Func<T, bool> AndNot<T>(this Func<T, bool> self, Func<T, bool> other) => self.And(Not(other));
        
        public static Func<T, bool> Or<T>(this Func<T, bool> self, Func<T, bool> other) => t => self(t) || other(t);
        
        public static Func<T, bool> OrNot<T>(this Func<T, bool> self, Func<T, bool> other) => self.Or(Not(other));
        
        public static Func<T, bool> Not<T>(this Func<T, bool> self) => t => !self(t);

        public static Func<TResult> Then<T, TResult>(this Func<T> f, Func<T, TResult> g)
            => () => g(f());

        public static Func<T1, T3> Then<T1, T2, T3>(this Func<T1, T2> f, Func<T2, T3> g)
            => x => g(f(x));

        public static Func<TResult> Before<T, TResult>(this Func<T, TResult> f, Func<T> g)
            => () => f(g());

        public static Func<T1, T3> Before<T1, T2, T3>(this Func<T2, T3> f, Func<T1, T2> g)
            => x => f(g(x));
    }
}
