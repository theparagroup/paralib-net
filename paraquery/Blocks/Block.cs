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


        protected Block(IContext context, bool format = true, bool empty = false) : base(context)
        {
            _format = format;
            _empty = empty;
        }

        protected virtual string Name
        {
            get
            {
                return "";
            }
        }

        protected virtual string Id
        {
            get
            {
                return "";
            }
        }

        public bool IsFormatted
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
                //this should be conditional on if newline was called last before this block started
                if (!_writer.IsNewLine)
                {
                    if (_debug)
                    {
                        _writer.Write($" <!-- nl prebegin {Name} {Id} -->", false);
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
                //this should be conditional on if newline was called last in OnBegin
                if (!_writer.IsNewLine)
                {
                    if (_debug)
                    {
                        _writer.Write($" <!-- nl postbegin {Name} {Id} -->", false);
                    }

                    _writer.NewLine();
                }

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
                if (!_empty)
                {

                    //this should be conditional on if newline was called last in the content
                    if (!_writer.IsNewLine)
                    {
                        if (_debug)
                        {
                            _writer.Write($" <!-- nl preend {Name} {Id} -->", false);
                        }

                        _writer.NewLine();
                    }

                    //undo the indent
                    _writer.Dedent();
                }
            }
        }

        protected override void OnPostEnd()
        {
            if (_format)
            {
                //if this is an empty element, we already newlined in PostBegin(), so no need to do it again
                if (!_empty)
                {

                    //this should be conditional on if newline was called last in OnEnd
                    if (!_writer.IsNewLine)
                    {
                        if (_debug)
                        {
                            _writer.Write($" <!-- nl postend {Name} {Id} -->", false);
                        }

                        _writer.NewLine();
                    }

                }
            }
        }


    }
}
