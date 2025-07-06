using System.Collections.Concurrent;
using System.Diagnostics;

namespace Glitch.Functional
{
    public static partial class FN
    {
        public static Func<T> Memo<T>(this Func<T> func)
        {
            bool isValueCreated = false;
            T? value = default;
            var lockTarget = new object();

            return () =>
            {
                if (isValueCreated)
                {
                    return value!;
                }
                
                lock (lockTarget)
                {
                    // Run check again in case another thread modified the value before we 
                    // acquired the lock.
                    if (isValueCreated)
                    {
                        return value!;
                    }
                    else
                    {
                        value = func();
                        isValueCreated = true;
                        return value;
                    }
                }
            };
        }

        public static Func<T, TResult> Memo<T, TResult>(this Func<T, TResult> func)
            where T : notnull
        {
            var cache = new ConcurrentDictionary<T, WeakReference<MemoEntry<TResult>>>();
            var lockTargets = new ConcurrentDictionary<T, object>();

            return arg =>
            {
                if (cache.TryGetValue(arg, out var weakRef) && weakRef.TryGetTarget(out var entry))
                {
                    return entry.Value;
                }
                else
                {
                    var lockTarget = lockTargets.GetOrAdd(arg, new object());

                    TResult value;

                    lock (lockTarget)
                    {
                        var wrappedFunc = func.Then(x => new MemoEntry<TResult>(x, () => cache.TryRemove(arg, out _)))
                                              .Then(e => new WeakReference<MemoEntry<TResult>>(e));

                        weakRef = cache.GetOrAdd(arg, wrappedFunc);

                        if (weakRef.TryGetTarget(out entry))
                        {
                            value = entry.Value;
                        }
                        else
                        {
                            weakRef = wrappedFunc(arg);
                            cache.AddOrUpdate(arg, weakRef, (_, _) => weakRef);

                            if (weakRef.TryGetTarget(out entry))
                            {
                                value = entry.Value;
                            }
                            else
                            {
                                Debug.Fail("Failed to get target of weak reference we literally just created");
                                Console.Error.WriteLine("Memoization failed");
                                return func(arg); // Bypass memoization to avoid breaking client code.
                            }
                        }
                    }

                    // Stole this part from the LanguageExt library. I don't actually know 
                    // why we need to remove the lock right after we created it, but it's
                    // probably to prevent memory leaks somehow. 
                    // My best guess is that we only need these targets for the immediate moment we're
                    // locking the specific memoized value in case another thread accesses it during
                    // this logic, but I'll need to do more experiments with multithreading to figure
                    // out exactly what's driving this. 
                    lockTargets.TryRemove(arg, out lockTarget);

                    return value;
                }
            };
        }

        private class MemoEntry<T>
        {
            internal readonly T Value;

            private Action cleanUp;

            internal MemoEntry(T value, Action cleanUp)
            {
                Value = value;
                this.cleanUp = cleanUp;
            }

            ~MemoEntry() 
            {
                cleanUp();
            }
        }
    }
}
