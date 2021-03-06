﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
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
            ContainerMode

        ContainerMode is not used here in the Renderer, but it is are used to control how
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

        Additional note: OnPreBegin() and OnPostEnd() seem to do the same thing so it's easy to 
        wonder why we do it in both places. Usually OnPostEnd() takes care of it, but since we have 
        no control over the Writer state, it's possible for impelementors to write content before a 
        Single/Multiple renderer's Begin() is called, in which case we need to do it in OnPreBegin().

    */

    public class Renderer : IContent
    {
        private Context _context;
        public ContentStates ContentState { private set; get; } = ContentStates.New;
        public LineModes LineMode { private set; get; }
        public ContainerModes ContainerMode { private set; get; }
        public bool IndentContents { private set; get; }


        public Renderer(LineModes lineMode, ContainerModes containerMode, bool indentContents)
        {
            LineMode = lineMode;
            ContainerMode = containerMode;
            IndentContents = indentContents;
        }

        protected virtual Context Context
        {
            get
            {
                return _context;
            }
        }

        protected Writer Writer
        {
            get
            {
                return Context.Writer;
            }
        }

        protected Options Options
        {
            get
            {
                return Context.Options;
            }
        }

        protected virtual void Comment(string text)
        {
        }

        private bool DebugSourceFormatting
        {
            get
            {
                return Options.DebugSourceFormatting;
            }
        }

        protected virtual void Begin(Context context, LineModes? lineMode)
        {
            if (context==null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;

            if (lineMode!=null)
            {
                LineMode = lineMode.Value;
            }

            OnPreBegin();
            OnBegin();
            OnPostBegin();
            OnPreContent();
        }

        protected virtual void OnPreBegin()
        {
            if (LineMode == LineModes.Single || LineMode == LineModes.Multiple)
            {
                //make sure lines and blocks start on a newline
                //this should be conditional on if newline was called last before this block started
                if (!Writer.IsNewLine)
                {
                    if (DebugSourceFormatting)
                    {
                        Comment($"nl prebegin");
                    }

                    Writer.NewLine();
                }
            }
        }

        public event EventHandler<EventArgs> Opening;

        protected virtual void OnBegin()
        {
            var opening = Opening;

            if (opening!=null)
            {
                opening(this, EventArgs.Empty);
            }
        }

        protected virtual void OnPostBegin()
        {
            if (LineMode == LineModes.Multiple)
            {
                //make sure content nested under a block starts on a newline
                //this should be conditional on if newline was called last in OnBegin
                if (!Writer.IsNewLine)
                {
                    if (DebugSourceFormatting)
                    {
                        Comment($"nl postbegin");
                    }

                    Writer.NewLine();
                }

                if (IndentContents)
                {
                    //indent content
                    Writer.Indent();
                }

            }
        }

        protected virtual void OnPreContent()
        {
        }


        protected virtual void End()
        {
            OnPreEnd();
            OnEnd();
            OnPostEnd();
        }

        protected virtual void OnPreEnd()
        {
            if (LineMode == LineModes.Multiple)
            {
                //make sure block endings start on a newline
                //this should be conditional on if newline was called last in the content
                if (!Writer.IsNewLine)
                {
                    if (DebugSourceFormatting)
                    {
                        Comment($"nl preend");
                    }

                    Writer.NewLine();
                }

                if (IndentContents)
                {
                    //undo the content indent
                    Writer.Dedent();
                }
            }
        }

        public event EventHandler<EventArgs> Closing;

        protected virtual void OnEnd()
        {
            var closing = Closing;

            if (closing != null)
            {
                closing(this, EventArgs.Empty);
            }
        }

        protected virtual void OnPostEnd()
        {
            if (LineMode == LineModes.Single || LineMode == LineModes.Multiple)
            {
                //make sure blocks and lines end with a newline 
                //this should be conditional on if newline was called last when this block ended
                if (!Writer.IsNewLine)
                {
                    if (DebugSourceFormatting)
                    {
                        Comment($"nl postend");
                    }

                    Writer.NewLine();
                }
            }
        }
       

        void IContent.Open(Context context, LineModes? lineMode)
        {
            Begin(context, lineMode);
            ContentState = ContentStates.Open;
        }

        void IContent.Close()
        {
            End();
            ContentState = ContentStates.Closed;
        }

    }
}
