using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Blocks
{
    /*

        SimpleBlock defines the basics of the IDisposable interface and provides for context and a writer.

        Block derived from this class, but this class is also useful for non-html containers such as the ElementContainer.

        It is up to the instatiator wrap the instance in a using statement, or to call End() explicitly.

    */

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
