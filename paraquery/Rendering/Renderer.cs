using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*

        Renderer is the base class for use with the RendererStack. 
        
        Provides for basic (source) formatting of nested blocks, i.e. "pretty printing".

        Renderers can be Blocks, Inline, or Containers.

        Containers and Inline Renderers do no formatting, however we keep them distinct for differences between Blocks and Inline.
        The RendererStack uses the renderer type to determine when to close nested blocks. Adding a block to an inline will
        close all blocks up to and including the last non-inlined block.

        Blocks are formatted with newlines and tabs. Empty Blocks are formatted at the begining of the Block with a newline
        before and after, but Inlines (empty or not) are not formatted.

        Example:

            block1-start
                block2-start
                    unformatted-block-start...unformatted-block-end
                block2-end
            block1-end

        Of course, for HTML it will look more like this

            <div>
                <div>
                    <span>content <strong>is</strong> king</span></br>
                </div>
            </div>

        Start tags would be generated in the OnBegin, end tags in the OnEnd.

        Note, Blocks generate newlines and controls the tab level, but does NOT generate the tabs...  this is handled by the Writer
        based on the indent level (the first content after a newline is tabbed out per the tab level, subsequent content is not.)

    */

    public abstract class Renderer : BeginBase
    {
        protected IContext _context { private set; get; }
        protected IWriter _writer;
        private RendererTypes _rendererType;
        private bool _empty;

        public string Extra { set; get; }

        protected Renderer(IContext context, RendererTypes rendererType, bool empty)
        {
            _context = context;
            _writer = _context.Writer;
            _rendererType = rendererType;
            _empty = empty;
        }


        protected abstract void Debug(string message);


        //protected override void Debug(string message)
        //{
        //    _writer.Write($" <!-- {message} -->");
        //}

        public RendererTypes RendererType
        {
            get
            {
                return _rendererType;
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

        protected override void OnPreBegin()
        {
            //make sure new blocks start on a newline
            if (_rendererType == RendererTypes.Block)
            {
                //this should be conditional on if newline was called last before this block started
                if (!_writer.IsNewLine)
                {
                    if (DebugSourceFormatting)
                    {
                        Debug($"nl prebegin");
                    }

                    _writer.NewLine();
                }
            }
        }


        protected override void OnPostBegin()
        {
            //make sure block content starts on a newline
            //make sure content (for non-empty blocks) is indented
            if (_rendererType == RendererTypes.Block)
            {
                //this should be conditional on if newline was called last in OnBegin
                if (!_writer.IsNewLine)
                {
                    if (DebugSourceFormatting)
                    {
                        Debug($"nl postbegin");
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
            //make sure blocks with endings (non-empty) end on a newline, and back out the ident level
            if (_rendererType == RendererTypes.Block)
            {
                if (!_empty)
                {

                    //this should be conditional on if newline was called last in the content
                    if (!_writer.IsNewLine)
                    {
                        if (DebugSourceFormatting)
                        {
                            Debug($"nl preend");
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
            //after a block ends, go ahead and newline for the next renderer if needed (PreBegin would do this if we didn't)
            if (_rendererType == RendererTypes.Block)
            {
                //if this is an empty element, we already newlined in PostBegin(), so no need to do it again
                if (!_empty)
                {

                    //this should be conditional on if newline was called last in OnEnd
                    if (!_writer.IsNewLine)
                    {
                        if (DebugSourceFormatting)
                        {
                            Debug($"nl postend");
                        }

                        _writer.NewLine();
                    }

                }
            }
        }


    }
}
