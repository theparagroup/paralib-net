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

        protected Block(IContext context, bool format = true, bool empty = false) : base(context)
        {
            _format = format;
            _empty = empty;
        }

        protected virtual string Description
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

        protected virtual bool DebugSourceFormatting
        {
            get
            {
                return _context.Options.DebugSourceFormatting;
            }
        }

        protected virtual void Comment(string text)
        {
            _writer.Write($" <!-- {text} -->", false);
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
                    if (DebugSourceFormatting)
                    {
                        Comment($"nl prebegin {Description} {Id}");
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
                    if (DebugSourceFormatting)
                    {
                        Comment($"nl postbegin {Description} {Id}");
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
                        if (DebugSourceFormatting)
                        {
                            Comment($"nl preend {Description} {Id}");
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
                        if (DebugSourceFormatting)
                        {
                            Comment($"nl postend {Description} {Id}");
                        }

                        _writer.NewLine();
                    }

                }
            }
        }


    }
}
