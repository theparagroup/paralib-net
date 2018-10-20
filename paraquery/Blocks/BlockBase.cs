using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Blocks
{
    public abstract class SimpleBlock : IDisposable
    {
        private bool _disposed;
        protected IContext _context { private set; get; }
        protected IWriter _writer  => _context.Writer;

        protected SimpleBlock(IContext context)
        {
            _context = context;
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
        }

        protected abstract void OnEnd();

        protected virtual void OnPostEnd()
        {
        }


    }
}
