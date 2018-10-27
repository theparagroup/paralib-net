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

        protected Renderer(IContext context)
        {
            _context = context;
            _writer = _context.Writer;
        }

    }
}
