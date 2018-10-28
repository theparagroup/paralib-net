using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*

        BlockRenderers are formatted, with start (and optionally end) tags getting thier own line. 
        
        BlockRenderers can be visible or invisible. This refers to the block start and end, not 
        content. Content is always visible if present, and is not under the control of the block anyway.

        Content can be optionally "offset" (on a new line an indented).

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

        With content offset:

            <start>
                content
            </end>

        Without:

            <start>content</content>

        HTML "empty tags" would have offsetContent == false

            <br />

    */
    public abstract class BlockRenderer : Renderer
    {
        private bool _visible;
        private bool _offsetContent;

        protected BlockRenderer(IContext context, bool offsetContent, bool visible = true) : base(context)
        {
            _offsetContent = offsetContent;
            _visible = visible;
        }

        public bool IsVisible
        {
            get
            {
                return _visible;
            }
        }

        public bool IsContentOffset
        {
            get
            {
                return _offsetContent;
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
            //make sure new blocks start on a newline
            //this should be conditional on if newline was called last before this block started
            if (!_writer.IsNewLine)
            {
                Debug($"nl prebegin");

                _writer.NewLine();
            }
        }

        protected override void OnPostBegin()
        {
            //make sure block content starts on a newline
            //make sure content (for non-empty blocks) is indented

            //this should be conditional on if newline was called last in OnBegin
            if (!_writer.IsNewLine)
            {
                Debug($"nl postbegin");

                _writer.NewLine();
            }

            if (_offsetContent)
            {
                //any content should be indented
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
            //make sure blocks with endings (non-empty) end on a newline, and back out the ident level
            if (_offsetContent)
            {

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
            //TODO i think this is assuming we don't write anything in OnEnd()... not sure

            //after a block ends, go ahead and newline for the next renderer if needed (PreBegin would do this if we didn't)

            //if the content is not offset on additional lines, then we already newlined in PostBegin(), so no need to do it again
            if (_offsetContent)
            {

                //this should be conditional on if newline was called last in OnEnd
                if (!_writer.IsNewLine)
                {
                    Debug($"nl postend");

                    _writer.NewLine();
                }

            }
        }
    }
}
