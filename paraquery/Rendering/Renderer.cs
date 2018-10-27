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

        protected Renderer(IContext context)
        {
            _context = context;
            _writer = _context.Writer;
        }

    }
}
