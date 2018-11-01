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
        public HtmlRenderer(Context context, RenderModes renderMode, bool visible = true) : base(context, renderMode, visible)
        {
        }

        public static void Comment(Writer writer, string text)
        {
            writer.Write($"<!-- {text} -->");
        }

        public void Comment(string text)
        {
            Writer.Write($"<!-- {text} -->");
        }

        protected override void OnDebug(string text)
        {
            Comment(text);
        }


    }
}
