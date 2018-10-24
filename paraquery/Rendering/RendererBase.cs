using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{

    /*

       Base class for "Renderers", the general conceptual unit paraquery is built around.

       Provides for context and a writer, etc.

       Defines the basics of the IDisposable interface ("End" semantics). It is up to the instatiator wrap 
       the instance in a using() statement, or to call End() explicitly.

       Also provides "Begin semantics". It is up to the instatiator or subclass to call Begin().

       Renderer derives from RendererBase, and is the primary class used in the framework, but whenever an implementor
       wants to use "using()" syntax, they can derive from RendererBase.

   */

    public abstract class RendererBase : IDisposable
    {
        private bool _disposed;
        protected IContext _context { private set; get; }
        protected IWriter _writer;

        protected RendererBase(IContext context)
        {
            _context = context;
            _writer = _context.Writer;
        }

        protected abstract void Debug(string message);

        public void Begin()
        {
            _begin();
        }

        protected virtual void _begin()
        {
            OnPreBegin();
            OnBegin();
            OnPostBegin();
        }

        protected virtual void OnPreBegin() { }

        protected abstract void OnBegin();

        protected virtual void OnPostBegin() { }

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
