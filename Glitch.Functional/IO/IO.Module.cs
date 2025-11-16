using Glitch.Functional.Core;

namespace Glitch.Functional
{
    public static partial class IO
    {
        public static IO<IOEnv> Env() => Lift(env => env);

        public static IO<T> Return<T>() where T : new() => IO<T>.Return(new());
        public static IO<T> Return<T>(T value) => IO<T>.Return(value);
        public static IO<T> Return<T>(Expected<T> result) => IO<T>.Return(result);
        public static IO<T> ReturnAsync<T>(Task<T> value) => IO<T>.ReturnAsync(value);
        public static IO<T> Fail<T>(Error error) => IO<T>.Fail(error);
        public static IO<T> Lift<T>(Func<T> func) => IO<T>.Lift(func);
        public static IO<T> Lift<T>(Func<IOEnv, T> func) => IO<T>.Lift(func);
        public static IO<T> LiftAsync<T>(Func<Task<T>> func) => IO<T>.LiftAsync(func);
        public static IO<T> LiftAsync<T>(Func<IOEnv, Task<T>> func) => IO<T>.LiftAsync(func);

        public static IO<Unit> Return() => Return(Unit.Value);
        public static IO<Unit> Fail(Error error) => Fail<Unit>(error);
        public static IO<Unit> Lift(Action action) => Lift(() => { action(); return Unit.Value; });
        public static IO<Unit> Lift(Action<IOEnv> action) => Lift(env => { action(env); return Unit.Value; });
        public static IO<Unit> LiftAsync(Func<Task> func) => LiftAsync(async _ => await func());
        public static IO<Unit> LiftAsync(Func<IOEnv, Task> func) 
            => IO<Unit>.LiftAsync(async env =>
            {
                await func(env);
                return Nothing;
            });

        public static IO<T> Use<T>(IO<T> resource) => IO<T>.Use(resource);
        public static IO<T> Use<T>(T resource) => IO<T>.Use(resource);
        public static IO<T> Use<T>(Func<T> resource) => IO<T>.Use(resource);
        public static IO<T> Use<T>(Func<IOEnv, T> resource) => IO<T>.Use(resource);

        public static IO<Unit> Release<T>(T resource) => IO<T>.Release(resource);
        public static IO<Unit> Release<T>(IO<T> resource) => IO<T>.Release(resource);
    }

    public abstract partial class IO<T>
    {
        public static IO<T> Return(T value) => ReturnAsync(Task.FromResult(value));
        public static IO<T> Return(Expected<T> result) => result.Match(Return, Fail);
        public static IO<T> ReturnAsync(Task<T> value) => new ReturnAsyncIO<T>(value);

        public static IO<T> Fail(Error error) => new FailIO<T>(error);
        public static IO<T> Lift(Func<T> func) => new LiftIO<T>(_ => func());
        public static IO<T> Lift(Func<IOEnv, T> func) => new LiftIO<T>(func);
        public static IO<T> LiftAsync(Func<Task<T>> func) => LiftAsync(_ => func());
        public static IO<T> LiftAsync(Func<IOEnv, Task<T>> func) => new LiftAsyncIO<T>(func);

        public static IO<T> Use(T resource) => Use(Return(resource));
        public static IO<T> Use(IO<T> resource) => new UsingIO<T>(resource);
        public static IO<T> Use(Func<T> resource) => Use(Lift(resource));
        public static IO<T> Use(Func<IOEnv, T> resource) => Use(Lift(resource));

        public static IO<Unit> Release(T resource) => IO<Unit>.Lift(env => env.Release(resource));
        public static IO<Unit> Release(IO<T> resource) => resource.AndThen(Release);
    }
}