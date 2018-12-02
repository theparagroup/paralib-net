using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;
using com.parahtml.Core;

namespace com.parahtml.Html
{
    public class HtmlRendererStack : RendererStack, IHasContext
    {
        protected HtmlContext Context { private set; get; }

        public HtmlRendererStack(LineModes lineMode) : base(lineMode)
        {
        }

        public void SetContext(HtmlContext context)
        {
            Context = context;
        }

        public override IRenderer Open(IRenderer renderer)
        {
            if (renderer is IHasContext)
            {
                ((IHasContext)renderer).SetContext(Context);
            }

            Push(renderer);
            return renderer;
        }

    }
}
