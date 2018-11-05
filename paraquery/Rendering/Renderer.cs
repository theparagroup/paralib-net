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
            
            FormatMode
            StructureMode
            Empty
            Indent

        Empty and StructureMode are not used here in the Renderer, but they are used to control how
        the renderers are pushed and popped in the RendererStack to create structured content.

        FormatMode controls how renderers are formatted (pretty-printed):

            For such a renderer:
                
                [start]{inside content}[end]

            These modes behave in the following ways:

                None (no indenting or newlines):

                    {outside content}[start]{inside content}[end]{outside content}

                Line (renderer is on a line by itself):
            
                    {outside content}
                    [start]{inside content}[end]
                    {outside content}

                Block (renderer starts and ends on newlines, content is on a newline and indented):

                    {outside content}
                        [start]
                            {inside content}
                        [end]
                    {outside content}


        Indent only applies to Block mode, as only "blocks" have nested content to indent. Indentation
        of block content is by default turned on, but can be turned off for special cases. We use it
        internally to make debug output easier to read (by eliminating excessive nesting).

        It is important to understand that our FormatModes only loosely correspond to HTML concepts.
        Our FormatModes are concerned with how generated text, markup or source code is formatted,
        not how it is displayed in a browser (for example).

        In HTML, "blocks" (CSS "display" property = "block"), can be formatted in source many ways:

            <div>
            </div>

            <div></div>

            <hr />

            <div>
                </div>


        All are "HTML blocks", however only the first one would have a FormatMode=Block. The second
        and third would be Line or None, and the last would be impossible to generate. 

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
        protected Context Context { private set; get; }
        public FormatModes FormatMode { private set; get; }
        public StructureModes StructureMode { private set; get; }
        public bool Empty { private set; get; }
        public bool Indent { private set; get; }

        protected Renderer(Context context, FormatModes formatMode, StructureModes structureMode, bool empty=false, bool indent=true)
        {
            Context = context;

            FormatMode = formatMode;
            StructureMode = structureMode;
            Empty = empty;
            Indent = indent;
        }

        private bool DebugSourceFormatting
        {
            get
            {
                return Context.IsDebug(DebugFlags.SourceFormatting);
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
            OnDebug($" {text}");
        }

        protected virtual void OnDebug(string text)
        {
        }

        protected override void OnPreBegin()
        {
            if (FormatMode == FormatModes.Line || FormatMode == FormatModes.Block)
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

        protected override void OnPostBegin()
        {
            if (FormatMode == FormatModes.Block)
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

        protected override void OnPreEnd()
        {
            if (FormatMode == FormatModes.Block)
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

        protected override void OnPostEnd()
        {
            //TODO: is this a duplicate of OnPreBegin? remove this?

            if (FormatMode == FormatModes.Line || FormatMode == FormatModes.Block)
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
