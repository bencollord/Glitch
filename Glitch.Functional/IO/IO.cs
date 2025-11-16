using Glitch.Functional.Core;
using Glitch.Functional.Errors;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public abstract partial class IO<T>
    {
        public T Run()
        {
            using var env = IOEnv.New();

            return Run(env);
        }

        public T Run(IOEnv env)
        {
            var task = RunAsync(env);

            if (task.IsCompleted)
            {
                return task.Result;
            }

            return task.GetAwaiter().GetResult();
        }

        public async Task<T> RunAsync()
        {
            using var env = IOEnv.New();

            return await RunAsync(env);
        }

        public async Task<T> RunAsync(IOEnv env)
        {
            if (!env.CancellationToken.IsCancellationRequested)
            {
                return await RunIOAsync(env);
            }

            return await Task.FromCanceled<T>(env.CancellationToken);
        }

        protected abstract Task<T> RunIOAsync(IOEnv env);

        public Expected<T> Try()
        {
            using var env = IOEnv.New();

            return Try(env);
        }

        public Expected<T> Try(IOEnv env)
        {
            try
            {
                return Run(env);
            }
            catch (Exception e)
            {
                return Expected.Fail<T>(e);
            }
        }

        public IO<T> With(Func<IOEnv, IOEnv> mapEnv) => new MapEnvIO<T>(this, mapEnv);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> Select<TResult>(Func<T, TResult> map) => AndThen(x => IO.Return(map(x)));
        public IO<TResult> SelectAsync<TResult>(Func<T, Task<TResult>> map) 
            => AndThenAsync(async x => IO.Return(await map(x)));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> Apply<TResult>(IO<Func<T, TResult>> map) => map.AndThen(fn => Select(fn));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> AndThen<TResult>(Func<T, IO<TResult>> bind) => AndThen(bind, (_, y) => y);
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> AndThen<TElement, TResult>(Func<T, IO<TElement>> bind, Func<T, TElement, TResult> project) => new ContinueIO<T, TElement, TResult>(this, bind, project);

        public IO<TResult> AndThenAsync<TResult>(Func<T, Task<IO<TResult>>> bind) => AndThenAsync(bind, (_, y) => y);
        public IO<TResult> AndThenAsync<TElement, TResult>(Func<T, Task<IO<TElement>>> bind, Func<T, TElement, TResult> project) => new ContinueAsyncIO<T, TElement, TResult>(this, bind, project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> AndThen<TResult>(Func<T, Option<TResult>> bind) => AndThen(bind, (_, y) => y);
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> AndThen<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(x => bind(x).Match(IO.Return, _ => IO.Fail<TElement>(Errors.Errors.NoElements)), project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> AndThen<TResult>(Func<T, Expected<TResult>> bind) => AndThen(bind, (_, y) => y);
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> AndThen<TElement, TResult>(Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(x => bind(x).Match(IO.Return, IO.Fail<TElement>), project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> AndThen<E, TResult>(Func<T, Result<TResult, E>> bind) => AndThen(bind, (_, y) => y);
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> AndThen<E, TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project) => AndThen(x => bind(x).Match(IO.Return, err => IO.Fail<TElement>(Error.From(err))), project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> Then<TResult>(IO<TResult> other) => Then(other, (_, y) => y);
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> Then<TElement, TResult>(IO<TElement> other, Func<T, TElement, TResult> project) => AndThen(FN<T>.Constant(other), project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<T> Or(IO<T> other) => OrElse(_ => other);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<T> OrElse(Func<Error, IO<T>> bind) => Catch(bind);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<T> Where(Func<T, bool> predicate) => Guard(predicate);
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<T> Guard(Func<T, bool> predicate) => Guard(predicate, v => $"Value {v} did not match filter");
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<T> Guard(Func<T, bool> predicate, Func<T, Error> error) => new GuardIO<T>(this, predicate, error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> Match<TResult>(Func<T, TResult> okay, Func<Error, TResult> error)
            => Select(okay).Catch(error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<Unit> Match(Action<T> okay, Action<Error> error)
            => Match(okay.Return(), error.Return());

        public IO<TResource> Use<TResource>(Func<T, TResource> acquire) => IO<TResource>.Use(Select(acquire));

        public IO<Unit> Release() => IO.Release(this);

        public IO<T> Catch<TError>(Func<TError, T> map) => Catch(err => err switch
        {
            TError error => IO.Return(map(error)),
            ExceptionError ex when ex.Exception is TError exception => IO.Return(map(exception)),
            _ => IO.Fail<T>(err)
        });

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<T> Catch(Func<Error, IO<T>> bind) => new CatchIO<T>(this, bind);
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<T> Catch(Func<Error, bool> filter, Func<Error, IO<T>> bind) => new CatchIO<T>(this, bind, filter);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<TResult> Cast<TResult>() => Select(DynamicCast<TResult>.From);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IO<T> Do(Action<T> action)
            => Select(v =>
            {
                action(v);
                return v;
            });

        public IO<T> DoAsync(Func<T, Task> action)
            => SelectAsync(async v =>
            {
                await action(v).ConfigureAwait(false);
                return v;
            });

        // LinqPad
        protected object? ToDump()
        {
            return Run();
        }

        public static implicit operator IO<T>(T value) => Return(value);
        public static implicit operator IO<T>(Error error) => Fail(error);

        public static IO<T> operator |(IO<T> x, T y) => x | Return(y);
        public static IO<T> operator |(IO<T> x, Error y) => x | Fail(y);
        public static IO<T> operator |(IO<T> x, IO<T> y) => x.Or(y);

        public static IO<T> operator >>(IO<T> x, IO<T> y) => x.AndThen(_ => y);
        public static IO<T> operator >>(IO<T> x, IO<Unit> y) => x.AndThen(v => y.Select(_ => v));
    }
}