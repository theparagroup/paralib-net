using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;

namespace com.paraquery.Core
{
    public abstract class Block : IDisposable
    {
        private bool _disposed;
        public IContext Context { private set; get; }

        protected Block(IContext context)
        {
            Context = context;
        }

        protected virtual void Begin()
        {
            OnBegin();
            Context.Response.Indent();
        }

        protected abstract void OnBegin();

        public void End()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                Context.Response.Dedent();
                OnEnd();
            }
        }

        protected abstract void OnEnd();

    }
}
