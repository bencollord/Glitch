using System.Collections.Concurrent;

namespace Glitch.Functional
{
    public record IOEnv : IDisposable
    {
        private readonly ConcurrentDictionary<object, IOResource> resources = new();

        public static IOEnv New() => new();

        public Unit Track(object? resource)
        {
            if (resource is IDisposable d)
            {
                resources.TryAdd(d, new DisposableResource(d));
            }

            return Nothing;
        }

        public Unit Release(object? resource)
        {
            if (resource is not null && resources.TryRemove(resource, out IOResource? d))
            {
                return d.Release();
            }

            return Nothing;
        }

        public Unit ReleaseAll()
        {
            foreach (var (_, resource) in resources)
            {
                resource.Release();
            }

            resources.Clear();

            return Nothing;
        }

        public void Dispose() => ReleaseAll();

        private abstract record IOResource
        {
            internal abstract Unit Release();
        }

        private record DisposableResource(IDisposable Resource) : IOResource
        {
            private bool isDisposed = false;

            internal override Unit Release()
            {
                if (!isDisposed)
                {
                    Resource.Dispose();
                    isDisposed = true;
                }

                return Nothing;
            }
        }
    }
}