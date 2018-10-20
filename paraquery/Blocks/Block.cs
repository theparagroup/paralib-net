using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Blocks
{
    public abstract class Block : SimpleBlock
    {
        private bool _format;
        private bool _empty;
        private bool _debug = true;


        protected Block(IContext context, bool format=true, bool empty=false):base(context)
        {
            _format = format;
            _empty = empty;
        }

        public bool IsFormat
        {
            get
            {
                return _format;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _empty;
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
            if (_format)
            {
                //this should be conditional on if newline was called last
                if (!_writer.IsNewLine)
                {
                    if (_debug)
                    {
                        _writer.Write("<!-- newlined prebegin -->", false);
                    }

                    _writer.NewLine();
                }
            }
        }

        protected abstract void OnBegin();

        protected virtual void OnPostBegin()
        {
            if (_format)
            {
                _writer.NewLine();

                if (!_empty)
                {
                    //any content should be indented
                    _writer.Indent();
                }
            }
        }

        protected override void OnPreEnd()
        {
            if (_format)
            {
                //this should be conditional on if newline was called last
                if (!_writer.IsNewLine)
                {
                    if (_debug)
                    {
                        _writer.Write("<!-- newlined preend -->", false);
                    }

                    _writer.NewLine();
                }

                if (!_empty)
                {
                    //undo the indent
                    _writer.Dedent();
                }
            }
        }

        protected override void OnPostEnd()
        {
            if (_format)
            {
                if(!_empty)
                {
                    //we already newlined in PostBegin()
                    _writer.NewLine();
                }
            }
        }


    }
}
