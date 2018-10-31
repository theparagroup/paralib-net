using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.Html
{
    public abstract class HtmlRenderer : Renderer
    {
        public HtmlRenderer(IContext context, RenderModes renderMode, bool visible = true) : base(context, renderMode, visible)
        {
        }

        public static void HtmlComment(IWriter writer, string text)
        {
            writer.Write($"<!-- {text} -->");
        }

        protected override void Comment(string text)
        {
            HtmlComment(Writer, text);
        }


    }
}
