using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*

        Base class for "Renderers", the general conceptual unit paraquery is built around.

        Provides for a context, etc.

        Renderer is the class used with the RendererStack, which allows renderers to be nested. 

        Using Begin and End semantics you can build custom renderers, as we do with HTML tags.

        Renderers have some important properties that control how they are formatted (pretty-printed)
        and how the behave in the RendererStack:
            
            LineMode
            Indent
            StackMode
            Terminal
            Visible

        StackMode and Terminal are not used here in the Renderer, but they are used to control how
        the renderers are pushed and popped in the RendererStack to create structured content.

        Visible basically controls what is written to the Writer. Invisible renderers do not write
        anything (in this base class) to the output stream. However, all of the BeginBase and EndBase
        methods are called in the normal sequence.

        LineMode controls how renderers are formatted (pretty-printed):

            For such a renderer:
                
                [start]{inside content}[end]

            These modes behave in the following ways:

                None (no indenting or newlines):

                    {outside content}[start]{inside content}[end]{outside content}

                Single (renderer is on a line by itself):
            
                    {outside content}
                    [start]{inside content}[end]
                    {outside content}

                Multiple (renderer starts and ends on newlines, content is on a newline and indented):

                    {outside content}
                        [start]
                            {inside content}
                        [end]
                    {outside content}


        Indent only applies to Multiple mode, as only this mode can have nested content to indent. 
        Indentation of content is by default turned on, but can be turned off for special cases. We 
        use it internally to make debug output easier to read (by eliminating excessive nesting).

        Note, Renderer generate newlines and controls the tab level, but does NOT generate the tabs...  
        this is handled by the Writer based on the indent level. The Writer always indents the first 
        Write() after a newline (based on the current tab level) but subsequent Writes() are not.

        It is totally possible to screw up the formatting with spurious newlines or changing the tab
        level, so care should be taken to structure your code with that in mind. The RendererStack
        solves this problem by putting Renderers on a stack and imposing certain rules. For example,
        pushing a block on top of an inline (i.e., trying to nest a block inside an inline) would close 
        all the inline renderers up to and including the last block, before pushing the new block.

        Generally speaking, you should structure your content by creating custom renderers and 
        components, and not by issuing tabs and newlines into the content stream. 
        
        It is fine to issue newlines inside block content (it was designed for that).

    */

    public abstract class Renderer : BeginBase
    {
        public Context Context { private set; get; }
        public LineModes LineMode { private set; get; }
        public StackModes StackMode { private set; get; }
        public bool Terminal { private set; get; }
        public bool Indent { private set; get; }
        public bool Visible { private set; get; }

        protected Renderer(Context context, LineModes lineMode, StackModes stackMode, bool terminal, bool visible, bool indent=true)
        {
            Context = context;

            LineMode = lineMode;
            StackMode = stackMode;
            Terminal = terminal;
            Visible = visible;
            Indent = indent;
        }


        private bool DebugSourceFormatting
        {
            get
            {
                return Context.Options.DebugSourceFormatting;
            }
        }

        protected Writer Writer
        {
            get
            {
                return Context.Writer;
            }
        }

        protected void Debug(string text)
        {
            if (Visible)
            {
                OnDebug($" {text}");
            }
        }

        protected virtual void OnDebug(string text)
        {
        }

        protected override void OnPreBegin()
        {
            if (Visible)
            {
                if (LineMode == LineModes.Single || LineMode == LineModes.Multiple)
                {
                    //make sure lines and blocks start on a newline
                    //this should be conditional on if newline was called last before this block started
                    if (!Writer.IsNewLine)
                    {
                        if (DebugSourceFormatting)
                        {
                            Debug($"nl prebegin");
                        }

                        Writer.NewLine();
                    }
                }
            }
        }

        protected override void OnPostBegin()
        {
            if (Visible)
            {
                if (LineMode == LineModes.Multiple)
                {
                    //make sure content nested under a block starts on a newline
                    //this should be conditional on if newline was called last in OnBegin
                    if (!Writer.IsNewLine)
                    {
                        if (DebugSourceFormatting)
                        {
                            Debug($"nl postbegin");
                        }

                        Writer.NewLine();
                    }

                    if (Indent)
                    {
                        //make sure content under blocks is indented (can be disabled)
                        Writer.Indent();
                    }
                }
            }
        }

        protected override void OnPreEnd()
        {
            if (Visible)
            {
                if (LineMode == LineModes.Multiple)
                {
                    //make sure block endings start on a newline
                    //this should be conditional on if newline was called last in the content
                    if (!Writer.IsNewLine)
                    {
                        if (DebugSourceFormatting)
                        {
                            Debug($"nl preend");
                        }

                        Writer.NewLine();
                    }

                    if (Indent)
                    {
                        //undo the indent for block content (can be disabled)
                        Writer.Dedent();
                    }
                }
            }
        }

        protected override void OnPostEnd()
        {
            //TODO: is this a duplicate of OnPreBegin? remove this?
            if (Visible)
            {
                if (LineMode == LineModes.Single || LineMode == LineModes.Multiple)
                {
                    //make sure blocks and lines end with a newline 
                    //this should be conditional on if newline was called last when this block ended
                    if (!Writer.IsNewLine)
                    {
                        if (DebugSourceFormatting)
                        {
                            Debug($"nl postend");
                        }

                        Writer.NewLine();
                    }
                }
            }
        }

    }
}
