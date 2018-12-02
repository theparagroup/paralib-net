using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.paralib.Gen;

namespace com.parahtml.Html
{
    public class FluentHtml : FluentHtmlBase<FluentHtml>
    {
        public FluentHtml(HtmlContext context, RendererStack rendererStack) : base(rendererStack)
        {
            ((IHasContext)this).SetContext(context);
        }
    }
}
