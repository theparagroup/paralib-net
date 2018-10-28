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

        Renderer is the class used with the RendererStack. 

        Using Begin and End semantics you can build custom renderers, as we do with HTML tags.

        Some renderers are just containers, and do not render, usefull for creating controls and other
        logical components such as the Grid class.

    */

    public abstract class Renderer : BeginBase
    {
        protected IContext _context { private set; get; }
        protected IWriter _writer;
        private RenderModes _renderMode;
        private bool _visible;

        protected Renderer(IContext context, RenderModes renderMode, bool visible = true)
        {
            _context = context;
            _writer = _context.Writer;
            _renderMode = renderMode;
            _visible = visible;
        }

        public bool IsVisible
        {
            get
            {
                return _visible;
            }
        }

        public RenderModes RenderMode
        {
            get
            {
                return _renderMode;
            }
        }

        protected void Debug(string text)
        {
            if (this is ICommentator)
            {
                if (_context.Options.DebugSourceFormatting)
                {
                    ((ICommentator)this).Comment(text);
                }
            }
        }

        protected override void _begin()
        {
            //don't call OnPreBegin, Begin, or OnPostBegin if we're not visible

            if (_visible)
            {
                base._begin();
            }
        }

        protected override void OnPreBegin()
        {
            if (_renderMode == RenderModes.Line || _renderMode == RenderModes.Block)
            {
                //make sure we start on a newline
                //this should be conditional on if newline was called last before this block started
                if (!_writer.IsNewLine)
                {
                    Debug($"nl prebegin");
                    _writer.NewLine();
                }
            }
        }

        protected override void OnPostBegin()
        {
            if (_renderMode == RenderModes.Block)
            {
                //make sure content starts on a newline
                //this should be conditional on if newline was called last in OnBegin
                if (!_writer.IsNewLine)
                {
                    Debug($"nl postbegin");
                    _writer.NewLine();
                }

                //make sure content (for non-empty blocks) is indented
                _writer.Indent();
            }

        }

        protected override void _end()
        {
            //don't call OnPreEnd, End, or OnPostEnd if we're not visible

            if (_visible)
            {
                base._end();
            }
        }

        protected override void OnPreEnd()
        {
            if (_renderMode == RenderModes.Block)
            {
                //make sure blocks with endings (non-empty) end on a newline, and back out the ident level
                //this should be conditional on if newline was called last in the content
                if (!_writer.IsNewLine)
                {
                    Debug($"nl preend");

                    _writer.NewLine();
                }

                //undo the indent
                _writer.Dedent();
            }
        }

        protected override void OnPostEnd()
        {

            if (_renderMode == RenderModes.Line || _renderMode == RenderModes.Block)
            {
                //make sure we end with a newline
                //this should be conditional on if newline was called last when this block ended
                if (!_writer.IsNewLine)
                {
                    Debug($"nl postend");
                    _writer.NewLine();
                }
            }
        }



    }
}
