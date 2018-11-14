using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;
using com.paralib.Gen;

namespace com.parahtml.Core
{
    /*
        
        Basically defines Comment and OnDebug.

        All HTML-based renderers should derive from this.

    */
    public abstract class HtmlRenderer : RendererBase
    {
        public HtmlRenderer(HtmlContext context, LineModes lineMode, ContainerModes containerMode, bool indentContent) : base(context, lineMode, containerMode, indentContent)
        {
        }

        protected new HtmlContext Context
        {
            get
            {
                return (HtmlContext)base.Context;
            }
        }

        public static void HtmlComment(Writer writer, string text)
        {
            writer.Write($"<!-- {text} -->");
        }

        public static void CssComment(Writer writer, string text)
        {
            writer.Write($"/* {text} */");
        }

    }
}
