using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;
using com.parahtml.Core;
using com.paralib.Gen;

namespace com.parahtml.Html
{
    public class HtmlRendererStack : RendererStack, IHtmlRendererStack
    {
        protected HtmlContext _context;

        public HtmlRendererStack(LineModes lineMode) : base(lineMode)
        {
        }

        void IHtmlRendererStack.SetContext(HtmlContext context)
        {
            _context = context;
        }

        public override IRenderer Open(IRenderer renderer)
        {
            renderer.SetContext(_context);
            Push(renderer);
            return renderer;
        }

    }
}
