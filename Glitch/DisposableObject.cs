using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public abstract class DisposableObject : IDisposable
    {
        private bool disposed = false; // To detect redundant calls

        protected bool Disposed => disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed) return;

            if (disposing)
            {
                ReleaseManagedResources();
            }

            disposed = true;
        }

        protected abstract void ReleaseManagedResources();
    }
}
