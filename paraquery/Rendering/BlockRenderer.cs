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


    //public abstract class BlockRenderer : Renderer
    //{
        
    //}
}
