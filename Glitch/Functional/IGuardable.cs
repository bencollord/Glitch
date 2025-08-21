namespace Glitch.Functional
{
    public interface IGuardable<TSelf, T, TError>
        where TSelf : IGuardable<TSelf, T, TError>
    {
        virtual TSelf Guard(bool condition, TError error) => Guard(_ => condition, _ => error);
        virtual TSelf Guard(bool condition, Func<T, TError> error) => Guard(_ => condition, error);
        virtual TSelf Guard(Func<T, bool> predicate, TError error) => Guard(predicate, _ => error);
        TSelf Guard(Func<T, bool> predicate, Func<T, TError> error);
    }
}