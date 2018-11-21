using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Html
{
    public class FluentHtml<C> : FluentHtmlBase<C,FluentHtml<C>> where C:HtmlContext
    {
        public FluentHtml(C context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }
    }
}
