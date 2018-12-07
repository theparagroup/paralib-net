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
    public class HtmlRendererStack : RendererStack//, IHtmlRendererStack
    {

        public HtmlRendererStack(LineModes lineMode) : base(lineMode)
        {
        }

        public override void Open(IRenderer renderer)
        {
            Push(renderer);
        }

    }
}
