using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Tags.Fluent
{
    public class Html : Html<Html>
    {
        public Html(HtmlContext context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }
    }
}
