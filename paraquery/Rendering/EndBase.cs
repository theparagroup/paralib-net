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


   */

    public abstract class EndBase : IDisposable
    {
        private bool _disposed;

        public void End()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _end();
            }
        }

        protected virtual void _end()
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
