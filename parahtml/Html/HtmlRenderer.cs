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
    /*

        All HTML-based renderers should derive from this.

    */
    public abstract class HtmlRenderer : Renderer
    {
        public HtmlRenderer(LineModes lineMode, ContainerModes containerMode, bool indentContent) : base(lineMode, containerMode, indentContent)
        {
        }

        public HtmlRenderer(HtmlContext context, LineModes lineMode, ContainerModes containerMode, bool indentContent) : base(lineMode, containerMode, indentContent)
        {
        }

        protected new HtmlContext Context
        {
            get
            {
                return (HtmlContext)base.Context;
            }
        }

        protected new HtmlOptions Options
        {
            get
            {
                return (HtmlOptions)base.Context.Options;
            }
        }

        public static void Comment(Writer writer, string text)
        {
            writer.Write($"<!-- {text} -->");
        }

        protected override void Comment(string text)
        {
            Comment(Writer, text);
        }

    }
}
