namespace Glitch.Functional;

public static partial class FuncExtensions
{
    extension<T>(Func<T, bool> _)
    {
        public static Func<T, bool> operator !(Func<T, bool> fn) => t => !fn(t);

        public static Func<T, bool> operator &(Func<T, bool> lhs, Func<T, bool> rhs) => t => lhs(t) && rhs(t);

        public static Func<T, bool> operator |(Func<T, bool> lhs, Func<T, bool> rhs) => t => lhs(t) || rhs(t);
        
        public static Func<T, bool> operator ^(Func<T, bool> lhs, Func<T, bool> rhs) => t => (lhs(t) && !rhs(t)) || (!lhs(t) && rhs(t));
    }
}
