using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    public abstract class Block : IDisposable
    {
        private bool _autoIndent;
        private bool _disposed;
        public IContext _context { private set; get; }

        protected Block(IContext context, bool autoIndent=true)
        {
            _context = context;
            _autoIndent = autoIndent;
        }

        protected IResponse _response
        {
            get
            {
                return _context.Response;
            }
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
               _response.NewLine();
               _response.Indent();
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
                //this should be conditional on if newline was called last
                if (!_response.IsNewLine)
                {
                    _response.Tab();
                    _response.Write("<!-- newlined -->",false);
                    _response.NewLine();
                }

                _response.Dedent();
            }
        }

        protected abstract void OnEnd();

        protected virtual void OnPostEnd()
        {
            _response.NewLine();
        }


    }
}
