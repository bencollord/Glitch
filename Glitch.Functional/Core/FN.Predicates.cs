namespace Glitch.Functional.Core
{
    public static partial class FN
    {
        public static Func<T, bool> True<T>() => _ => true;

        public static Func<T, bool> False<T>() => _ => false;

        public static Func<T, bool> And<T>(Func<T, bool> f, Func<T, bool> g) => x => f(x) && g(x);
        
        public static Func<T, bool> Or<T>(Func<T, bool> f, Func<T, bool> g) => x => f(x) || g(x);
        
        public static Func<T, bool> Not<T>(Func<T, bool> f) => x => !f(x);
    }

    public static partial class FuncExtensions
    {
        public static Func<T, bool> And<T>(this Func<T, bool> self, Func<T, bool> other) => t => self(t) && other(t);

        public static Func<T, bool> AndNot<T>(this Func<T, bool> self, Func<T, bool> other) => t => self(t) && !other(t);

        public static Func<T, bool> Or<T>(this Func<T, bool> self, Func<T, bool> other) => t => self(t) || other(t);

        public static Func<T, bool> OrNot<T>(this Func<T, bool> self, Func<T, bool> other) => t => self(t) && !other(t);

        public static Func<T, bool> Not<T>(this Func<T, bool> self) => t => !self(t);

        extension<T>(Func<T, bool> _)
        {
            public static Func<T, bool> operator &(Func<T, bool> lhs, Func<T, bool> rhs) => t => lhs(t) && rhs(t);

            public static Func<T, bool> operator |(Func<T, bool> lhs, Func<T, bool> rhs) => t => lhs(t) || rhs(t);
            
            public static Func<T, bool> operator ^(Func<T, bool> lhs, Func<T, bool> rhs) => t => (lhs(t) && !rhs(t)) || (!lhs(t) && rhs(t));
        }
    }
}
