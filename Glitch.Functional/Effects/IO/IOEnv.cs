using Glitch.Functional.Core;
using System.Collections.Concurrent;

namespace Glitch.Functional.Effects
{
    public record IOEnv : IDisposable
    {
        private readonly ConcurrentDictionary<object, IOResource> resources = new();

        public CancellationToken CancellationToken { get; init; }

        public static IOEnv New() => new();
        public static IOEnv New(CancellationToken cancellationToken) => new() { CancellationToken = cancellationToken };

        public Unit Track(object? resource)
        {
            if (resource is IDisposable d)
            {
                if (resources.TryAdd(d, new DisposableResource(d)))
                {
                    OnResourceTracked(resource);
                }
            }

            return Unit.Value;
        }

        public Unit Release(object? resource)
        {
            if (resource is not null && resources.TryRemove(resource, out IOResource? d))
            {
                d.Release();
                OnResourceReleased(resource);
            }

            return Unit.Value;
        }

        public Unit ReleaseAll()
        {
            foreach (var (obj, resource) in resources)
            {
                resource.Release();
                OnResourceReleased(obj);
            }

            resources.Clear();

            return Unit.Value;
        }

        public void Dispose()
        {
            ReleaseAll();
            OnDisposed();
            GC.SuppressFinalize(this);
        }

        protected virtual void OnResourceTracked(object resource) { }
        protected virtual void OnResourceReleased(object resource) { }
        protected virtual void OnDisposed() { }

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

                return Unit.Value;
            }
        }
    }
}