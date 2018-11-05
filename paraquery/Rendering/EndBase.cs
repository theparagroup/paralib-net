using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{

    /*

       Defines the basics of the IDisposable interface ("End" semantics). It is up to the instatiator wrap 
       the instance in a using() statement, or to call End() explicitly.

       Note: nothing bad happends if you call End() multiple times, but DoEnd() is called only one time.

   */

    public abstract class EndBase : IDisposable
    {
        internal bool _disposed;

        public void End()
        {
            Dispose();
        }

        public void Dispose()
        {
            DoDispose();
        }

        internal virtual void DoDispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                DoEnd();
            }
        }

        protected virtual void DoEnd()
        {
            OnPreEnd();
            OnEnd();
            OnPostEnd();
        }

        protected virtual void OnPreEnd() { }

        protected abstract void OnEnd();

        protected virtual void OnPostEnd() { }


    }
}
