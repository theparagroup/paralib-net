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
        private bool _autoIndent;
        private bool _disposed;
        public IContext Context { private set; get; }

        protected Block(IContext context, bool autoIndent=true)
        {
            Context = context;
            _autoIndent = autoIndent;
        }

        protected virtual void Begin()
        {
            OnPreBegin();
            OnBegin();
            OnPostBegin();
        }

        protected virtual void OnPreBegin()
        {
        }

        protected abstract void OnBegin();

        protected virtual void OnPostBegin()
        {
            if (_autoIndent)
            {
                Context.Response.Indent();
            }
        }

        public void End()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                OnPreEnd();
                OnEnd();
                OnPostEnd();
            }
        }

        protected virtual void OnPreEnd()
        {
            if (_autoIndent)
            {
                Context.Response.Dedent();
            }
        }


        protected abstract void OnEnd();

        protected virtual void OnPostEnd()
        {
        }


    }
}
