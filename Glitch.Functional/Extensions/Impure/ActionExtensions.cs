namespace Glitch.Functional.Extensions.Impure;

public static class ActionExtensions
{
    extension<T>(T self)
    {
        public T PipeInto(Action<T> action)
        {
            action(self);
            return self;
        }

        public static T operator >>(T obj, Action<T> action) => obj.PipeInto(action);
    }

    extension<T>(Action<T>)
    {
        public static T operator <<(Action<T> action, T obj) => obj.PipeInto(action);
    }
}
