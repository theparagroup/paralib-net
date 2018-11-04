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

        Renderers can be formatted or unformatted. Formatted Renderers use newlines and the indent
        level to "pretty print", but otherwise all of the "OnXXX" methods are called, OnEnd(), etc.

        Renderers are formatted (pretty-printed) based on thier RenderMode:

            Inline (effectively the same as Component, except the RendererStack treats it differently):

                {outside content}[start]{inside content}[end]{outside content}

            Line:
            
                {outside content}
                    [start]{inside content}[end]
                {outside content}

            Block:

                {outside content}
                    [start]
                        {inside content}
                    [end]
                {outside content}


        Note, Renderer generate newlines and controls the tab level, but does NOT generate the tabs...  
        this is handled by the Writer based on the indent level (the first content after a newline is 
        tabbed out per the tab level, subsequent content is not.)

        It is totally possible to screw up the formatting with spurious newlines or changing the tab
        level, so care should be taken to structure your code with that in mind. The RendererStack
        solves this problem by putting Renderers on a stack and imposing certain rules. For example,
        pushing a block on top of an inline (i.e., trying to nest a block inside an inline) would close 
        all the inline renderers up to and including the last block, before pushing the new block.


    */

    public abstract class Renderer : BeginBase
    {
        private bool _debugSourceFormatting;
        protected Context Context { private set; get; }
        protected Writer Writer { private set; get; }
        public FormatModes FormatMode { private set; get; }
        public StructureModes StructureMode { private set; get; }

        public bool Indent { private set; get; }

        protected Renderer(Context context, FormatModes formatMode, StructureModes structureMode, bool indent=true)
        {
            Context = context;
            Writer = Context.Writer;
            FormatMode = formatMode;
            StructureMode = structureMode;
            Indent = indent;

            _debugSourceFormatting = Context.IsDebug(DebugFlags.SourceFormatting);
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
                //make sure we start on a newline
                //this should be conditional on if newline was called last before this block started
                if (!Writer.IsNewLine)
                {
                    if (_debugSourceFormatting)
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
                //make sure content starts on a newline
                //this should be conditional on if newline was called last in OnBegin
                if (!Writer.IsNewLine)
                {
                    if (_debugSourceFormatting)
                    {
                        Debug($"nl postbegin");
                    }

                    Writer.NewLine();
                }

                if (Indent)
                {
                    //make sure content (for non-empty blocks) is indented
                    Writer.Indent();
                }
            }
        }

        protected override void OnPreEnd()
        {
            if (FormatMode == FormatModes.Block)
            {
                //make sure blocks with endings (non-empty) end on a newline, and back out the ident level
                //this should be conditional on if newline was called last in the content
                if (!Writer.IsNewLine)
                {
                    if (_debugSourceFormatting)
                    {
                        Debug($"nl preend");
                    }

                    Writer.NewLine();
                }

                if (Indent)
                {
                    //undo the indent
                    Writer.Dedent();
                }
            }
        }

        protected override void OnPostEnd()
        {
            if (FormatMode == FormatModes.Line || FormatMode == FormatModes.Block)
            {
                //make sure we end with a newline
                //this should be conditional on if newline was called last when this block ended
                if (!Writer.IsNewLine)
                {
                    if (_debugSourceFormatting)
                    {
                        Debug($"nl postend");
                    }

                    Writer.NewLine();
                }
            }
        }

    }
}
