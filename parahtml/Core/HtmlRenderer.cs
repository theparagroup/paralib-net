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
    public abstract class HtmlRenderer : Renderer
    {
        public HtmlRenderer(HtmlContext context, LineModes lineMode, ContainerModes containerMode, bool visible, bool indentContent) : base(context, lineMode, containerMode, visible, indentContent)
        {
        }

        protected new HtmlContext Context
        {
            get
            {
                return (HtmlContext)base.Context;
            }
        }

        public static void Comment(Writer writer, string text)
        {
            writer.Write($"<!-- {text} -->");
        }

        public void Comment(string text)
        {
            Comment(Writer, text);
        }

        protected override void OnDebug(string text)
        {
            Comment(text);
        }

    }
}
